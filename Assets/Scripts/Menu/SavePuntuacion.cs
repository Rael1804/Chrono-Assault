
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public class SavePuntuacion
{
    public List<string> jugador = new List<string>(); // Inicializa la lista
    public List<string> tiempo = new List<string>();
    public List<int> oleada = new List<int>();
    public List<int> aliens = new List<int>();


    public SavePuntuacion(string j, string t, int o, int a)
    {

        SavePuntuacion data = SaveManager.LoadDataPuntuacion();
        if (data != null)
        {
            this.jugador = data.jugador;
            this.tiempo = data.tiempo;
            this.oleada = data.oleada;
            this.aliens = data.aliens;
        }


        jugador.Add(j);
        tiempo.Add(t);
        oleada.Add(o);
        aliens.Add(a);
    }



}
