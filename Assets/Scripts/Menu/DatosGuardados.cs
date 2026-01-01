using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosGuardados : MonoBehaviour
{
    public void whichPlayer(string player)
    {
        PlayerPrefs.SetString("Player",player);
    }

    public string getPlayer()
    {
        return PlayerPrefs.GetString("Player");
    }

    public void setName(string name)
    {
        PlayerPrefs.SetString("Name", name);
    }

    public string getName()
    {
        return PlayerPrefs.GetString("Name");
    }
}
