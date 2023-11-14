using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Image backgroundImage;
    private Image joystickImage;
    public float offset;
    public Vector2 InputDirection { set; get; }

    private void Start()
    {
        backgroundImage = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
        Debug.Log(backgroundImage);
        Debug.Log(joystickImage);
        InputDirection = Vector2.zero; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;
        float bgImageSizeX = backgroundImage.rectTransform.sizeDelta.x;
        float bgImageSizeY = backgroundImage.rectTransform.sizeDelta.y;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImage.rectTransform,eventData.position,eventData.pressEventCamera, out pos))
        {
            pos.x /= bgImageSizeX;
            pos.y /= bgImageSizeY;
            InputDirection = new Vector2(pos.x, pos.y);
            InputDirection = InputDirection.magnitude > 1 ? InputDirection.normalized : InputDirection;
            
            
            joystickImage.rectTransform.anchoredPosition = new Vector2(InputDirection.x*(bgImageSizeX/offset), InputDirection.y*(bgImageSizeY/offset));
           // Debug.Log(InputDirection); 
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputDirection = Vector2.zero;
        joystickImage.rectTransform.anchoredPosition = Vector2.zero; 
    }
}
