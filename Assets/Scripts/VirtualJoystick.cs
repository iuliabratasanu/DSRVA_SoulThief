using UnityEngine.EventSystems;
using UnityEngine;


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bgImage;
    private Image joystickImage;
    private Vector2 inputVector; // joystick position

    private void Start()
    {
        bgImage = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;

        // screen position conversion
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform,
                                                                    ped.position,
                                                                    ped.pressEventCamera,
                                                                    out pos))
        {
            // normalize position
            pos.x = (pos.x / bgImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImage.rectTransform.sizeDelta.y);

            // get values from -1 to 1
            inputVector = new Vector2(pos.x * 2, pos.y * 2);

            // normalize input
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            // joystick position
            //                              (0,1) = top
            //          (-1,0) = left       (0,0) = center       (1,0) = right
            //                              (0,-1) = bottom

            joystickImage.rectTransform.anchoredPosition =
                new Vector2(inputVector.x * (bgImage.rectTransform.sizeDelta.x / 2),
                            inputVector.y * (bgImage.rectTransform.sizeDelta.y / 2));
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        joystickImage.rectTransform.anchoredPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        return inputVector.x;
    }

    public float Vertical()
    {
        return inputVector.y;
    }
}