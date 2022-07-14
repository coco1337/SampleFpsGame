using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BaseStatus : MonoBehaviour
{
  [SerializeField] private int hp;

  public void GetDamage(int damage)
  {
    hp -= damage;
    if (hp <= 0) Death();
  }

  public void Death()
  {
    D.Log($"this bot(${transform.name}) died");
  }
}
