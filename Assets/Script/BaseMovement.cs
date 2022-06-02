using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    protected float max_speed = 10.0f;

    protected float max_rotate_speed = 240.0f;

    Vector3 lastMoveVector;
    Vector3 currentMoveVector;
    Vector3 totalMoveVector;

    Vector3 lastRotateVector;
    Vector3 currentRotateVector;
    Vector3 totalRotateVector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProceedRotate();
        ProceedMove();
    }

    public void CancelMoveTo() 
    {
        totalMoveVector = Vector3.zero;
    }

    public void MoveTo(Vector3 position) 
    {
        totalMoveVector += position - transform.position;
    }

    public void MoveBy(Vector3 moveVector) 
    {
        currentMoveVector += moveVector;
    }

    // This function gets eulerAngles(Vector3)
    public void RotateTo(Vector3 eulerAngles) 
    {
        totalRotateVector += eulerAngles - transform.rotation.eulerAngles;
    }

    // This function gets eulerAngles(Vector3)
    public void RotateBy(Vector3 eulerAngles) 
    {
        currentRotateVector += eulerAngles;
    }

    protected void ProceedMove() 
    {
        float totalMoveVectorSize = totalMoveVector.magnitude;
        float currentMoveVectorSize = currentMoveVector.magnitude;

        lastMoveVector = Vector3.zero;
        if (totalMoveVectorSize.Equals(0) && currentMoveVectorSize.Equals(0))
        {
            return;
        }
        else 
        {
            if (! currentMoveVectorSize.Equals(0))
            {
                if (currentMoveVectorSize > max_speed)
                {
                    lastMoveVector = currentMoveVector.normalized * max_speed;
                }
                else 
                {
                    lastMoveVector = currentMoveVector;
                }
                currentMoveVector = Vector3.zero;
            }

            if (!totalMoveVectorSize.Equals(0)) 
            { 
                if (totalMoveVectorSize > max_speed)
                {
                    lastMoveVector += totalMoveVector.normalized * max_speed;
                    totalMoveVector -= totalMoveVector.normalized * max_speed;
                }
                else 
                {
                    lastMoveVector += totalMoveVector;
                    totalMoveVector = Vector3.zero;
                }
            }
 
            transform.position += lastMoveVector;
        }

        Debug.Log("ProceedMove!");
    }

    protected void ProceedRotate() 
    {
        if (totalRotateVector.magnitude == 0)
        {
            return;
        }
        else
        {
            if (totalRotateVector.magnitude > max_speed)
            {
                totalRotateVector += totalRotateVector.normalized * max_speed;
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + totalRotateVector.x, transform.rotation.eulerAngles.y + totalRotateVector.y, transform.rotation.eulerAngles.z + totalRotateVector.z);
                totalRotateVector -= lastRotateVector;
            }
            else
            {
                lastRotateVector = totalRotateVector;
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + totalRotateVector.x, transform.rotation.eulerAngles.y + totalRotateVector.y, transform.rotation.eulerAngles.z + totalRotateVector.z);
                totalRotateVector = lastRotateVector;
            }
        }
    }
}
