using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Compass : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Transform nearestPlanet; // The nearest planet
    [SerializeField] float offset;
    [SerializeField] float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //default value is randomly assigned
        float randomX = Random.Range(200f, 300f);
        float randomY = Random.Range(200f, 300f);
        randomX *= Mathf.Sign(Random.Range(-1, 1));
        randomY *= Mathf.Sign(Random.Range(-1, 1));
        GameObject sample = Instantiate(new GameObject(), new Vector3(randomX, randomY, 0), Quaternion.identity);
        nearestPlanet = sample.GetComponent<Transform>();
        InvokeRepeating("setNearestPlanet", 0.2f, 1.75f);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction from the player to the nearest planet
        Vector3 direction = nearestPlanet.position - player.transform.position;

        // Calculate the angle of the direction vector in the x-y plane
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Smoothly rotate the compass towards the direction of the nearest planet
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle + offset), Time.deltaTime * rotationSpeed);
    }

    public static void compassSetter()
    {
        GameObject.Find("compass_arrow").GetComponent<Compass>().setNearestPlanet();
    }
    void setNearestPlanet()
    {
        // Find all objects with the "Planet" tag
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

        if (!(planets.Count() > 0))
        {
            return;
        }
        // Set the nearest planet to be the first planet in the array (for now)
        nearestPlanet = planets[0].transform;

        // Find the nearest planet
        foreach (GameObject planet in planets)
        {
            if (Vector3.Distance(transform.position, planet.transform.position) < Vector3.Distance(transform.position, nearestPlanet.position))
            {
                nearestPlanet = planet.transform;
            }
        }

    }
}

