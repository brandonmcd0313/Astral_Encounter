using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    //spawns two random powerups in slot one and slot two
    //NOT the same powerup fr fr

    [SerializeField] Vector2 pos1;
    [SerializeField] Vector2 pos2;
    [SerializeField] List<Image> powerupPrefabs;
    [SerializeField] Transform canvas;
    // Start is called before the first frame update
    void Start()
    {
       List<Image> localPowerups = new List<Image>();
            Image p1, p2;
        //recreate arraylist locally so i can mess with it
        localPowerups = powerupPrefabs;
        
        p1 = localPowerups[Random.Range(0,localPowerups.Count())];

        localPowerups.Remove(p1);

        p2= localPowerups[Random.Range(0, localPowerups.Count())];

        //make them happen!
        // Instantiate the image
        Image newImage = Instantiate(p1);

        // Get the current scale of the image
        Vector3 currentScale = newImage.rectTransform.localScale;

        // Set the image as a child of the canvas
        newImage.transform.SetParent(canvas);

        // Get the current scale of the image
        currentScale = new Vector3(1,1,1);

        // Set the position of the image
        newImage.rectTransform.localPosition = pos1;

        // Set the scale of the image back to its original value
        newImage.rectTransform.localScale = currentScale;

        // Instantiate the image
        newImage = Instantiate(p2);

        // Set the image as a child of the canvas
        newImage.transform.SetParent(canvas);

        // Set the position of the image
        newImage.rectTransform.localPosition = pos2;

        // Set the scale of the image back to its original value
        newImage.rectTransform.localScale = currentScale;


    }

}
