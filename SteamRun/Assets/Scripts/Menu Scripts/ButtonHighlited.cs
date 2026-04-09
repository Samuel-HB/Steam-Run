using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHighlited : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI buttonText;
    private Color originalColor;

    private void Start()
    {
        originalColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = new Color(230, 220, 175);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = originalColor;
    }

    //public void OnMouseEnter()
    //{
    //    buttonText.color = new Color(230, 220, 175);
    //}

    //public void OnMouseExit()
    //{
    //    buttonText.color = originalColor;
    //}
}
