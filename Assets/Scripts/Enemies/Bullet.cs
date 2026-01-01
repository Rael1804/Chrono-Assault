using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject[] balas;

    public void DesactivarBalas(int indice)
    {
        balas[indice].SetActive(false);
    }
    public void ActivarBalas(int cantidad)
    {
        DesactivarBalasAll();
        for (int i = 0; i < cantidad; i++)
        {
            balas[i].SetActive(true);
        }
    }

    public void DesactivarBalasAll()
    {
        foreach (GameObject bala in balas)
        {
            bala.SetActive(false);
        }
    }

}
