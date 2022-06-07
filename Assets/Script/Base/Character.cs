using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    BaseMovement baseMovement;
    public BaseCharacterController CharacterController;

    [SerializeField] float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        baseMovement = GetComponent<BaseMovement>();

        if (baseMovement == null)
        {
            baseMovement = gameObject.AddComponent<BaseMovement>();
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


    public BaseMovement GetMovement() 
    {
        return baseMovement;
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
}
