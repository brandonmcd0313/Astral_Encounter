using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //camera moves with player
    public GameObject player;
    GameObject background;
    Camera camera; GameObject prev;
    // Start is called before the first frame update
    void Start()
    {

        camera = GetComponent<Camera>();
        // Get the bounds of the camera's view frustum in world space


        //set the background
        background = GetClosestBackground();
        InvokeRepeating("ReassignBackground", 0, 1f);

        //generate a new section
        Generation.newZone(background, this.gameObject, 0);
    }
    void ReassignBackground()
    {
        // This method will be called once per second.

        //set the background
        background = GetClosestBackground();
    }
    // Update is called once per frame
    void Update()
    {

        // Get the top-left corner of the camera's viewport
        Vector2 topLeft = camera.ViewportToWorldPoint(new Vector3(0, 1, camera.nearClipPlane));

        // Get the bottom-right corner of the camera's viewport
        Vector2 bottomRight = camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane));

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -15);
        //if the camera space is no longer in the background.

        bool cameraContained = (background.GetComponent<BoxCollider2D>().bounds.Contains(topLeft))
            && (background.GetComponent<BoxCollider2D>().bounds.Contains(bottomRight));
            if (!cameraContained && (prev == null))
                {
                      prev = background;
                      print("exitted00");
                      //generate a new section
                      Generation.newZone(background, this.gameObject);
                 }
            else if(!cameraContained && (prev != null))
        {
            //check and make sure you can SEE backround, otherwise reassign backround
            Bounds combinedBounds = prev.GetComponent<BoxCollider2D>().bounds;
            combinedBounds.Encapsulate(background.GetComponent<BoxCollider2D>().bounds);
            bool cameraContained2 = (combinedBounds.Contains(topLeft))
           && (combinedBounds.Contains(bottomRight));
             if(!cameraContained2)
            {
                ReassignBackground();
                Generation.newZone(background, this.gameObject);
            }
        }
        else if(prev != background)
               {
                   prev = null;
               }
    }

    public GameObject GetClosestBackground()
    {
        // Find all game objects with the "Background" tag
        GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("Background");

        // Set the initial closest distance to a high value
        float closestDistance = Mathf.Infinity;

        // Set the initial closest background to null
        GameObject closestBackground = null;

        // Iterate through each background
        foreach (GameObject background in backgrounds)
        {
            // Get the distance between the current game object and the background
            float distance = Vector3.Distance(transform.position, background.transform.position);

            // Check if the distance is less than the current closest distance
            if (distance < closestDistance)
            {
                // Set the new closest distance
                closestDistance = distance;

                // Set the new closest background
                closestBackground = background;
            }
        }

        // Return the closest background
        return closestBackground;
    }

    public GameObject GetClosestBackground(GameObject current)
    {
        // Find all game objects with the "Background" tag
        GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("Background");

        // Set the initial closest distance to a high value
        float closestDistance = Mathf.Infinity;

        // Set the initial closest background to null
        GameObject closestBackground = null;

        // Iterate through each background
        foreach (GameObject background in backgrounds)
        {
            // Get the distance between the current game object and the background
            float distance = Vector3.Distance(transform.position, background.transform.position);

            // Check if the distance is less than the current closest distance
            if (distance < closestDistance && background != current)
            {
                // Set the new closest distance
                closestDistance = distance;

                // Set the new closest background
                closestBackground = background;
            }
        }

        // Return the closest background
        return closestBackground;
    }
}
