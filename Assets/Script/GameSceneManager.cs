using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

// This scripts contains scene context and manage game state
public sealed class GameSceneManager : MonoBehaviour
{
  public static GameSceneManager Instance;
  // Start is called before the first frame update
  private void Start()
  {
    DontDestroyOnLoad(this);
    Instance ??= this;
  }

  // Update is called once per frame
  private void Update()
  {

  }

  public void PauseGame()
  {
    Time.timeScale = 0.0f;
  }

  public void ResumeGame()
  {
    Time.timeScale = 1.0f;
  }

  public void ChangeScene(string sceneName)
  {
    SceneManager.LoadScene(sceneName);
  }

  public void ChangeScene(int sceneIndex)
  {
    SceneManager.LoadScene(sceneIndex);
  }
}
