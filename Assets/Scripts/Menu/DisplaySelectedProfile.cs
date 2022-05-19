using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplaySelectedProfile : MonoBehaviour
{
    private const string selectedProfileFormat = "You have selected profile {0}";

    private Text selectedProfileText;

    public void UpdateSelectedProfile(Profile selectedProfile)
    {
        selectedProfileText.text = string.Format(selectedProfileFormat, selectedProfile.ProfileID);
    }

    private void Awake()
    {
        selectedProfileText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        GameManager.OnProfileChanged += UpdateSelectedProfile;
    }

    private void OnDisable()
    {
        GameManager.OnProfileChanged -= UpdateSelectedProfile;
    }

    private void Start()
    {
        UpdateSelectedProfile(GameManager.SelectedProfile);
    }
}
