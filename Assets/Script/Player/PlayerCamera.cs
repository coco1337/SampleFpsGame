using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject targetObject;
    [SerializeField] Vector3 correctionPosition;

    public enum ECameraMode 
    { 
        FirstPersonView, ThirdPersonView
    }

    float ThirdPersonViewMaxCameraDistance;
    Vector3 ThirdPersonViewDelta;

    ECameraMode cameraMode = ECameraMode.FirstPersonView;

    void Start()
    {
        transform.rotation = Quaternion.Euler(targetObject.transform.forward);
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
        transform.position = targetObject.transform.position + correctionPosition + targetObject.transform.forward * 0.2f;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetObject.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
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
    public void Rotate(Vector3 rotateVector)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotateVector);
    }
}
