using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class vidamanager : MonoBehaviour
{
    public UnityEngine.UI.Image barravida;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void recebedano(float d){
        barravida.fillAmount -= d / 10f;
    }
}
