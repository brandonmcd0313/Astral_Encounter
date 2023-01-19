using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverTipManager : MonoBehaviour
{
    [Tooltip("The Text object for the window")]
    public Text tipText;
    public RectTransform tipWindow;
    public Canvas parentCanvas; public Camera mainCam;
    //string = message, vector2 = position
    public static Action<string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;
    [SerializeField] Vector2 offset;
    // Use this for initialization
    bool active;
    void Start()
    {
        HideTip();
        active = false;
    }

    void Update()
    {
        if (active)
        {
            ShowTip();
        }
    }
    void OnEnable()
    {
        //when enabled subscribe methods
        //this makes it so in other classes these actions refrence this class
        OnMouseHover += ShowTip;
        OnMouseLoseFocus += HideTip;

    }

    void OnDisable()
    {
        //unsubcribe methods
        OnMouseHover -= ShowTip;
        OnMouseLoseFocus -= HideTip;
    }
    private void ShowTip(string tip, Vector2 mousePos)
    {
        
        if (!active)
        {
            active = true;
        }
        print(mousePos);
        print(Camera.main.ScreenToWorldPoint(mousePos));
        tipText.text = tip;
        //scale backround to fit text
        //using ternary operators to make things easier
        //tipWindow.sizeDelta = new Vector2(tipText.preferredWidth > 160 ? 160 : tipText.preferredWidth, tipText.preferredHeight);

        tipWindow.gameObject.SetActive(true);
        //vector 2 of nneded window pos
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Input.mousePosition, mainCam,
        out pos);
        //print("pos: " + pos);
        Vector2 shifted = pos + offset;
        tipWindow.transform.localPosition = shifted;
        //keep the window on the screen 
        /*
        if (pos.y > 128)
        {
            tipWindow.transform.localPosition = new Vector2(pos.x, 128);

        }
        if (pos.x > 191)
        {
            //make tip window appear on left side of mouse
            tipWindow.transform.localPosition -= new Vector3(tipWindow.sizeDelta.x, 0);
        }
        */
        //fix the child position
        tipText.transform.localPosition = new Vector3(10, 0, 0);
    }

    private void ShowTip()
    {
        //local showtip that is used to have tooltip follow the mouse
        //vector 2 of nneded window pos
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Input.mousePosition, mainCam,
        out pos);
        //print("pos: " + pos);
        Vector2 shifted = pos + offset;
        tipWindow.transform.localPosition = shifted;
        //keep the window on the screen 
        /*
        if (pos.y > 128)
        {
            tipWindow.transform.localPosition = new Vector2(pos.x, 128);

        }
        if (pos.x > 200)
        {
            //make tip window appear on left side of mouse
            tipWindow.transform.localPosition -= new Vector3(tipWindow.sizeDelta.x, 0);
        }
        */
    }

    private void HideTip()
    {
        //hide the tooltip, null message and disable window
        tipText.text = null;
        tipWindow.gameObject.SetActive(false);
        active = false;
    }

}