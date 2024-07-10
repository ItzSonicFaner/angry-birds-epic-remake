using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevsLoadingScreen : MonoBehaviour
{
    [Header("Loading")]
    public GameObject gameObj;

    public void EnableGameobj()
    {
        gameObject.SetActive(false);
        gameObj.SetActive(true);
    }

    public void DisableGameobj()
    {
        gameObj.SetActive(false);
    }

    public void DisableCurObj()
    {
        gameObject.SetActive(false);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
