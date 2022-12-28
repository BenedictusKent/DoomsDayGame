using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonExtension : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public float pressDurationTime = 1f;
    public bool responseOnceByPress = false;
    public float doubleClickIntervalTime = 0.5f;

    public UnityEvent onDoubleClick;
    public UnityEvent onPress;
    public UnityEvent onClick;

    private bool isDown = false;
    private bool isPress = false;
    private float downTime = 0;

    private float clickIntervalTime = 0;
    private int clickTimes = 0;

    // Update is called once per frame
    void Update()
    {
        if(isDown)
        {
            if(responseOnceByPress && isPress)
            {
                return;
            }
            downTime += Time.deltaTime;
            if(downTime > pressDurationTime)
            {
                isPress = true;
                onPress.Invoke();
            }
        }

        if(clickTimes >= 1)
        {
            clickIntervalTime += Time.deltaTime;
            if(clickIntervalTime >= doubleClickIntervalTime)
            {
                if(clickTimes >= 2)
                {
                    onDoubleClick.Invoke();
                }
                else
                {
                    onClick.Invoke();
                }
                clickTimes = 0;
                clickIntervalTime = 0;
            }
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if(!isPress){
            clickTimes++;
        }
        else{
            isPress = false;
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        downTime = 0;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        isDown = false;
        isPress = false;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
    }
}
