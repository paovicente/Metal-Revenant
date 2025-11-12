using UnityEngine;
//using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    /*
    public int vidaMaxima = 5;
    private int vidaActual;

    public Image barraRelleno; // Imagen que se llena (tipo Filled)

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarBarra();
    }

    public void RecibirDa?o(int da?o)
    {
        vidaActual -= da?o;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        Debug.Log("Jugador recibi? da?o. Vida actual: " + vidaActual);
        ActualizarBarra();

        if (vidaActual <= 0)
        {
            Morir();

        }
    }

    void ActualizarBarra()
    {
        if (barraRelleno != null)
        {
            float fill = (float)vidaActual / vidaMaxima;
            barraRelleno.fillAmount = fill;
        }
    }

    void Morir()
    {
        Debug.Log("Jugador muri?.");
        GameManager.Instance.GameOver();
        Destroy(gameObject);
        GameObject musica = GameObject.Find("MusicaFondo");
        if (musica != null)
        {
            Destroy(musica);
        }
    }
    */
}

