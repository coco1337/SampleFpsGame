using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class BaseProjectile : MonoBehaviour
{
  private enum EMoveState
  {
    NONE = 0,
    READY,  // enabled or spawned, ready to move
    MOVING, // moving
    STOP, // finish moving, need to destory or disable
    MAX = STOP -1
  }

  [Header("Options")]
  [SerializeField] private bool isHitScan;
  [SerializeField] private bool isPiercer;

  [Space]
  [Header("Base")]
  [SerializeField] private EMoveState state;
  [SerializeField] private float moveSpeed;
  [SerializeField] private int damage;
  [SerializeField] private float gravityScale; // if weightless => 0

  private void Update()
  {
    if (state == EMoveState.MOVING)
    {
      // TODO : move, gravity
    }
  }

  private bool CheckCollision()
  {
    // TODO : hit wall / hit some other player
    // use raycast instead of OnCollisionEnter event
    return false;
  }

  private void Move()
  {
    if (isHitScan)
    {
      // hit scan => check ray hit => destroy
    } 
    else
    {
      // move w/ gravity, speed
    }
  }
}
