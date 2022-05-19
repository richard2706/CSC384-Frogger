using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimerUI : MonoBehaviour
{
    private const string timerTextFormat = "{0} seconds";

    [SerializeField] private Timer timer;

    private Text timerText;

    private void Awake()
    {
        timerText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        Timer.OnTimerUpdate += UpdateTimeText;
    }

    private void OnDisable()
    {
        Timer.OnTimerUpdate -= UpdateTimeText;
    }

    private void UpdateTimeText(Timer timer)
    {
        if (this.timer = timer)
        {
            timerText.text = string.Format(timerTextFormat, timer.TimeRemaining.ToString());
        }
    }
}
