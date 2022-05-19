using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ProfileButton : MonoBehaviour
{
    [SerializeField] private Color deselectedColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private int profileID;

    private Button button;
    private Text buttonText;

    public void Select()
    {
        Debug.Log("Select profile " + profileID);
        ColorBlock colors = button.colors;
        colors.normalColor = selectedColor;
        button.colors = colors;
        buttonText.color = Color.white;
    }

    public void Deselect()
    {
        Debug.Log("Deselect profile " + profileID);
        ColorBlock colors = button.colors;
        colors.normalColor = deselectedColor;
        button.colors = colors;
        buttonText.color = Color.black;
    }

    public void Click()
    {
        Debug.Log("Profile " + profileID);
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<Text>();
        Deselect();
    }

    private void Start()
    {
        if (GameManager.SelectedProfile.ProfileID == profileID) Select();
    }
}
