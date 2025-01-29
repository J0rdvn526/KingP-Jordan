using TMPro;
using UnityEngine;

public class TimerBehaviour : MonoBehaviour
{
    private float timer;
    private TextMeshProUGUI textField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        if (textField == null)
        {
            Debug.Log("Null component found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.time;

        int minutes = (int)timer / 60;
        int seconds = (int)timer % 60;

        string message = string.Format("<color=white>Time: <color=#4EFCEB>{00:00}<color=white>:<color=#4EFCEB>{1:00}", minutes, seconds);

        textField.text = message;



        // Debug.Log(timer);
    }
}
