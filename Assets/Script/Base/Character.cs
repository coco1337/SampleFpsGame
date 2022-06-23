using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public BaseCharacterController CharacterController;

    [SerializeField] float speed = 3.0f;
    [SerializeField] float maxSpeed = 5.0f;
    [SerializeField] float jumpPower = 5.0f;

    [SerializeField] Animator characterMovementAnimator;
    [SerializeField] CapsuleCollider collider;
    // 임시 영역
    [SerializeField] Rigidbody rigidbody;

    // Animation Parameters
    bool Jumped = false;
    float Blend = 0.0f;
    bool IsGrounded = true;

    float height { get { return collider.height; } }
    float skin_width = 0.02f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CalculateAnimationParameters();
        ApplyAnimationParameters();
    }

    public void Move(Vector3 moveVector) 
    {
        Vector3 rigidMoveVector = ((transform.forward * moveVector.z) + (transform.right * moveVector.x)) * Time.deltaTime * speed;
        rigidbody.MovePosition(transform.position + rigidMoveVector);
        
        Blend = rigidMoveVector.magnitude / maxSpeed;
        characterMovementAnimator.SetFloat("Blend", Blend);
    }

    public void Rotate(Vector3 rotateVector) 
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotateVector);
    }

    public void Jump() 
    {
        Jumped = true;
        rigidbody.AddForce(new Vector3(0, 1, 0) * jumpPower, ForceMode.Impulse);
    }

    void CalculateAnimationParameters()
    {
        CheckGround();
    }

    void ApplyAnimationParameters()
    {
        characterMovementAnimator.SetBool("Jumped", Jumped);
        characterMovementAnimator.SetFloat("Blend", Blend);
        characterMovementAnimator.SetBool("IsGrounded", IsGrounded);

        Jumped = false;
    }

    private void CheckGround()
    {
        Debug.DrawRay(transform.position, -Vector3.up * (height / 2 + skin_width), Color.red);
        Debug.Log($"IsGrounded : {IsGrounded}");
        IsGrounded = Physics.Raycast(transform.position, -Vector3.up, height / 2 + skin_width);
    }

    public bool GetIsGrounded()
    {
        return IsGrounded;
    }
}
