using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Character is having mass, mass center and can be affected gravity
// alse having various moving states (walk, run, swim, etc.)

public class CharacterMovement : BaseMovement
{
    //
    [SerializeField] Animator characterMovementAnimator;
    [SerializeField] CapsuleCollider collider;
    [SerializeField] Rigidbody rigidbody;

    // Animator Parameters
    bool Jumped = false;
    float Blend = 0.0f;
    bool IsGrounded = true;

    Vector3 headPoint;
    Vector3 bottomPoint;

    float height { get { return collider.height; } }
    float skin_width = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        if(characterMovementAnimator == null) 
        {
            characterMovementAnimator = GetComponent<Animator>();
        }
        collider = GetComponent<CapsuleCollider>();

        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.freezeRotation = true;
            rigidbody.useGravity = true;
        }
    }

    private void Update()
    {
        CalculateRotateBy();
        ProceedRotate();
        CalculateMoveBy();
        ProceedMove();

        CalculateAnimationParameters();
        ApplyAnimationParameters();
    }

    void CalculateAnimationParameters() 
    {
        Blend = lastMoveVector.magnitude / Max_speed * 400;
        CheckGround();
    }

    void ApplyAnimationParameters() 
    {
        characterMovementAnimator.SetBool("Jumped", Jumped);
        characterMovementAnimator.SetFloat("Blend", Blend);
        characterMovementAnimator.SetBool("IsGrounded", IsGrounded);

        Jumped = false;
    }

    public void Jump(float jumpPower) 
    {
        Jumped = true;
        rigidbody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
    }

    private void CheckGround() 
    {
        Debug.DrawRay(transform.position, -Vector3.up * (height / 2 + skin_width), Color.red);
        Debug.Log($"IsGrounded : {IsGrounded}");
        IsGrounded = Physics.Raycast(transform.position, -Vector3.up, height / 2 + skin_width);
    }

    protected override void ApplyRotate() 
    {
        rigidbody.MoveRotation(Quaternion.Euler(transform.rotation.eulerAngles + currentRotateVector));
    }

    protected override void ApplyMove() 
    {
        base.ApplyMove();
        // rigidbody.MovePosition(transform.position + currentMoveVector);
    }

    public bool GetIsGrounded() 
    {
        return IsGrounded;
    }
}
