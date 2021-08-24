using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    Vector3 currentCamPos;

    Vector3 desPos;
    public float offsetX = 15;
    // what easetype to use for iTweening
    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
    // how fast we move
    public float moveSpeed = 10f;
    // delay to use before any call to iTween
    public float iTweenDelay = 0f;

    public Transform desTrans;

    public Animator animator;

    
    private void Awake()
    {
        currentCamPos = Camera.main.transform.position;
      
    }


    public void MoveLeft()
    {
     
        desPos.x = currentCamPos.x - offsetX;
        desPos.y = currentCamPos.y;
        desPos.z = currentCamPos.z;
        StartCoroutine(MoveCameraTo(desPos.x, desPos.y, desPos.z));
    }

    public void MoveRight()
    {
        desPos.x = currentCamPos.x + offsetX;
        desPos.y = currentCamPos.y;
        desPos.z = currentCamPos.z;

        StartCoroutine(MoveCameraTo(desPos.x, desPos.y, desPos.z));
       
    }



    public void MoveCamAnimation()
    {
        animator.SetTrigger("Move");
    }
   

    IEnumerator MoveCameraTo(float desX, float desY, float desZ)
    {
        
        iTween.MoveTo(gameObject, iTween.Hash(
          "x", desX,
          "y", desY,
          "z", desZ,
          "delay", iTweenDelay,
          "easetype", easeType,
          "speed", moveSpeed
          )); ;

        while(Vector3.Distance(desPos, transform.position) > 0.01f)
        {
            yield return null;
        }

        currentCamPos = Camera.main.transform.position;

       
    }


  

}
