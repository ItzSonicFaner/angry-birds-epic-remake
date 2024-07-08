using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateBtn : MonoBehaviour
{
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
}
