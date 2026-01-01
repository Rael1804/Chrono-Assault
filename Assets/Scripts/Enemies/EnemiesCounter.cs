using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EnemiesCounter : MonoBehaviour
{
    public int enemiesCounter = 0;
    public Text textEnemies;
    
    public int Oleada=1;
    public int lastOleada=1;
    public Text TextOleada;

    public Text AmmonationText;
    public int AmmonationCounter = 0;

    public Text ReloadText;
    public int ReloadCounter = 0;

    public Text KillsText;
    public int KillsCounter = 0;

    public TimeController tiempo;
    public DatosGuardados nombre;
    public ItemsSpawn itemsSpawn;

    private void Update() {
        if (Oleada != lastOleada) {
            lastOleada = Oleada;
            itemsSpawn.SetOleada(Oleada);
        }
    }
    
    public void IncrementEnemies()
    {
        enemiesCounter += 1;
        textEnemies.text = enemiesCounter + " ENEMIGOS";
    }

    public void textoOleada()
    {
        TextOleada.text ="OLEADA " + Oleada;
    }

    public void DecrementEnemies()
    {
        enemiesCounter -= 1;
        textEnemies.text = enemiesCounter + " ENEMIGOS";
    }

    public void SetAmmonation(int ammonation) {
        AmmonationCounter = ammonation;
        AmmonationText.text = AmmonationCounter + " BALAS";
    }

    public void SetReload(int ammonation) {
        ReloadCounter = ammonation;
        ReloadText.text = ReloadCounter + "";
    }

    public void IncrementKills()
    {
        KillsCounter += 1;
        KillsText.text = "KILLS " + KillsCounter;
    }


    public void ChangeScene(string sceneName)
    {
        if (enemiesCounter <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void guardar()
    {
        SaveManager.SavePuntuacionData(nombre.getName(),tiempo.getTime(),Oleada,KillsCounter);
    }
}
