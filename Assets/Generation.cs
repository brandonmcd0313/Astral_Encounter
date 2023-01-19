using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class Generation : MonoBehaviour
{
    [SerializeField] GameObject planetPrefab;
    private static GameObject planetPre;

    private void Start()
    {
        planetPre = planetPrefab;
    }
    // Returns a random color from the rainbow
    public static Color GetRandomColor()
    {
        Color[] rainbowColors = {
        Color.red,
        new Color(1f, 0.5f, 0f), // orange
        Color.yellow,
        Color.green,
        Color.blue,
        new Color(75f/255f, 0f, 130f/255f), // indigo
        new Color(148f/255f, 0f, 211f/255f) // violet
        };
        int index1 = Random.Range(0, rainbowColors.Length);
        Color[] primary = {
        Color.red,
        Color.yellow,
        Color.blue,
          Color.red,
        Color.yellow,
        Color.blue,
          Color.red,
        Color.yellow,
        Color.blue,
              rainbowColors[index1]
        };
        
        int index2 = Random.Range(0, primary.Length);

        // Blend the two colors using Lerp
        float blendAmount = Random.Range(0f, 0.5f);
        return Color.Lerp(rainbowColors[index1], primary[index2], blendAmount);
    }

    public static void newZone(GameObject previous, GameObject player)
    {
        int quadrant = 0; int x = 0;
        //determine the direction to place the new zone based on player and previous
        //background is 39x39
        //will place THREE new zones in the corner of the player
        //then a PLANET is placed in that zone

        Vector2 refrence = previous.transform.position;
        Vector2 placement = player.transform.position;

        //assign a quadrant for use in spawning the backgrouds!
        //x is positive
        if (placement.x - refrence.x > 0)
        {
            x = 1;
        }
        //x is negative
        else
        {
            x = 2;
        }

        //y is positive
        if (placement.y - refrence.y > 0)
        {
            if (x == 1) { quadrant = 1; }
            else if (x == 2) { quadrant = 2; }
        }
        else
        {
            if (x == 1) { quadrant = 4; }
            else if (x == 2) { quadrant = 3; }
        }
        bool check = true;
        //random rotation to make it seem more random
        Quaternion randRot = Quaternion.identity;
        randRot.z = 90 * Random.Range(1, 100);
        //up is 1 and 2
        if (quadrant == 1 || quadrant == 2)
        {
            Vector3 newPos = new Vector3(refrence.x, refrence.y + 60, 0);
            if (!BackgroundExistsAtPosition(newPos))
            {
                check = false;
                Instantiate(previous, newPos, randRot);
            }
        }
        randRot.z = 90 * Random.Range(1, 100);
        //left is 2 and 3
        if (quadrant == 2 || quadrant == 3)
        {
            Vector3 newPos = new Vector3(refrence.x - 60, refrence.y, 0);
            if (!BackgroundExistsAtPosition(newPos))
            {
                check = false;
                Instantiate(previous, newPos, randRot);
            }
        }
        randRot.z = 90 * Random.Range(1, 100);
        //down is 3 and 4
        if (quadrant == 3 || quadrant == 4)
        {
            Vector3 newPos = new Vector3(refrence.x, refrence.y - 60, 0);
            if (!BackgroundExistsAtPosition(newPos))
            {
                check = false;
                Instantiate(previous, newPos, randRot);
            }
        }
        randRot.z = 90 * Random.Range(1, 100);
        //right is 1 and 4
        if (quadrant == 1 || quadrant == 4)
        {
            Vector3 newPos = new Vector3(refrence.x + 60, refrence.y, 0);
            if (!BackgroundExistsAtPosition(newPos))
            {
                check = false;
                Instantiate(previous, newPos, randRot);
            }
        }
        randRot.z = 90 * Random.Range(1, 100);
        //corners!
        switch (quadrant)
        {
            case 1:
                {
                    //top right
                    Vector3 newPos = new Vector3(refrence.x + 60, refrence.y + 60, 0);
                    if (!BackgroundExistsAtPosition(newPos))
                    {
                        check = false;
                        Instantiate(previous, newPos, randRot);
                    }
                    return;
                }
            case 2:
                {
                    //top left
                    Vector3 newPos = new Vector3(refrence.x - 60, refrence.y + 60, 0);
                    if (!BackgroundExistsAtPosition(newPos))
                    {
                        check = false;
                        Instantiate(previous, newPos, randRot);
                    }
                    return;
                }
                case 3:
                {
                    //bottom left
                    Vector3 newPos = new Vector3(refrence.x - 60, refrence.y - 60, 0);
                    if (!BackgroundExistsAtPosition(newPos))
                    {
                        check = false;
                        Instantiate(previous, newPos, randRot);
                    }
                    return;
                }
                case 4:
                {
                    //bottom right
                    Vector3 newPos = new Vector3(refrence.x + 60, refrence.y - 60, 0);
                    if (!BackgroundExistsAtPosition(newPos))
                    {
                        check = false;
                        Instantiate(previous, newPos, randRot);
                    }
                    return;
                }
        }
        if (check) { return; }
        //10% of a new planet spawning
        int odds = Random.Range(1, 10);

       if(odds == 7)
        {
            //instaniate a planet!
            GameObject planet = Instantiate(planetPre);
            //set colour (Fun) 
            planet.GetComponent<SpriteRenderer>().color = GetRandomColor();

            //spawn in sort of the directon the player is facing

            // Get the direction that the player is facing
            Vector3 playerDirection = player.transform.up;

            // Generate a random angle between -60 and 60 degrees
            float angle = Random.Range(-60f, 60f);

            // Rotate the player direction by the random angle
            Vector3 planetDirection = Quaternion.Euler(0, angle, 0) * playerDirection;

            // Set the position of the planet to be 30-100 units in the direction the player is facing
            planet.transform.position = player.transform.position + planetDirection * Random.Range(30f, 100f);
            planet.transform.localPosition = new Vector3( planet.transform.localPosition.x, planet.transform.localPosition.y, 0);

            // Create a BoxCollider2D for the 50x50 area
            BoxCollider2D boxCollider = new GameObject().AddComponent<BoxCollider2D>();
            boxCollider.size = new Vector2(50, 50);

            // Set the position of the BoxCollider2D to be the same as the new planet
            boxCollider.transform.position = planet.transform.position;

            // Use OverlapBox to check if any colliders overlap with the BoxCollider2D
            Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCollider.transform.position, boxCollider.size, 0);

            // Filter the colliders by tag
            List<Collider2D> filteredColliders = new List<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.tag == "Planet")
                {
                    filteredColliders.Add(collider);
                }
            }

            // If colliders with the "Planet" tag were found, do something (e.g. move the planet somewhere else)
            if (filteredColliders.Count > 0)
            {
                //destroy the planet
                Destroy(planet);
            }

            // Destroy the BoxCollider2D (we don't need it anymore)
            Destroy(boxCollider.gameObject);
        }

        //set new planet for compass
        Compass.compassSetter();
    }
    
    private static bool BackgroundExistsAtPosition(Vector3 position)
    {
        // Find all game objects with the "Background" tag
        GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("Background");

        // Iterate through each background object
        foreach (GameObject background in backgrounds)
        {
            // Check if the position of the background object matches the given position
    if (background.transform.position == position)
            {
                // A background object already exists at the given position
                return true;
            }
        }
    // No background object was found at the given position
return false;
    }


    public static void newZone(GameObject previous, GameObject player, int i)
    {
        //the int is just to overload it
        //this one ALWAYS SPAWNS PLANET

        int quadrant = 0; int x = 0;
        //determine the direction to place the new zone based on player and previous
        //background is 39x39
        //will place THREE new zones in the corner of the player
        //then a PLANET is placed in that zone

        Vector2 refrence = previous.transform.position;
        Vector2 placement = player.transform.position;

        //assign a quadrant for use in spawning the backgrouds!
        //x is positive
        if (placement.x - refrence.x > 0)
        {
            x = 1;
        }
        //x is negative
        else
        {
            x = 2;
        }

        //y is positive
        if (placement.y - refrence.y > 0)
        {
            if (x == 1) { quadrant = 1; }
            else if (x == 2) { quadrant = 2; }
        }
        else
        {
            if (x == 1) { quadrant = 4; }
            else if (x == 2) { quadrant = 3; }
        }
        bool check = true;
        //random rotation to make it seem more random
        Quaternion randRot = Quaternion.identity;
        randRot.z = 90 * Random.Range(1, 100);
        //up is 1 and 2
        if (quadrant == 1 || quadrant == 2)
        {
            Vector3 newPos = new Vector3(refrence.x, refrence.y + 60, 0);
            if (!BackgroundExistsAtPosition(newPos))
            {
                check = false;
                Instantiate(previous, newPos, randRot);
            }
        }
        //left is 2 and 3
        if (quadrant == 2 || quadrant == 3)
        {
            Vector3 newPos = new Vector3(refrence.x - 60, refrence.y, 0);
            if (!BackgroundExistsAtPosition(newPos))
            {
                check = false;
                Instantiate(previous, newPos, randRot);
            }
        }
        //down is 3 and 4
        if (quadrant == 3 || quadrant == 4)
        {
            Vector3 newPos = new Vector3(refrence.x, refrence.y - 60, 0);
            if (!BackgroundExistsAtPosition(newPos))
            {
                check = false;
                Instantiate(previous, newPos, randRot);
            }
        }
        //right is 1 and 4
        if (quadrant == 1 || quadrant == 4)
        {
            Vector3 newPos = new Vector3(refrence.x + 60, refrence.y, 0);
            if (!BackgroundExistsAtPosition(newPos))
            {
                check = false;
                Instantiate(previous, newPos, randRot);
            }
        }
        if (check) { return; }
        //10% of a new planet spawning
        int odds = 7;

        if (odds == 7)
        {
            //instaniate a planet!
            GameObject planet = Instantiate(planetPre);
            //set colour (Fun) 
            planet.GetComponent<SpriteRenderer>().color = GetRandomColor();

            //spawn in sort of the directon the player is facing

            // Get the direction that the player is facing
            Vector3 playerDirection = player.transform.up;

            // Generate a random angle between -360 and 360 degrees
            float angle = Random.Range(-360f, 360f);

            // Rotate the player direction by the random angle
            Vector3 planetDirection = Quaternion.Euler(0, angle, 0) * playerDirection;

            // Set the position of the planet to be 30-100 units in the direction the player is facing
           

            planet.transform.position = player.transform.position + planetDirection * Random.Range(100f, 250f);
            planet.transform.localPosition = new Vector3(planet.transform.localPosition.x, planet.transform.localPosition.y, 0);

            // Create a BoxCollider2D for the 50x50 area
            BoxCollider2D boxCollider = new GameObject().AddComponent<BoxCollider2D>();
            boxCollider.size = new Vector2(50, 50);

            // Set the position of the BoxCollider2D to be the same as the new planet
            boxCollider.transform.position = planet.transform.position;

            // Use OverlapBox to check if any colliders overlap with the BoxCollider2D
            Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCollider.transform.position, boxCollider.size, 0);

            // Filter the colliders by tag
            List<Collider2D> filteredColliders = new List<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.tag == "Planet")
                {
                    filteredColliders.Add(collider);
                }
            }

            // If colliders with the "Planet" tag were found, do something (e.g. move the planet somewhere else)
            if (filteredColliders.Count > 0)
            {
                //destroy the planet
                Destroy(planet);
            }

            // Destroy the BoxCollider2D (we don't need it anymore)
            Destroy(boxCollider.gameObject);
        }

        //set new planet for compass
        Compass.compassSetter();
    }
}
