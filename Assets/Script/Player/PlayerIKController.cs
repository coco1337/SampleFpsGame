using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerIKController : MonoBehaviour
{
  [SerializeField] private PlayerIKControl ikController;

  [SerializeField] private Transform leftIKTarget;
  [SerializeField] private Transform rightIKTarget;

  public void SetActive(bool b) => this.ikController.SetIKActive(b);

  private void Start()
  {
    this.ikController ??= GetComponentInChildren<PlayerIKControl>();

    SetIKTarget(this.leftIKTarget, this.rightIKTarget);
    SetActive(true);
  }

  public void SetIKTarget(Transform leftIK, Transform rightIK)
  {
    this.leftIKTarget = leftIK;
    this.rightIKTarget = rightIK;

    if (this.ikController == null)
    {
      D.Log("invalid ik controller");
      return;
    }

    this.ikController.SetHandTargets(leftIK, rightIK);
  }
}
