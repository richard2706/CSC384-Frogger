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

    public void Select()
    {
        ColorBlock colors = button.colors;
        colors.normalColor = selectedColor;
        button.colors = colors;
        buttonText.color = Color.white;
    }

    public void Deselect()
    {
        ColorBlock colors = button.colors;
        colors.normalColor = deselectedColor;
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
                else Deselect();
            }
        }
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<Text>();
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
}
