using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    CharacterMovement baseMovement;
    public BaseCharacterController CharacterController;

    [SerializeField] float speed = 200.0f;
    [SerializeField] float jumpPower = 5.0f;
    
    // 임시 영역
    [SerializeField] Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        baseMovement = GetComponent<CharacterMovement>();

        if (baseMovement == null)
        {
            baseMovement = gameObject.AddComponent<CharacterMovement>();
            Debug.Log("CharacterMovement is null so create it!");
        }
        else
        {
            Debug.Log("CharacterMovement is not null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public CharacterMovement GetMovement() 
    {
        return baseMovement;
    }

    public void Move(Vector3 moveVector) 
    {
        Vector3 rigidMoveVector = (transform.forward * moveVector.z) + (transform.right * moveVector.x);
        rigidbody.MovePosition(transform.position + rigidMoveVector * Time.deltaTime * speed);
        // transform.Translate(moveVector * Time.deltaTime * speed);
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
        // Debug.Log("Character MoveBy!");
        // baseMovement.MoveBy(moveVector);

        if (baseMovement != null) 
        {
            Debug.Log("Character MoveBy!");
            baseMovement.MoveBy(moveVector * speed);
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
            // Rotate!
            baseMovement.RotateBy(rotateVector);
        }
    }

    public void Jump() 
    {
        if (baseMovement.GetIsGrounded()) 
        {
            baseMovement.Jump(jumpPower);
        }
    }
}
