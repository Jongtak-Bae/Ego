using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFade : MonoBehaviour
{
   public Animator animator;

    public void StartFadeAnimation()
    {
        animator.SetTrigger("Start");
    }
}
