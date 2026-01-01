using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DatosJugador
{
    public string Jugador { get; set; }
    public string Tiempo { get; set; }
    public int Oleada { get; set; }

    public int Aliens { get; set; }

    public DatosJugador(string nombre, string metros, int escena, int aliens)
    {
        Jugador = nombre;
        Tiempo = metros;
        Oleada = escena;
        Aliens = aliens;
    }
}