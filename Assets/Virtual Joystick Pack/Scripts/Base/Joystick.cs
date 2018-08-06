using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    //[Header("Options")]
    //[Range(0f, 2f)] public float handleLimit = 1f;

    //[Header("Components")]
    ////public RectTransform background;
    ////public RectTransform handle;
    public Image bgImg;
    public Image joystickimg;
    private Vector3 inVector;
    private RectTransform canvas;
    private RectTransform jump;
    private RectTransform MenuCtrl;

    //public float Horizontal { get { return inVector.x; } }
    //public float Vertical { get { return inVector.y; } }

    private void Start()
    {
        MenuCtrl = GameObject.Find("MenuCtrl").GetComponent<RectTransform>();
        jump = GameObject.Find("Jump").GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        //bgImg = GetComponent<Image>();
        //joystickimg = transform.GetChild(0).GetComponent<Image>();
        //Rect r = bgImg.rectTransform.rect;
        //bgImg.rectTransform.position = new Vector3((canvas.localScale.x + 0.05f) * r.width, (r.height * 0.05f));
        //jump.position = new Vector3(Screen.width - (jump.rect.width * 0.05f), jump.rect.height * 0.05f);
        //Debug.Log("WIDTH: " + canvas.rect.width);
        //Debug.Log("HEIGHT: " + canvas.rect.height);
        bgImg.rectTransform.sizeDelta = new Vector2(canvas.rect.width * 0.2f, canvas.rect.width * 0.2f);
        joystickimg.rectTransform.sizeDelta = new Vector2(canvas.rect.width * 0.1f, canvas.rect.width * 0.1f);
        jump.sizeDelta = new Vector2(canvas.rect.width * 0.22f, canvas.rect.width * 0.22f);
        MenuCtrl.sizeDelta = new Vector2(canvas.rect.width * 0.06f, canvas.rect.width * 0.06f);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            //bgImg.rectTransform.sizeDelta.x (positive) for bottom right joystick placement
            pos.x = (pos.x / -bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);
            inVector = new Vector3(pos.x*2 + 1, 0, pos.y*2 - 1);
            inVector = (inVector.magnitude > 1.0f) ? inVector.normalized : inVector;
            joystickimg.rectTransform.anchoredPosition = 
                new Vector3(inVector.x * (-bgImg.rectTransform.sizeDelta.x / 2), 
                            inVector.z * (bgImg.rectTransform.sizeDelta.y / 2));
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        inVector = Vector3.zero;
        joystickimg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal() {
        if (inVector.x != 0)
            return inVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inVector.z != 0)
            return inVector.z;
        else
            return Input.GetAxis("Vertical");
    }
}
