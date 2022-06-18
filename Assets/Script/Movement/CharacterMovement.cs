using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Character is having mass, mass center and can be affected gravity
// alse having various moving states (walk, run, swim, etc.)
// This project uses this codes instead of rigidbody

public class CharacterMovement : BaseMovement
{
    //
    [SerializeField] Animator characterMovementAnimator;

    // Animator Parameters
    bool Jumped = false;
    float Blend = 0.0f;
    bool IsGrounded = true;

    Vector3 headPoint;
    Vector3 bottomPoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        DetermineRotate();
        CalculateRotateBy();
        ProceedRotate();

        DetermineMove();
        CalculateGravity();
        CalculateMoveBy();
        ProceedMove();

        CalculateAnimationParameters();
        ApplyAnimationParameters();
    }

    void CalculateAnimationParameters() 
    {
        Blend = lastMoveVector.magnitude / max_speed * 4;
    }

    void ApplyAnimationParameters() 
    {
        characterMovementAnimator.SetBool("Jumped", Jumped);
        characterMovementAnimator.SetFloat("Blend", Blend);
        characterMovementAnimator.SetBool("IsGrounded", IsGrounded);

        Jumped = false;
    }

    public void Jump() 
    {
        Jumped = true;
        MoveBy(new Vector3(0, 1, 0));
    }
    // Update is called once per frame
}
