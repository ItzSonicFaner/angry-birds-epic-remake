using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimateBtn : MonoBehaviour
{
    [Header("Games")]
    public GameObject birdAnimation;
    public string levelName;

    public void onButtonDown()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Pressed");
        animator.SetBool("stillPressed", true);
    }

    public void onButtonUp()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("stillPressed", false);
    }

    public void loadScene()
    {
        birdAnimation.GetComponent<DevsLoadingScreen>().sceneName = levelName;
        birdAnimation.transform.parent.gameObject.SetActive(true);
    }
}
