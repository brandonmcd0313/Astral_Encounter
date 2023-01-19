using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject asteroid;
    [SerializeField] Sprite[] astSprites;
    public float initAsteroidSpawnRate;
    public static float AsteroidSpawnRate = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(AsteroidSpawnRate == 0) {
            AsteroidSpawnRate = initAsteroidSpawnRate;
        }
        StartCoroutine(spawnRepeating());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator spawnRepeating()
    {
        while(true)
        {
            float time = AsteroidSpawnRate * Random.Range(0.7f, 1.3f);
            yield return new WaitForSeconds(time );
            SpawnAsteroid();
        }
    }
    //spawn an asteroid in sort of the direction the player is facing
    public void SpawnAsteroid()
    {
        //instaniate a planet!
        GameObject aster= Instantiate(asteroid);
        //set sprite (Fun) 
        aster.GetComponent<SpriteRenderer>().sprite = astSprites[(Random.Range(0, astSprites.Length))];
        //reset polygon collider
        Destroy(aster.GetComponent<PolygonCollider2D>());
        aster.AddComponent<PolygonCollider2D>();
        //spawn in sort of the directon the player is facing

        // Get the direction that the player is facing
        Vector3 playerDirection = player.transform.up;

        // Generate a random angle between -60 and 60 degrees
        float angle = Random.Range(-20f, 20f);

        // Rotate the player direction by the random angle
        Vector3 planetDirection = Quaternion.Euler(0, angle, 0) * playerDirection;

        // Set the position of the planet to be 30-100 units in the direction the player is facing
        aster.transform.position = player.transform.position + planetDirection * Random.Range(20f, 35f);
        aster.transform.localPosition = new Vector3(aster.transform.localPosition.x, aster.transform.localPosition.y, 0);

    }

    public static void setAstRate(float mult)
    {
        AsteroidSpawnRate *= mult;
    }
}
