using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    BaseMovement baseMovement;
    BaseCharacterController baseCharacterController;

    // Start is called before the first frame update
    void Start()
    {
        baseMovement = GetComponent<BaseMovement>();
        baseCharacterController = GetComponent<BaseCharacterController>();

        if (!baseMovement) 
        {
            gameObject.AddComponent<BaseMovement>();
        }

        if (!baseCharacterController) 
        {
            gameObject.AddComponent<BaseCharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public BaseCharacterController GetController()
    {
        return baseCharacterController;
    }

    public BaseMovement GetMovement() 
    {
        return baseMovement;
    }

    public virtual void MoveTo(Vector3 position) 
    {
        if (baseMovement)
            baseMovement.MoveTo(position);
    }

    public virtual void MoveBy(Vector3 moveVector) 
    {
        if(baseMovement)
            baseMovement.MoveBy(moveVector);
    }
}
