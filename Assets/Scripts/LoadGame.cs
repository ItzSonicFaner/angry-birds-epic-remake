using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    [Header("Levels")]
    public string levelName;

    public void loadLevel()
    {
        SceneManager.LoadScene(levelName);
    }

    void OnMouseUpAsButton()
    {
        Debug.Log("Test");
        loadLevel();
    }
}
