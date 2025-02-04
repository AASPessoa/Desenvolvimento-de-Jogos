using System.ComponentModel;
using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    [SerializeField] private int vida;
    [SerializeField] private int dano;
    [SerializeField] private Animator animacoes;
    [SerializeField] private float raioataque = 1f;
    [SerializeField] private LayerMask layerataque;
    [SerializeField] private Transform pontoataque;
    
    private int vidaatual;
    private bool atacando;
    private float tempo = 0f;

    void Start()
    {
        vidaatual = vida;
        atacando = false;
    }

    void Update()
    {
        if(FindAnyObjectByType<GameControlle>().jogoativo == false){
            return;
        }

        if(vidaatual <= 0){
            morte();
        }

        tempo+= Time.deltaTime;
        if(tempo >= 4f){
            tempo = 0f;
            atacando = true;
        }
        animacao();

    }

    void setataque(){
        atacando = false;
    }

    void ataque(){
        Collider2D ataque = Physics2D.OverlapCircle(pontoataque.position,raioataque,layerataque);
            if(ataque){
                if(ataque.gameObject.GetComponent<PlayerController>() != null){
                    ataque.gameObject.GetComponent<PlayerController>().recebedano(dano);
                }
            }
    }

    public int getdano(){
        return dano;
    }

    void animacao(){
        if(atacando){
            animacoes.SetBool("atacando",true);
        }
        else{
            animacoes.SetBool("atacando",false);
        }
    }

    public void recebedano(int d){
        vidaatual -= d;
    }

    void morte()
    {
        Destroy(this.gameObject);
    }
}
