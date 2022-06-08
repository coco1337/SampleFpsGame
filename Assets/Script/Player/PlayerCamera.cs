using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject targetObject;

    public enum ECameraMode 
    { 
        FirstPersonView, ThirdPersonView
    }

    float ThirdPersonViewMaxCameraDistance;
    Vector3 ThirdPersonViewDelta;

    ECameraMode cameraMode = ECameraMode.FirstPersonView;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (cameraMode == ECameraMode.FirstPersonView)
        {
            LateUpdateFirstPersonViewCamera();
        }
        else if (cameraMode == ECameraMode.ThirdPersonView) 
        {
            LateUpdateThirdPersonViewCamera();
        }
    }

    protected void LateUpdateFirstPersonViewCamera() 
    {
        transform.position = targetObject.transform.position;
    }

    protected void LateUpdateThirdPersonViewCamera() 
    { 
    
    }

    public void SetCameraMode(ECameraMode cameraMode) 
    {
        this.cameraMode = cameraMode;
    }

    public void SetThirdCameraDelta(Vector3 delta) 
    { 
        
    }
}
