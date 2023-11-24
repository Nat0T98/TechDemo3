using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThumbStickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Image Border;
    private Image ThumbCenter;
    public float offset;
    public Vector2 InputDirection { set; get; }

    private void Start()
    {
        Border = GetComponent<Image>();
        ThumbCenter = transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector2.zero; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;
        float BorderSizeX = Border.rectTransform.sizeDelta.x;
        float BorderSizeY = Border.rectTransform.sizeDelta.y;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(Border.rectTransform,eventData.position,eventData.pressEventCamera, out pos))
        {
            pos.x /= BorderSizeX;
            pos.y /= BorderSizeY;
            InputDirection = new Vector2(pos.x, pos.y);
            InputDirection = InputDirection.magnitude > 1 ? InputDirection.normalized : InputDirection;           
            ThumbCenter.rectTransform.anchoredPosition = new Vector2(InputDirection.x*(BorderSizeX/offset), InputDirection.y*(BorderSizeY/offset));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputDirection = Vector2.zero;
        ThumbCenter.rectTransform.anchoredPosition = Vector2.zero; 
    }
}
