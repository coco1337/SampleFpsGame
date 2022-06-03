using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MainMenuUI : MonoBehaviour
{
  public void OnClickStartButton()
  {
    GameSceneManager.Instance.ChangeScene(1);
  }
}
