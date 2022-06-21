using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    // this values are based on per second
    // must multiply deltaTime
    public float max_speed = 10.0f;
    public float max_moveBy_speed = 10.0f;
    public float max_moveTo_speed = 10.0f;
    public float max_acceleration = 10.0f;

    protected float Max_speed { get { return max_speed * Time.deltaTime; } }

    public float max_rotate_angle = 240.0f;
    public float max_rotateTo_angle = 240.0f;
    public float max_rotateBy_angle = 240.0f;

    protected float Max_rotate_angle { get { return max_rotate_angle * Time.deltaTime; } }

    protected Vector3 currentMoveVector;
    protected Vector3 lastMoveVector;
    protected Vector3 moveByVector;
    protected Vector3 moveToVector;

    protected Vector3 currentRotateVector;
    protected Vector3 lastRotateVector;
    protected Vector3 rotateByVector;
    protected Vector3 rotateToVector;

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
        CalculateRotateBy();
        ProceedRotate();

        CalculateMoveBy();
        ProceedMove();
    }

    #region move

    public void CancelMoveTo() 
    {
        moveToVector = Vector3.zero;
    }

    public virtual void MoveTo(Vector3 position) 
    {
        moveToVector = position - transform.position;
    }

    public void AddMoveToVector(Vector3 position)
    {
        moveToVector += position - transform.position;
    }

    public virtual void MoveBy(Vector3 moveVector) 
    {
        moveByVector += moveVector;
    }


    protected virtual void CalculateMoveBy() 
    {
        float MoveByVectorSize = moveByVector.magnitude;

        if (MoveByVectorSize != 0)
        {
            if (MoveByVectorSize > Max_speed)
            {
                currentMoveVector += moveByVector.normalized * Max_speed;
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
            if (MoveToVectorSize > Max_speed)
            {
                currentMoveVector += moveToVector.normalized * Max_speed;
                moveToVector -= moveToVector.normalized * Max_speed;
            }
            else
            {
                currentMoveVector += moveToVector;
                moveToVector = Vector3.zero;
            }
        }
    }

    protected virtual void ApplyMove() 
    {
        transform.position += currentMoveVector;
    }

    protected void ProceedMove() 
    {
        ApplyMove();
        lastMoveVector = currentMoveVector;
        currentMoveVector = Vector3.zero;
    }

    #endregion

    #region rotate

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

    private void CalculateRotateTo()
    {
        float rotateToVectorSize = rotateToVector.magnitude;

        if (rotateToVectorSize != 0) 
        {
            if (rotateToVectorSize > Max_rotate_angle)
            {
                currentRotateVector += rotateToVector.normalized * Max_rotate_angle;
                
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
            if (rotateByVectorSize > Max_rotate_angle)
            {
                currentRotateVector += rotateByVector.normalized * Max_rotate_angle;
            }
            else
            {
                currentRotateVector += rotateByVector;
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotateByVector);
            }
            rotateByVector = Vector3.zero;
        }
    }

    protected virtual void ApplyRotate() 
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + currentRotateVector);
    }

    protected void ProceedRotate() 
    {
        ApplyRotate();
        lastRotateVector = currentRotateVector;
        currentRotateVector = Vector3.zero;
    }

#endregion
}
