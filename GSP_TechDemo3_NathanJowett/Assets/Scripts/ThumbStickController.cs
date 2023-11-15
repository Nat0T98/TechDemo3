using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThumbStickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Image Background;
    private Image ThumbStickImage;
    public float offset;
    public Vector2 InputDirection { set; get; }

    private void Start()
    {
        Background = GetComponent<Image>();
        ThumbStickImage = transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector2.zero; 
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;
        float bgImageSizeX = Background.rectTransform.sizeDelta.x;
        float bgImageSizeY = Background.rectTransform.sizeDelta.y;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(Background.rectTransform,eventData.position,eventData.pressEventCamera, out pos))
        {
            pos.x /= bgImageSizeX;
            pos.y /= bgImageSizeY;
            InputDirection = new Vector2(pos.x, pos.y);
            InputDirection = InputDirection.magnitude > 1 ? InputDirection.normalized : InputDirection;      
            ThumbStickImage.rectTransform.anchoredPosition = new Vector2(InputDirection.x*(bgImageSizeX/offset), InputDirection.y*(bgImageSizeY/offset));
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
       OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        InputDirection = Vector2.zero;
        ThumbStickImage.rectTransform.anchoredPosition = Vector2.zero; 
    }
}
