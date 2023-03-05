using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float swipeRange = 3f; // The distance the camera can move left or right
    public float swipeSpeed = 5f; // The speed at which the camera moves when swiped
    private Vector3 startPosition; // The starting position of the camera
    private Vector3 currentPosition; // The current position of the camera

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // Get the horizontal movement of the touch
                float deltaHorizontal = Input.GetTouch(0).deltaPosition.x / Screen.width;

                // Move the camera by the horizontal movement within the defined range
                currentPosition = transform.position;
                currentPosition.x = Mathf.Clamp(currentPosition.x - deltaHorizontal * swipeSpeed,
                                                startPosition.x - swipeRange,
                                                startPosition.x + swipeRange);
                transform.position = currentPosition;
            }
        
    }
}
