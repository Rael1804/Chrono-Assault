using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayer : MonoBehaviour
{

    public bool ManPlayer = false;
    public GameObject PlayerMan;
    public GameObject PlayerWoman;
    public GameObject CameraControllerMan;
    public GameObject CameraControllerWoman;
    public GameObject GunControllerMan;
    public GameObject GunControllerWoman;
    public DatosGuardados d;

    public Text namePlayer;

    private void Awake()
    {

       if (d.getPlayer().Equals("Chico")) {
            PlayerMan.SetActive(true);
            PlayerWoman.SetActive(false);
            CameraControllerMan.SetActive(true);
            CameraControllerWoman.SetActive(false);
            GunControllerMan.SetActive(true);
            GunControllerWoman.SetActive(false);       
        } else {
            PlayerMan.SetActive(false);
            PlayerWoman.SetActive(true);
            CameraControllerMan.SetActive(false);
            CameraControllerWoman.SetActive(true);
            GunControllerMan.SetActive(false);
            GunControllerWoman.SetActive(true);
        }

        namePlayer.text = d.getName();
    }
}
