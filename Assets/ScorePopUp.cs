using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

public class ScorePopUp : MonoBehaviour
{
    public int value;
    TextMesh tm; Color colour;
    // Use this for initialization
    void Start()
    {
        //render above all else
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.sortingLayerID = SortingLayer.NameToID("UI");
        //set Text Mesh
        tm = this.GetComponent<TextMesh>();


        //color the object accordingly
        if (value > 0)
        {
            tm.color = Color.green;
            tm.text = "+" + value;
        }
        else
        {
            tm.color = Color.red;
            tm.text = "" + value;
        }
        colour = tm.color;
        StartCoroutine(fade());
    }
    public void setVal(int val)
    {
        value = val;
    }
    IEnumerator fade()
    {

        while (tm.color.a >= 0.01)
        {

            //alpha value -a
            tm.color = colour;
            colour.a -= 0.01f;
            transform.localPosition = new Vector3(transform.position.x, transform.position.y + 0.01f, -1);
            yield return new WaitForSeconds(0.005f);
        }
    }

}