using Unity.Mathematics;
using UnityEngine;

public class EnemyController2 : MonoBehaviour
{
    [SerializeField] private int vida;
    [SerializeField] private int dano;
    [SerializeField] private Animator animacoes;
    [SerializeField] private float vel;
    private int vidaatual;
    private bool andando;
    private bool viradodireita;
    private float dx;


    void Start()
    {
        vidaatual = vida;
        andando = true;
        viradodireita = false;
        
    }

    void Update()
    {
        dx =  Time.deltaTime * vel;
        if(FindAnyObjectByType<GameControlle>().jogoativo == false){
            return;
        }
        if(vidaatual <= 0){
            morte();
        }
        
        animacao();

        if(viradodireita)
            transform.Translate(dx,0,0);
        else
            transform.Translate(-dx,0,0);
    }


    void vira(){
        viradodireita = !viradodireita;
        UnityEngine.Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void animacao(){
        if(andando){
            animacoes.SetBool("andando",true);
        }
        else{
            animacoes.SetBool("andando",false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.tag == "chao" || other.gameObject.tag == "inimigo")
        {
            vira();
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

