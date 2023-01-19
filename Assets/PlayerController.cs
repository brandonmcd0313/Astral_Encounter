using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float thrustForce;
    [SerializeField] Sprite active, idle;
    [SerializeField] float rotationSpeed;
    Rigidbody2D rb; SpriteRenderer sr;
    float currentTime = 0;
    [Tooltip("Time in seconds from 0 to fullThrust")]
    [SerializeField] float acelTime;

    //gun stuff
    Camera Acamera;
    bool canFire = true;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletCOoldown;
    [SerializeField] Vector2 offset;

    private List<GameObject> activeBullets = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Acamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Physics2D.IgnoreCollision(bulletPrefab.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        killBullets();
        InvokeRepeating("killBullets", 0f, 1.25f);
    }

    // Update is called once per frame
    void Update()
    {
        //Store input from the player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //horizontal controls change rotate player
        //rotate on z axis at inverse of horizontal axis
        transform.Rotate(0, 0, -horizontalInput * rotationSpeed * Time.deltaTime);
        //vertical controls boost player or slow player
        //if forward

        // Calculate the thrust force based on the current speed of the player
        // The thrust force increases as the player's speed increases\
        float thrust = 0;
        thrust = thrustForce * verticalInput * (currentTime / acelTime);
        // Apply thrust to the player character
        if (verticalInput > 0)
        {
            rb.AddForce(transform.up * thrust * Time.deltaTime);
            //print(thrust * Time.deltaTime);
            sr.sprite = active;
            // Increase the current timeset of the player
            currentTime += Time.deltaTime;
            // Clamp the current speed to the maximum speed
            currentTime = Mathf.Clamp(currentTime, 0, acelTime);
        }
        else
        {
            sr.sprite = idle;
            //time is now zero, we stopped accelerating
            currentTime = 0;
        }

        //give the player a GUN

        if (Input.GetButtonDown("Fire1") && canFire)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            Vector2 directBullet = transform.up * bulletSpeed;
            bullet.GetComponent<Rigidbody2D>().velocity = directBullet;
            activeBullets.Add(bullet);
            bullet = null;
            StartCoroutine(GunCooldown());

        }
    }
    void killBullets()
    {
        for (int i = activeBullets.Count - 1; i >= 0; i--)
        {
            GameObject bullet = activeBullets[i];
            //every 2 seconds destroy any bullet not in cam space
            //make sure not in camera space
            // Get the top-left corner of the camera's viewport
            Vector2 topLeft = Acamera.ViewportToWorldPoint(new Vector3(0, 1, Acamera.nearClipPlane));

            // Get the bottom-right corner of the camera's viewport
            Vector2 bottomRight = Acamera.ViewportToWorldPoint(new Vector3(1, 0, Acamera.nearClipPlane));
            if (bullet != null)
            {
                // Get the position of the game object in world space
                Vector2 goPos = bullet.transform.position;

                Bounds cameraBounds = new Bounds(bottomRight, topLeft);
                //check if this gameobject is contained with in those two points
                bool cameraContains = (goPos.x > topLeft.x && goPos.y < topLeft.y && goPos.x < bottomRight.x && goPos.y > bottomRight.y);
                if (!cameraContains)
                {
                    activeBullets.RemoveAt(i);
                    Destroy(bullet);
                }
            }
            else
            {
                activeBullets.RemoveAt(i);
            }
        }

    }



    IEnumerator GunCooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(bulletCOoldown);
        canFire = true;
    }
}

