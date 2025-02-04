using UnityEngine;

public class EnemyController3 : MonoBehaviour
{
    [SerializeField] private int vida;
    [SerializeField] private int dano;
    [SerializeField] private Animator animacoes;
    [SerializeField] private float vel;
    [SerializeField] private float pulo;
    [SerializeField] private float gravidade;
    [SerializeField] private Transform check;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float raio;
    [SerializeField] private GameObject player;

    private int vidaatual;
    private bool pulando;
    private bool viradodireita;
    private bool nochao;
    private float vely;
    private float tempo;
    private UnityEngine.Vector2 vec;
    private Collider2D vejogador;
    void Start()
    {
        vidaatual = vida;
        pulando = false;
        viradodireita = false;
        nochao = false;
        vec = new UnityEngine.Vector2(0,0);
        tempo = 0f;
    }

    void Update()
    {
        if(FindAnyObjectByType<GameControlle>().jogoativo == false){
            return;
        }
        
        if(vidaatual <= 0){
            morte();
        }
        
        animacao();

        tempo+=Time.deltaTime;

        if(tempo >=2){
            vira();
            tempo = 0f;
        }

        vejogador = Physics2D.OverlapCircle(check.position,raio,layer);

        if(vejogador && nochao){
            vely = pulo;
            if(viradodireita)
                vec = new UnityEngine.Vector2(vel,vely);
            if(!viradodireita)
                vec = new UnityEngine.Vector2(-vel,vely);

            pulando = true;
        }
        else{
            vec = new UnityEngine.Vector2(0,0);
        }
        
        if(pulando && !nochao)
        {
            if(nochao)
            {
                pulando = false;
                pulo = 0;
            }
            else{
                vely -= gravidade * Time.deltaTime;    
                vec = new UnityEngine.Vector2(-vel,vely);
            }
        }
        
        if(!vejogador){
           pulando = false;
        }

        transform.Translate(vec * Time.deltaTime);

    }



    void vira(){
        viradodireita = !viradodireita;
        UnityEngine.Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    void animacao(){
        if(pulando){
            animacoes.SetBool("pulando",true);
        }
        else{
            animacoes.SetBool("pulando",false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "chao")
        {
            nochao = true;
        }
             
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "chao")
        {
            nochao = false;
            pulando = true;
        }
    }

    public void recebedano(int d){
        vidaatual -= d;
    }
    public int getdano(){
        return dano;
    }
    void morte()
    {
        Destroy(this.gameObject);
    }
}

