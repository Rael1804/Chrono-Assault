
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public float time;
    [SerializeField] private Text timeText;
    private  float lastUpdate = 0f;


    private void Start() {
        timeText.text = FormatTime(time);
    }

    public  void SetTime() {
        time = 0f;

    }

    public void Update() {
        float currentTime = Time.time;  
        if (currentTime - lastUpdate >= 1f) {
            time += 1f;
            lastUpdate = currentTime;
            timeText.text = FormatTime(time);
        } 
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public string getTime()
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
