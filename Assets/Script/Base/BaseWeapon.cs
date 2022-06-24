using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
  [SerializeField] private Transform leftHandIKTarget;
  [SerializeField] private Transform rightHandIKTarget;

  public Transform GetLeftHandIKTarget() => this.leftHandIKTarget;
  public Transform GetRightHandIKTarget() => this.rightHandIKTarget;
}
