using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerWild : MonoBehaviour
{
    public float swipeRangeX = 12f;
    public float swipeRangeY = 16f;
    public float swipeSpeed = 20f; 
    private Vector3 startPosition; 
    private Vector3 currentPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            float deltaHorizontal = Input.GetTouch(0).deltaPosition.x / Screen.width;
            float deltaVertical = Input.GetTouch(0).deltaPosition.y / Screen.height;
            currentPosition = transform.position;
            currentPosition.x = Mathf.Clamp(currentPosition.x - deltaHorizontal * swipeSpeed,
                                            startPosition.x - swipeRangeX,
                                            startPosition.x + swipeRangeX);
            currentPosition.y = Mathf.Clamp(currentPosition.y - deltaVertical * swipeSpeed,
                                            startPosition.y - swipeRangeY,
                                            startPosition.y + swipeRangeY);
            transform.position = currentPosition;
        }
    }
}
