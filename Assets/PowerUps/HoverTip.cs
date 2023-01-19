using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string itemName;
    [SerializeField] string itemDesc;
    [SerializeField] string itemAbility;
    [SerializeField] int itemNum;
    private string tipToShow;
    private float timeToWait = 0.5f;

    //this script is on every button
    public void Start()
    {
        //set tipToShow
        tipToShow = itemName + "!\n\n" + itemDesc + "\n\n" + itemAbility;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("on pointer enter");
        //on hover over object
        StopAllCoroutines();
        StartCoroutine(StartTimer());

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("on pointer exit");
        //on hover exit
        StopAllCoroutines();
        HoverTipManager.OnMouseLoseFocus();
    }

    private void ShowMessage()
    {
        HoverTipManager.OnMouseHover(tipToShow, Input.mousePosition);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);

        ShowMessage();
    }

    public void OnClick()
    {
        //do the action that this object does!

        switch (itemNum)
        {
            case 0: //decrease acelTime
                return;
            case 1: //increase max speed
                return;
            case 2: //decrease asteroid damage / inc asteroid points
                return;
            case 3: //decrease asteroid spawn rate
                return;
            case 4: //add min to timer
                return;
            case 5: //pause timer for one min
                return;
            case 6: //point buff 0 to 1000
                return;

        }
    }
}
