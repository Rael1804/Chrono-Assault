using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class Puntuaciones : MonoBehaviour
{
    [SerializeField] private Transform Nombre;
    [SerializeField] private Transform Tiempo;
    [SerializeField] private Transform Oleada;
    [SerializeField] private Transform Aliens;

    [SerializeField] private GameObject prefabTexto;
 
    public Canvas canvas;

    private List<string> jugador = new List<string>();
    private List<string> tiempo = new List<string>();
    private List<int> oleada = new List<int>();
    private List<int> aliens = new List<int>();

    private Color Primero;
    private Color Segundo;
    private Color Tercero;

    private List<DatosJugador> datosJugadores;
    // Start is called before the first frame update
    void Start()
    {
        ColorUtility.TryParseHtmlString("#EABE3F", out Primero);
        ColorUtility.TryParseHtmlString("#BEBEBE", out Segundo);
        ColorUtility.TryParseHtmlString("#CD7F32", out Tercero);

        Cargar();
    }

    public void Cargar()
    {
        SavePuntuacion puntuacion = SaveManager.LoadDataPuntuacion();
        Debug.Log("Datos: " + puntuacion);
        if (puntuacion != null)
        {
            jugador = puntuacion.jugador;
            oleada = puntuacion.oleada;
            tiempo = puntuacion.tiempo;
            aliens = puntuacion.aliens;

            datosJugadores = UnirListas(jugador, tiempo, oleada, aliens);
            datosJugadores = datosJugadores
     .OrderByDescending(jugador => jugador.Oleada)
     .ThenByDescending(jugador => jugador.Aliens)
     .ThenBy(jugador => jugador.Tiempo)
     .ToList();
            CargarDatosPartidasGuardadas();
        }
    }

    private void CargarDatosPartidasGuardadas()
    {
        int i = 0;

        Debug.Log("Hay " + jugador.Count + " jugadores");
        while (i < Mathf.Min(jugador.Count, 5))
        {
    
            GameObject TextoOleada = Instantiate(prefabTexto, canvas.transform);
            GameObject TextoNombre = Instantiate(prefabTexto, canvas.transform);
            GameObject TextoTiempo = Instantiate(prefabTexto, canvas.transform);
            GameObject TextoAliens = Instantiate(prefabTexto, canvas.transform);


            TextoOleada.GetComponent<TextMeshProUGUI>().text = datosJugadores[i].Oleada.ToString();
            TextoNombre.GetComponent<TextMeshProUGUI>().text = datosJugadores[i].Jugador;
            TextoTiempo.GetComponent<TextMeshProUGUI>().text = datosJugadores[i].Tiempo;
            TextoAliens.GetComponent<TextMeshProUGUI>().text = datosJugadores[i].Aliens.ToString();


            TextoTiempo.transform.position = new Vector3(Tiempo.position.x, Tiempo.position.y - 15 - i * 15, Tiempo.position.z); // Cambiar la posición vertical
            TextoTiempo.transform.localScale = new Vector3(1.388649f, 1.388649f, 1.388649f);

            TextoNombre.transform.position = new Vector3(Nombre.position.x, Nombre.position.y -15f - i * 15f, Nombre.position.z); // Cambiar la posición vertical
            TextoNombre.transform.localScale = new Vector3(1.388649f, 1.388649f, 1.388649f);

            TextoOleada.transform.position = new Vector3(Oleada.position.x, Oleada.position.y  -15 - i * 15,Oleada.position.z); // Cambiar la posición vertical
            TextoOleada.transform.localScale = new Vector3(1.388649f, 1.388649f, 1.388649f);

            TextoAliens.transform.position = new Vector3(Aliens.position.x, Aliens.position.y -15 - i * 15, Aliens.position.z); // Cambiar la posición vertical
            TextoAliens.transform.localScale = new Vector3(1.388649f, 1.388649f, 1.388649f);

            switch (i)
            {
                case 0:
                    TextoOleada.GetComponent<TextMeshProUGUI>().color = Color.black;
                    TextoTiempo.GetComponent<TextMeshProUGUI>().color = Color.black;
                    TextoNombre.GetComponent<TextMeshProUGUI>().color = Color.black;
                    TextoAliens.GetComponent<TextMeshProUGUI>().color = Color.black;
                    break;
                case 1:
                    TextoOleada.GetComponent<TextMeshProUGUI>().color = Color.black;
                    TextoTiempo.GetComponent<TextMeshProUGUI>().color = Color.black;
                    TextoNombre.GetComponent<TextMeshProUGUI>().color = Color.black;
                    TextoAliens.GetComponent<TextMeshProUGUI>().color = Color.black;
                    break;
                case 2:
                    TextoOleada.GetComponent<TextMeshProUGUI>().color = Color.black;
                    TextoTiempo.GetComponent<TextMeshProUGUI>().color = Color.black;
                    TextoNombre.GetComponent<TextMeshProUGUI>().color = Color.black;
                    TextoAliens.GetComponent<TextMeshProUGUI>().color = Color.black;
                    break;
                default:
                    TextoOleada.GetComponent<TextMeshProUGUI>().color = Color.black;
                    TextoTiempo.GetComponent<TextMeshProUGUI>().color = Color.black;
                    TextoNombre.GetComponent<TextMeshProUGUI>().color = Color.black;
                    TextoAliens.GetComponent<TextMeshProUGUI>().color = Color.black;
                    break;
            }
            i++;
        }
    }

    static List<DatosJugador> UnirListas(List<string> nombres, List<string> tiempos, List<int> oleadas, List<int> aliens)
    {
        List<DatosJugador> datosJugadores = new List<DatosJugador>();

        // Verificar que todas las listas tengan la misma longitud
        if (nombres.Count == tiempos.Count && nombres.Count == oleadas.Count && nombres.Count == aliens.Count)
        {
            // Crear un objeto DatosJugador para cada conjunto de datos y agregarlo a la lista
            for (int i = 0; i < nombres.Count; i++)
            {
                datosJugadores.Add(new DatosJugador(nombres[i], tiempos[i], oleadas[i], aliens[i]));
            }
        }
        return datosJugadores;
    }

    public void volvermenu()
    {
        SceneManager.LoadScene(0);
    }
}
