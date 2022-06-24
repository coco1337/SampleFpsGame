using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public sealed class PlayerIKControl : MonoBehaviour
{
  private Animator animator;

  private Transform leftHandTarget = null;
  private Transform rightHandTarget = null;

  private bool ikActive = false;

  public void SetLeftHandTarget(Transform leftHandIK) => this.leftHandTarget = leftHandIK;
  public void SetRightHandTarget(Transform rightHandIK) => this.rightHandTarget = rightHandIK;
  public void SetIKActive(bool b) => this.ikActive = b;

  private void Start()
  {
    this.animator = GetComponent<Animator>();
  }

  private void OnAnimatorIK(int layerIndex)
  {
    if (this.animator)
    {
      if (this.ikActive)
      {
        if (this.rightHandTarget != null)
        {
          this.animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
          this.animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
          this.animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
          this.animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
        }

        if (this.leftHandTarget != null)
        {
          this.animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
          this.animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
          this.animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
          this.animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
        }
      }
      else
      {
        this.animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        this.animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
        this.animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
        this.animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        // this.animator.SetLookAtWeight(0);
      }
    }
  }

  public void SetHandTargets(Transform leftHandIK, Transform rightHandIK)
  {
    this.leftHandTarget = leftHandIK;
    this.rightHandTarget = rightHandIK;
  }
}
