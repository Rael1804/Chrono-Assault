
using UnityEngine;


public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MenuPrincipal;
    public GameObject MenuOpciones; // Transform del segundo Canvas
    public GameObject MenuPuntuaciones;
    public GameObject ElegirPersonaje;

    public void VerPrincipal()
    {
        MenuPrincipal.SetActive(true);
        MenuOpciones.SetActive(false);
        MenuPuntuaciones.SetActive(false);
        ElegirPersonaje.SetActive(false);
    }

    public void VerOpciones()
    {
        MenuPrincipal.SetActive(false);
        MenuOpciones.SetActive(true);
        MenuPuntuaciones.SetActive(false);
        ElegirPersonaje.SetActive(false);
    }

    public void VerPuntuaciones()
    {
        MenuPrincipal.SetActive(false);
        MenuOpciones.SetActive(false);
        MenuPuntuaciones.SetActive(true);
        ElegirPersonaje.SetActive(false);
    }

    public void ElegirJugador()
    {
        MenuPrincipal.SetActive(false);
        MenuOpciones.SetActive(false);
        MenuPuntuaciones.SetActive(false);
        ElegirPersonaje.SetActive(true);
    }

       public void Salir()
    {
        Application.Quit();
    }
}
