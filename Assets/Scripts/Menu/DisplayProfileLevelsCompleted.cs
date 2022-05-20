using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayProfileLevelsCompleted : MonoBehaviour
{
    private const string levelsCompletedFormat = "{0} levels completed";
    private const string unknownLevelsCompletedText = "Empty profile";

    private Text levelsCompletedText;

    private void Awake()
    {
        levelsCompletedText = GetComponent<Text>();
    }

    public void DisplayLevelsCompleted(Profile profile)
    {
        levelsCompletedText.text = profile != null
            ? string.Format(levelsCompletedFormat, profile.LevelsCompleted)
            : unknownLevelsCompletedText;
    }
}
