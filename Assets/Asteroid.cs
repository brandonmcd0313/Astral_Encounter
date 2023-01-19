using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UIElements;

public class Asteroid : MonoBehaviour
{
    Camera Acamera;
    [SerializeField] float speed;
    Vector3 direction;
    static int astDamage = -20;
    static int astValue = 50;
    // Start is called before the first frame update
    void Start()
    {
        Acamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        // Get the position of the player
        Vector3 playerPosition = GameObject.Find("Player").transform.position;

        // Calculate the direction vector towards the player
        direction = playerPosition - transform.position;

        //random angle
        float randomSign = Random.Range(-1.0f, 1.0f);
        float angle;
        if (randomSign > 0)
        {
            angle = Random.Range(20f, 40f);
        }
        else
        {
            angle = Random.Range(-40f, -20f);
        }

        direction = Vector3.Normalize(direction);
        direction = Quaternion.Euler(0, 0, angle) * direction;

        //rotate the object 

        // Make sure direction is a unit vector
        direction.Normalize();
        //start a 10 second timer
        StartCoroutine(lifespan());
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object towards the player
        transform.position += (direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Player")
        {
            //hit the player LMAO

            //minus some points
            //50 points seems reasonable
            ScoreManager.updateScore(astDamage);
        }

        //if we hit a bullet :o
        if (collision.collider.gameObject.tag == "Bullet")
        {
            ScoreManager.updateScore(astValue, this.gameObject);
            Destroy(collision.collider.gameObject);
            Destroy(gameObject);
        }
        }

    IEnumerator lifespan()
    {
        yield return new WaitForSeconds(4f);
        //make sure not in camera space
        // Get the top-left corner of the camera's viewport
        Vector2 topLeft = Acamera.ViewportToWorldPoint(new Vector3(0, 1, Acamera.nearClipPlane));

        // Get the bottom-right corner of the camera's viewport
        Vector2 bottomRight = Acamera.ViewportToWorldPoint(new Vector3(1, 0, Acamera.nearClipPlane));

        // Get the position of the game object in world space
        Vector2 goPos = gameObject.transform.position;

        Bounds cameraBounds = new Bounds(bottomRight, topLeft);
        //check if this gameobject is contained with in those two points
        bool cameraContains = (goPos.x > topLeft.x && goPos.y < topLeft.y && goPos.x < bottomRight.x && goPos.y > bottomRight.y);

        while(cameraContains)
        {   //reassign and wait
            yield return new WaitForSeconds(0.5f);
            //make sure not in camera space
            // Get the top-left corner of the camera's viewport
            topLeft = Acamera.ViewportToWorldPoint(new Vector3(0, 1, Acamera.nearClipPlane));

            // Get the bottom-right corner of the camera's viewport
            bottomRight = Acamera.ViewportToWorldPoint(new Vector3(1, 0, Acamera.nearClipPlane));

            // Get the position of the game object in world space
            goPos = gameObject.transform.position;

            cameraBounds = new Bounds(bottomRight, topLeft);
            //check if this gameobject is contained with in those two points
            cameraContains = (goPos.x > topLeft.x && goPos.y < topLeft.y && goPos.x < bottomRight.x && goPos.y > bottomRight.y);
          
        }
        //no longer contained in cam space, destory this object
        Destroy(gameObject);
    }
}
