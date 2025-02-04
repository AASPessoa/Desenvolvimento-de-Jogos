using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float vel;
    [SerializeField] private float pulo;
    [SerializeField] private float gravidade;
    [SerializeField] private float maxDashTime;
    [SerializeField] private float veldash;
    [SerializeField] private float parardash;
    [SerializeField] private Transform pontoataque;
    [SerializeField] private float raioataque;
    [SerializeField] private LayerMask layerataque;
    [SerializeField] private float raioataque2;
    [SerializeField] private int vida;
    [SerializeField] private int dano;
    [SerializeField] private int dano2;
    [SerializeField] private Animator animacoes;
    private int vidaatual;
    private float currentDashTime;
    private bool pulando;
    private float yVel;
    private float dx;
    private float dy;
    private bool viradodireita;
    private bool dashativo;
    private bool nochao;
    private bool atacando;

    
    // Start is called before the first frame update
    void Start()
    {
        viradodireita = true;
        dashativo = false;
        currentDashTime = maxDashTime;          
        pulando = true;
        nochao = false;
        vidaatual = vida;
        yVel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(FindAnyObjectByType<GameControlle>().jogoativo == false){
            return;
        }

        if(vidaatual <= 0){
            morte();
        }

        pular();
        dash();
        animacao();

        dy = yVel * Time.deltaTime;
        dx = Input.GetAxis("Horizontal") * vel * Time.deltaTime;
        
        if(!dashativo){
        if (Input.GetAxisRaw ("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f) 
        {
            if (Input.GetAxisRaw ("Horizontal") > 0.5f && !viradodireita) 
            {
                vira ();
                viradodireita = true;
            } 
            else if (Input.GetAxisRaw("Horizontal") < 0.5f && viradodireita) 
            {
                vira ();
                viradodireita = false;
            }
        }
        }

        float xMax = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        float xMin = xMax - 2*(xMax - Camera.main.transform.position.x);
        if(transform.position.x + dx <= xMin || transform.position.x + dx >= xMax)
            dx = 0;

        float yMax = Camera.main.transform.position.y + Camera.main.orthographicSize * Camera.main.aspect;
        float yMin = yMax - 2*(yMax - Camera.main.transform.position.y);
        if(transform.position.y + dy <= yMin || transform.position.y + dy >= yMax)
            dy = 0;

        transform.Translate(dx, dy, 0);
    }

    void animacao(){
        if(dx != 0f){
            animacoes.SetBool("correndo",true);
        }else{
            animacoes.SetBool("correndo",false);
        }

        if(pulando){
            animacoes.SetBool("pulando",true);
        }else{
            animacoes.SetBool("pulando",false);
        }

        if(dashativo){
            animacoes.SetBool("dash",true);
        }else{
            animacoes.SetBool("dash",false);
        }

        if(!pulando && !dashativo &&Input.GetKeyDown(KeyCode.J)){
            animacoes.SetTrigger("atacando");
        }

        if(!pulando && !dashativo &&Input.GetKeyDown(KeyCode.I)){
            animacoes.SetTrigger("atacando2");
        }
        
        if(dashativo && Input.GetKeyDown(KeyCode.J)){
            animacoes.SetTrigger("ataquedash");
        }
        
        if(pulando && Input.GetKeyDown(KeyCode.J)){
            animacoes.SetTrigger("ataquepulo");
        }
        
    }
    void atacar1(){
        
            Collider2D ataque = Physics2D.OverlapCircle(pontoataque.position,raioataque,layerataque);
            if(ataque){
                if(ataque.gameObject.GetComponent<EnemyController1>() != null){
                    ataque.gameObject.GetComponent<EnemyController1>().recebedano(dano);
                }
                if(ataque.gameObject.GetComponent<EnemyController2>() != null){
                    ataque.gameObject.GetComponent<EnemyController2>().recebedano(dano);
                }
                if(ataque.gameObject.GetComponent<EnemyController3>() != null){
                    ataque.gameObject.GetComponent<EnemyController3>().recebedano(dano);
                }
            }
        
    }

    void atacar2(){
        
            Collider2D ataque = Physics2D.OverlapCircle(pontoataque.position,raioataque2,layerataque);
            if(ataque){
                if(ataque.gameObject.GetComponent<EnemyController1>() != null){
                    ataque.gameObject.GetComponent<EnemyController1>().recebedano(dano2);
                }
                if(ataque.gameObject.GetComponent<EnemyController2>() != null){
                    ataque.gameObject.GetComponent<EnemyController2>().recebedano(dano2);
                }
                if(ataque.gameObject.GetComponent<EnemyController3>() != null){
                    ataque.gameObject.GetComponent<EnemyController3>().recebedano(dano2);
                }
            }
        
    }
    
    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(pontoataque.position,raioataque);
        Gizmos.color = Color.black;
    }

    void dash(){
        
        if(Input.GetKeyDown(KeyCode.L)){
            currentDashTime = 0.0f;
            dashativo = true;    
        }
        
        if((currentDashTime >= maxDashTime) || (Input.GetKeyUp(KeyCode.L))){
            dashativo = false;
        }

        if(dashativo)
	    {
            if(nochao){
            if(viradodireita)
                transform.Translate(vel * veldash * Time.deltaTime, 0, 0);
            else
                transform.Translate(vel * -veldash * Time.deltaTime, 0, 0);
            }                
		    currentDashTime += parardash;
	    }
        
        
    }
    

    void pular(){
        if(pulando)
        {
            if(nochao)
            {
                pulando = false;
                yVel = 0;
            }
            else{
                yVel -= gravidade * Time.deltaTime;
            }
        }
        else
        {
            if(nochao && Input.GetKeyDown(KeyCode.K))
            {
                pulando = true;
                nochao = false;
                yVel = pulo;
                if(dashativo)
                    yVel += 3;                    
            }
        }

    }

    void vira(){
        viradodireita = !viradodireita;
        UnityEngine.Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "chao")
        {
            nochao = true;
        }
        if(other.gameObject.tag == "espinho")
        {
            morte();
        }
        if(other.gameObject.tag == "inimigo"){
            if(other.gameObject.GetComponent<EnemyController1>() != null){
                int dano = other.gameObject.GetComponent<EnemyController1>().getdano();
                recebedano(dano);
            }
            if(other.gameObject.GetComponent<EnemyController2>() != null){
                int dano = other.gameObject.GetComponent<EnemyController2>().getdano();
                recebedano(dano);
            }
            if(other.gameObject.GetComponent<EnemyController3>() != null){
                int dano = other.gameObject.GetComponent<EnemyController3>().getdano();
                recebedano(dano);
            }
        }
    }
    
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "chao")
        {
            nochao = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "fim"){
            FindAnyObjectByType<SceneManage>().creditos();
        }
    }

    public void recebedano(int d){
        vidaatual -= d;
        FindAnyObjectByType<vidamanager>().recebedano(d);
        if(viradodireita)
            transform.Translate(-0.8f,0,0);
        if(!viradodireita)
            transform.Translate(0.8f,0,0);
    }
    
    void morte()
    {
        FindAnyObjectByType<GameControlle>().jogoativo = false;
        Destroy(this.gameObject);
        FindAnyObjectByType<SceneManage>().creditos();
    }

    public int getvida(){
        return vidaatual;
    }
}
