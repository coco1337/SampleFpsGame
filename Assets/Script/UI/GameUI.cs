using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class GameUI : MonoBehaviour
{
  public static GameUI Instance;

  [SerializeField] private GameObject resultPanel;

  [SerializeField] private TextMeshProUGUI timer;
  [SerializeField] private TextMeshProUGUI weapon;
  [SerializeField] private TextMeshProUGUI magazine;

  private void Start()
  {
    Instance ??= this;
  }

  public void UpdateTimer()
  {
    timer.text = "";
  }

  public void UpdateWeapon(string weaponName)
  {
    weapon.text = weaponName;
  }

  public void UpdateMagazine(string remainBullet)
  {
    // TODO : magazine무기에따라 최대 양
    magazine.text = remainBullet;
  }
}
