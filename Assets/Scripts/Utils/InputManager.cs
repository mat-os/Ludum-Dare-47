using System;
using UnityEditor;
using UnityEngine;

public class InputManager : MonoBehaviourSingleton<InputManager>
{
    private float minInputRadius;
    private float smoothRadius;
    
    private float width;
    private float height;

    public float SmoothRadius => smoothRadius;
    public float MinInputRadius => minInputRadius;

    private Vector3 startTouchPos;
    
    private void Awake()
    {
        //minInputRadius = GameplayManager.Instance.GameConfig.InputConfig.minInputRadius;
        //smoothRadius = GameplayManager.Instance.GameConfig.InputConfig.smoothRadius;
        
        width = Screen.width / 2.0f;
        height = Screen.height / 2.0f;
    }
    public static Vector3 MouseWorldPos()
    {
        var screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        
        return screenPosition;
    }
    public Vector3 SwipeDirection()
    {
        Vector3 swipeDirection = Vector3.zero;
        
        //Считываем начальное нажатие
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPos = MouseWorldPos();
        }
        
        //Считываем направление
        else if (Input.GetMouseButton(0))
        {
            swipeDirection = MouseWorldPos() - startTouchPos;

            if (swipeDirection.magnitude > smoothRadius)
            {
                startTouchPos += swipeDirection * 0.01f;
            }
        }
        
        return swipeDirection;
    }

    public float SwipeDistanceCoefficient()
    {
        var minSwipeCoef = 0.1f;
        var maxSwipeCoef = 1f;
            
        var t = (SwipeDirection().magnitude - MinInputRadius) /
                (SmoothRadius - MinInputRadius);
            
        return Mathf.Lerp(minSwipeCoef, maxSwipeCoef, t);
    }
    
    /*private Vector3 fp; //First touch position
private Vector3 lp; //Last touch position
private float dragDistance; //minimum distance for a swipe to be registered

private void Start()
{
    dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
}*/
    
    /*private void Update()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            var touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position; //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {
                    //It's a drag
                    //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {
                        //If the horizontal movement is greater than the vertical movement...
                        if (lp.x > fp.x) //If the movement was to the right)
                            //Right swipe
                            Debug.Log("Right Swipe");
                        else
                            //Left swipe
                            Debug.Log("Left Swipe");
                    }
                    else
                    {
                        //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y) //If the movement was up
                            //Up swipe
                            Debug.Log("Up Swipe");
                        else
                            //Down swipe
                            Debug.Log("Down Swipe");
                    }
                }
                else
                {
                    //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }*/
}