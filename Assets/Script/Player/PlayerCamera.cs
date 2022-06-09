using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject targetObject;

    BaseMovement baseMovement;

    public enum ECameraMode 
    { 
        FirstPersonView, ThirdPersonView
    }

    float ThirdPersonViewMaxCameraDistance;
    Vector3 ThirdPersonViewDelta;

    ECameraMode cameraMode = ECameraMode.FirstPersonView;

    void Start()
    {
        if (baseMovement == null) 
        {
            baseMovement = gameObject.AddComponent<BaseMovement>();
        }

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

    public virtual void MoveTo(Vector3 position)
    {
        if (baseMovement != null)
        {
            Debug.Log("Character MoveTo!");
            baseMovement.MoveTo(position);
        }
    }

    public virtual void MoveBy(Vector3 moveVector)
    {
        if (baseMovement != null)
        {
            Debug.Log("Character MoveBy!");
            baseMovement.MoveBy(moveVector);
        }
    }

    public virtual void RotateTo(Vector3 rotateVector)
    {
        if (baseMovement != null)
        {
            baseMovement.RotateTo(rotateVector);
        }
    }

    public virtual void RotateBy(Vector3 rotateVector)
    {
        if (baseMovement != null)
        {
            baseMovement.RotateBy(rotateVector);
        }
    }
}
