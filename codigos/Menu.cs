using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void iniciar()
    {
        SceneManager.LoadScene("Jogo");
    }

    // Update is called once per frame
    public void sair()
    {
        Application.Quit();
    }
}
