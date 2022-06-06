using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    protected float max_speed = 10.0f;
    protected float max_rotate_speed = 240.0f;

    bool isGravityEnabled = true;
    float gravityScale = 1.0f;

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
        
        isGravityEnabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProceedRotate();
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
        Debug.Log("MoveBy Called!");
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


    // WIP
    protected void ProceedRotate() 
    {
        if (rotateToVector.magnitude == 0)
        {
            return;
        }
        else
        {
            if (rotateToVector.magnitude > max_speed)
            {
                rotateToVector += rotateToVector.normalized * max_speed;
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + rotateToVector.x, transform.rotation.eulerAngles.y + rotateToVector.y, transform.rotation.eulerAngles.z + rotateToVector.z);
                rotateToVector -= currentRotateVector;
            }
            else
            {
                currentRotateVector = rotateToVector;
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + rotateToVector.x, transform.rotation.eulerAngles.y + rotateToVector.y, transform.rotation.eulerAngles.z + rotateToVector.z);
                rotateToVector = currentRotateVector;
            }
        }
    }
}
