using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoosePlayer : MonoBehaviour
{
    public DatosGuardados d;
    public TMP_InputField Name;
    public Button[] botones;

    void Start()
    {
        // Deshabilitar los botones al inicio
        DeshabilitarBotones();
    }

    void Update()
    {
        // Verificar si se presiona Enter en el InputField y hay texto escrito
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(Name.text))
        {
            string nombre = Name.text;
            d.setName(nombre);
            // Habilitar los botones si se cumple la condición
            HabilitarBotones();
            // Desactivar el InputField
            Name.gameObject.SetActive(false);
        }
    }

    public void Jugar(string nombre)
    {
        d.whichPlayer(nombre);
        // Cargar la siguiente escena cuando se presione el botón jugar
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void DeshabilitarBotones()
    {
        // Deshabilitar todos los botones en el array
        foreach (Button boton in botones)
        {
            boton.interactable = false;
        }
    }

    void HabilitarBotones()
    {
        // Habilitar todos los botones en el array
        foreach (Button boton in botones)
        {
            boton.interactable = true;
        }
    }
}
