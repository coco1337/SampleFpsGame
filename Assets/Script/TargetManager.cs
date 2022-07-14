using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TargetManager : MonoBehaviour
{
  [SerializeField] private List<Transform> targetPositionList;
  [SerializeField] private PlayerCharacter enemyPrefab;

  private List<PlayerCharacter> enemyList;

  // Start is called before the first frame update
  private void Start()
  {
    SpawnEnemies();
  }

  private void SpawnEnemies()
  {
    foreach (var pos in targetPositionList)
    {
      var enemy = Instantiate<PlayerCharacter>(enemyPrefab);
      enemy.transform.position = pos.position;
      enemyList.Add(enemy);
    }
  }
}
