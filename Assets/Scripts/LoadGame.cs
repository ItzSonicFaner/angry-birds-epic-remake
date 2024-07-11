using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    [Header("Games")]
    public GameObject birdAnimation;
    public string levelName;

    public void loadScene()
    {
        birdAnimation.GetComponent<DevsLoadingScreen>().sceneName = levelName;
        birdAnimation.transform.parent.gameObject.SetActive(true);
    }

    void OnMouseUpAsButton()
    {
        loadScene();
    }
}
