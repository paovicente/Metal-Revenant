//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    /*
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

    [SerializeField] private string nombreMenuPrincipal;

    private bool juegoPausado = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausa();
            }
        }
    }

    public void Pausa()
    {
        juegoPausado = true;
        Time.timeScale = 0f; // Pausa el tiempo del juego
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Reanudar()
    {
        juegoPausado = false;
        Time.timeScale = 1f; // Reanuda el tiempo del juego
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Reiniciar()
    {
        juegoPausado = false;
        Time.timeScale = 1f; // Asegúrate de reanudar el tiempo antes de reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Esta función ahora cargará la escena del menú principal
    public void Cerrar()
    {
        // Es muy importante reanudar el tiempo antes de cambiar de escena
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreMenuPrincipal);
    }
    */
}


