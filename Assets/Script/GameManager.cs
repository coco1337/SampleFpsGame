using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
  public static GameManager Instance;

  // Start is called before the first frame update
  private void Start()
  {
    DontDestroyOnLoad(this);
    Instance ??= this;
  }
}
