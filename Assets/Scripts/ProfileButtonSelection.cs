using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ProfileButtonSelection : MonoBehaviour
{
    [SerializeField] private Color deselectedColor;
    [SerializeField] private Color selectedColor;

    private Image buttonBackground;

    public void Select()
    {
        buttonBackground.color = selectedColor;
    }

    public void Deselect()
    {
        buttonBackground.color = deselectedColor;
    }

    private void Awake()
    {
        buttonBackground = GetComponent<Image>();
        Deselect();
    }
}
