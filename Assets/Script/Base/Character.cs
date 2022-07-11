using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Character : MonoBehaviour
{
  public BaseCharacterController CharacterController;

  [SerializeField] private Vector3 moveVector = new Vector3();

  [SerializeField] private float speed = 3.0f;
  [SerializeField] private float maxSpeed = 1.0f;
  [SerializeField] private float jumpPower = 5.0f;

  [SerializeField] private Animator characterMovementAnimator;
  [SerializeField] private CapsuleCollider capsuleCollider;
  // 임시 영역
  [SerializeField] private Rigidbody characterRigidbody;
  [SerializeField] private float maxSnapSpeed = 50f;

  // Animation Parameters
  private bool jumped = false;
  private readonly int JUMPED = Animator.StringToHash("Jumped");
  private float blend = 0.0f;
  private readonly int BLEND = Animator.StringToHash("Blend");
  private bool isGrounded = true;
  private readonly int IS_GROUNDED = Animator.StringToHash("IsGrounded");

  private float height { get { return capsuleCollider.height; } }
  private float skinWidth = 0.02f;

  private float wallCheckDistance;

  private Vector3 groundCheckOffset;

  public bool GetIsGrounded() => isGrounded;
  public void UpdatePlayerInput(Vector3 vec) => moveVector = vec;

  // Start is called before the first frame update
  private void Start()
  {
    capsuleCollider = GetComponent<CapsuleCollider>();
    characterRigidbody = GetComponent<Rigidbody>();
    wallCheckDistance = 3f * capsuleCollider.radius;
    groundCheckOffset = new Vector3(0, -(capsuleCollider.height / 2) + capsuleCollider.radius - 0.05f, 0);
  }

  // Update is called once per frame
  private void FixedUpdate()
  {
    Move();
    CalculateAnimationParameters();
    ApplyAnimationParameters();
  }

  private void Move()
  {
    var rigidMoveVector = WallCheck(speed * Time.deltaTime * ((transform.forward * moveVector.z) + (transform.right * moveVector.x)));
    characterRigidbody.MovePosition(transform.position + rigidMoveVector);

    blend = rigidMoveVector.magnitude / (maxSpeed * Time.deltaTime);
    characterMovementAnimator.SetFloat(BLEND, blend);
  }

  public void Rotate(Vector3 rotateVector)
  {
    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotateVector);
  }

  public void Jump()
  {
    if (!isGrounded || jumped) return;
    jumped = true;
    characterRigidbody.AddForce(new Vector3(0, 1, 0) * jumpPower, ForceMode.Impulse);
  }

  private void CalculateAnimationParameters()
  {
    CheckGround();
  }

  private void ApplyAnimationParameters()
  {
    characterMovementAnimator.SetBool(JUMPED, jumped);
    characterMovementAnimator.SetBool(IS_GROUNDED, isGrounded);
  }

  private void CheckGround()
  {
    // Debug.DrawRay(transform.position, Vector3.down * (height / 2 + skinWidth), Color.red);
    // D.Log($"IsGrounded : {isGrounded}");

    // TODO : fix ground check on top-end of slope
    Debug.DrawRay(transform.position, Vector3.down * (height / 2 + skinWidth), Color.red);
    isGrounded = characterRigidbody.velocity.y <= 0 && Physics.CheckSphere(transform.position + groundCheckOffset, capsuleCollider.radius, 1 << LayerMask.NameToLayer("Ground"));
    isGrounded = characterRigidbody.velocity.y <= 0 && Physics.Raycast(transform.position, Vector3.down, height / 2 + skinWidth);
    // isGrounded = characterRigidbody.velocity.y == 0;
    // SnapToGround();
    
    if (isGrounded)
    {
      jumped = false;
    }
  }

  private bool SnapToGround()
  {
    var speed = characterRigidbody.velocity.magnitude;
    // if (speed > maxSnapSpeed) return false;
    if (!Physics.Raycast(transform.position, Vector3.down - groundCheckOffset, out var rayHit, 1 << LayerMask.NameToLayer("Ground"))) return false;

    var contactNormal = rayHit.normal;
    var dot = Vector3.Dot(characterRigidbody.velocity, rayHit.normal);
    if (dot > 0)
      characterRigidbody.velocity = (characterRigidbody.velocity - rayHit.normal * dot).normalized * speed;

    return true;
  }

  private Vector3 WallCheck(Vector3 moveVector)
  {
    // TODO : check with different height, slope
    CheckAxisX(ref moveVector);
    CheckAxisY(ref moveVector);
    CheckAxisZ(ref moveVector);

    // Debug.DrawRay(transform.position, moveVector * 20);
    return moveVector;
  }

  private void CharacterAxisCheck(Vector3 direction, ref Vector3 moveVector)
  {
    if (Physics.Raycast(transform.position, direction, out var hitWall, wallCheckDistance))
    {
      moveVector += ((Vector3.Dot(moveVector.normalized, -hitWall.normal) * moveVector.magnitude) * hitWall.normal);
    }
  }

  private void CheckAxisX(ref Vector3 moveVector)
  {
    if (moveVector.x == 0) return;
    CharacterAxisCheck(new Vector3(moveVector.x > 0 ? 1 : -1, 0, 0), ref moveVector);
  }

  private void CheckAxisY(ref Vector3 moveVector)
  {
    if (moveVector.y == 0) return;
    CharacterAxisCheck(new Vector3(0, moveVector.y > 0 ? 1 : -1, 0), ref moveVector);
  }

  private void CheckAxisZ(ref Vector3 moveVector)
  {
    if (moveVector.z == 0) return;
    CharacterAxisCheck(new Vector3(0, 0, moveVector.z > 0 ? 1 : -1), ref moveVector);
  }
}
