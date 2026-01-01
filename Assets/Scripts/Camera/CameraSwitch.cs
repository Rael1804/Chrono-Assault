using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject cameraFirstPerson;
    public GameObject cameraThirdPerson;
    public PlayerMove playerMove;
    public GameObject pointerGun;
    private bool firstPerson = false;

    private void Update() {
        SwitchCamera();
    }
    
   private void SwitchCamera(){
        if (Input.GetMouseButtonDown(1)) {
            SetFirstPerson(true);
            pointerGun.SetActive(true);
            DoSwitchCamera();
        } else if (Input.GetMouseButtonUp(1)) {
            SetFirstPerson(false);
            DoSwitchCamera();
            pointerGun.SetActive(false);
        }
    } 

    public void SetFirstPerson(bool state) {
        firstPerson = state;
    }

    public bool GetFirstPerson() {
        return firstPerson;
    }

    public void DoSwitchCamera(){
        if (firstPerson) {
            cameraFirstPerson.SetActive(true);
            cameraThirdPerson.SetActive(false);
            playerMove.ChangeFirstPerson(true);
        } else {
            cameraFirstPerson.SetActive(false);
            cameraThirdPerson.SetActive(true);
            playerMove.ChangeFirstPerson(false);
        }
    }
}
