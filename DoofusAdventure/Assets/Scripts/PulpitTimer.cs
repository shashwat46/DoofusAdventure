using UnityEngine;
using TMPro;

public class PulpitTimer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private float destroyTime;

    void Start()
{
    Debug.Log("PulpitTimer script initialized.");
}

    public void Initialize(float time)
    {
        destroyTime = time;
        timerText = GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(timerText != null ? "TimerText assigned!" : "TimerText is null");
    }

    void Update()
    {
        if (destroyTime > 0)
        {
            destroyTime -= Time.deltaTime;
            UpdateTimerDisplay();
            Debug.Log("Update is being called. DestroyTime: " + destroyTime);
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = destroyTime.ToString("F1");
        }
    }
}