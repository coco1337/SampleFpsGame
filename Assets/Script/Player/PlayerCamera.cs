using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Start is called before the first frame update
    BaseMovement camera_movement;

    enum ECameraMode 
    { 
        FirstPersonView, ThirdPersonView
    }

    float ThirdPersonViewDistance = 10.0f;


    void Start()
    {
        if (camera_movement == null) 
        {
            camera_movement = gameObject.AddComponent<BaseMovement>();
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
