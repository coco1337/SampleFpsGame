using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    // this values are based on per second
    public float max_speed = 10.0f;
    public float max_moveBy_speed = 10.0f;
    public float max_moveTo_speed = 10.0f;
    public float max_acceleration = 10.0f;

    public float max_rotate_angle = 240.0f;
    public float max_rotateTo_angle = 240.0f;
    public float max_rotateBy_angle = 240.0f;

    public bool isGravityEnabled = true;

    // default gravity constance is 9.8f (based on Physics.gravity)
    public float gravityScale = 1.0f;

    Vector3 currentMoveVector;
    Vector3 lastMoveVector;
    Vector3 moveByVector;
    Vector3 moveToVector;

    Vector3 currentRotateVector;
    Vector3 lastRotateVector;
    Vector3 rotateByVector;
    Vector3 rotateToVector;
    
    private void Awake()
    {
        currentMoveVector = Vector3.zero;
        lastMoveVector = Vector3.zero;
        moveByVector = Vector3.zero;
        moveToVector = Vector3.zero;

        currentRotateVector = Vector3.zero;
        lastMoveVector = Vector3.zero;
        rotateByVector = Vector3.zero;
        rotateToVector = Vector3.zero;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DetermineRotate();
        CalculateRotateBy();
        ProceedRotate();

        DetermineMove();
        CalculateGravity();
        CalculateMoveBy();
        ProceedMove();
    }

    public void CancelMoveTo() 
    {
        moveToVector = Vector3.zero;
    }

    public void MoveTo(Vector3 position) 
    {
        moveToVector = position - transform.position;
    }

    public void AddMoveToVector(Vector3 position)
    {
        moveToVector += position - transform.position;
    }

    public void MoveBy(Vector3 moveVector) 
    {
        moveByVector += moveVector;
    }

    protected void CalculateGravity()
    {
        if (isGravityEnabled)
        {
            currentMoveVector += Physics.gravity * gravityScale * Time.deltaTime;
        }
    }

    protected void CalculateMoveBy() 
    {
        float MoveByVectorSize = moveByVector.magnitude;

        if (MoveByVectorSize != 0)
        {
            if (MoveByVectorSize > max_speed)
            {
                currentMoveVector += moveByVector.normalized * max_speed;
            }
            else
            {
                currentMoveVector += moveByVector;
            }
            moveByVector = Vector3.zero;
        }
    }
    protected void CalculateMoveTo() 
    {
        float MoveToVectorSize = moveToVector.magnitude;

        if (MoveToVectorSize != 0)
        {
            if (MoveToVectorSize > max_speed)
            {
                currentMoveVector += moveToVector.normalized * max_speed;
                moveToVector -= moveToVector.normalized * max_speed;
            }
            else
            {
                currentMoveVector += moveToVector;
                moveToVector = Vector3.zero;
            }
        }
    }

    protected void DetermineMove() 
    {
    
    }

    protected void ProceedMove() 
    {
        transform.position += currentMoveVector;
        lastMoveVector = currentMoveVector;
        currentMoveVector = Vector3.zero;
    }


    // This function gets eulerAngles(Vector3)
    public void RotateTo(Vector3 eulerAngles) 
    {
        rotateToVector += eulerAngles - transform.rotation.eulerAngles;
    }

    // This function gets eulerAngles(Vector3)
    public void RotateBy(Vector3 eulerAngles) 
    {
        rotateByVector += eulerAngles;
    }

    private void DetermineRotate() 
    { 

    }
    // WIP

    private void CalculateRotateTo()
    {
        float rotateToVectorSize = rotateToVector.magnitude;

        if (rotateToVectorSize != 0) 
        {
            if (rotateToVectorSize > max_speed)
            {
                currentRotateVector += rotateToVector.normalized * max_speed;
                
                rotateToVector -= currentRotateVector;
            }
            else
            {
                currentRotateVector = rotateToVector;
                rotateToVector = Vector3.zero;
            }
        }
    }

    protected void CalculateRotateBy() 
    {
        float rotateByVectorSize = rotateByVector.magnitude;

        if (rotateByVectorSize != 0)
        {
            if (rotateByVectorSize > max_speed)
            {
                currentRotateVector += rotateByVector.normalized * max_speed;
            }
            else
            {
                currentRotateVector += rotateByVector;
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotateByVector);
            }
            rotateByVector = Vector3.zero;
        }
    }


    protected void ProceedRotate() 
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + currentRotateVector);
        lastRotateVector = currentRotateVector;
        currentRotateVector = Vector3.zero;
    }
}
