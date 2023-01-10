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

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
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
    }
}
