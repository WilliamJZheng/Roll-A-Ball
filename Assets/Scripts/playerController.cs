using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
 // Player Rigidbody
 private Rigidbody rb; 

 // Variable to keep track of collected points objects
 private int count;

 // Movement along X and Y axis
 private float movementX;
 private float movementY;

 // Speed at which the player moves
 public float speed = 0;

 // UI text component to display count of "PickUp" objects collected
 public TextMeshProUGUI countText;

 // UI object to display winning text
 public GameObject winTextObject;
public class Clone : MonoBehaviour
       {
       GameObject mother;
       GameObject[] Clones;     
       }
 void Start()
    {
//        mother = GameObject.Find("Collectibles");
//        Clones = new GameObjects[3000];
//        for (int i = 0; i < 3000; i++)
//       {
//              PickUp[i] = GameObject.Instantiate(mother);
//              PickUp[i].transform.position = new Vector3(Random.Range(-4f, 4f), 1f, i);
//       }

       rb = GetComponent<Rigidbody>();
       count = 0;
       SetCountText();
       winTextObject.SetActive(false);
    }
 
 // This function is called when a move input is detected
 void OnMove(InputValue movementValue)
    {
 // Convert the input value into a Vector2 for movement
        Vector2 movementVector = movementValue.Get<Vector2>();

 // Store the X and Y components of the movement
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

 // FixedUpdate is called once per fixed frame-rate frame
 private void FixedUpdate() 
    {
 // Create a 3D movement vector using the X and Y inputs
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

 // Apply force to the Rigidbody to move the player
        rb.AddForce(movement * speed); 
    }

 
 void OnTriggerEnter(Collider other) 
    {
 // Check if the object the player collided with has the "PickUp" tag
 if (other.gameObject.CompareTag("PickUp")) 
        {
              other.gameObject.SetActive(false);
              count = count + 1;
              SetCountText();
        }
else if (other.gameObject.CompareTag("Wall"))
        {
              rb.AddForce(0f, 200f, 0f);
        }

if (other.gameObject.CompareTag("Respawn"))
        {
              rb.AddForce(0f, 500f, 0f);
        }
    }

 // Function to update the displayed count of "PickUp" objects collected
 void SetCountText() 
    {
 // Update the count text with the current count
        countText.text = "Count: " + count.ToString();

 // Checks if count is equal to or above 12 points
 if (count >= 12)
        {
 // Display Win Text
            winTextObject.SetActive(true);
        }
    }
}