using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ProfileButton : MonoBehaviour
{
    private static List<ProfileButton> allProfileButtons = new List<ProfileButton>();

    [SerializeField] private Color deselectedColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private int profileID;

    private Button button;
    private Text buttonText;
    private DisplayProfileDetails levelsCompletedText;

    public void Select()
    {
        ColorBlock colors = button.colors;
        colors.normalColor = selectedColor;
        colors.selectedColor = selectedColor;
        button.colors = colors;
        buttonText.color = Color.white;
    }

    public void Deselect()
    {
        ColorBlock colors = button.colors;
        colors.normalColor = deselectedColor;
        colors.selectedColor = deselectedColor;
        button.colors = colors;
        buttonText.color = Color.black;
    }

    public void Click()
    {
        if (GameManager.Instance.SelectProfile(profileID))
        {
            foreach (ProfileButton profileButton in allProfileButtons)
            {
                if (profileButton == this) Select();
                else profileButton.Deselect();
            }
        }
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<Text>();
        levelsCompletedText = GetComponentInChildren<DisplayProfileDetails>();
        DisplayProfileInfo();
        Deselect();
    }

    private void OnEnable()
    {
        allProfileButtons.Add(this);
    }

    private void OnDisable()
    {
        allProfileButtons.Remove(this);
    }

    private void Start()
    {
        if (GameManager.SelectedProfile.ProfileID == profileID) Select();
    }

    private void DisplayProfileInfo()
    {
        Profile profile = SaveManager.LoadProfile(profileID);
        levelsCompletedText.DisplayLevelsCompleted(profile);
    }
}
