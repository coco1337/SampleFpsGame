using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Vector2 previousMousePosition;
    Vector2 mouseDelta;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMouseDelta();
    }

    public Vector2 GetMouseDelta() { return mouseDelta; }

    private void CalculateMouseDelta() 
    { 
        Vector2 currentMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        mouseDelta = currentMousePosition - previousMousePosition;
        previousMousePosition = currentMousePosition;
    }
}
