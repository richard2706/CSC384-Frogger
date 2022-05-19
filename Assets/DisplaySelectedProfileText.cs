using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplaySelectedProfileText : MonoBehaviour
{
    private const string selectedProfileFormat = "You have selected profile {0}";

    private Text selectedProfileText;

    public void UpdateSelectedProfile()
    {
        int selectedProfileID = GameManager.SelectedProfile.ProfileID;
        selectedProfileText.text = string.Format(selectedProfileFormat, selectedProfileID);
    }

    private void Awake()
    {
        selectedProfileText = GetComponent<Text>();
    }

    private void Start()
    {
        UpdateSelectedProfile();
    }
}
