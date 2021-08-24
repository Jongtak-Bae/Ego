using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GraphicMoverMode
{
    MoveTo,
    ScaleTo,
    MoveFrom,
    RotateFrom,
    RotateTo
}
public class GraphicMover : MonoBehaviour
{
    public GraphicMoverMode mode;

    public Transform startXform;

    public Transform endXform;



    public float moveTime = 1f;
    public float delay = 0f;
    public iTween.LoopType loopType = iTween.LoopType.none;

    public iTween.EaseType easeType = iTween.EaseType.easeOutExpo;

    public bool moveEnabled = true;

    

    private void Awake()
    {
        if (endXform == null)
        {
            endXform = new GameObject(gameObject.name + "XformEnd").transform;

            endXform.position = transform.position;
            endXform.rotation = transform.rotation;
            endXform.localScale = transform.localScale;
        }
        if(startXform == null)
        {
            startXform = new GameObject(gameObject.name + "XformStart").transform;

            startXform.position = transform.position;
            startXform.localRotation = transform.localRotation;
            startXform.localScale = transform.localScale;
        }
    }

    public void Reset()
    {
        switch (mode)
        {
            case GraphicMoverMode.MoveTo:
                if(startXform != null)
                {
                    transform.position = startXform.position;
                }
               
                break;
            case GraphicMoverMode.MoveFrom:
                if (endXform != null)
                {
                    transform.position = endXform.position;
                }
               
                break;
            case GraphicMoverMode.ScaleTo:
                if (startXform != null)
                {
                    transform.localScale = startXform.localScale;
                }
               
                break;
        }
    }


   

    public void BreathMoveBack()
    {
        
        endXform.position = gameObject.transform.position + new Vector3 (-400f, 0f, 0f);
        Move();
        endXform.position = gameObject.transform.position + new Vector3(0f, 0f, 0f);
    }

    public void MediMoveBack()
    {

        endXform.position = gameObject.transform.position + new Vector3(400f, 0f, 0f);
        Move();
        endXform.position = gameObject.transform.position + new Vector3(0f, 0f, 0f);
    }

    


    public void StartMoveWithDelay()
    {
        StartCoroutine(MoverWithDelay());
    }

    IEnumerator MoverWithDelay()
    {
        yield return new WaitForSeconds(1f);
        Move();
    }



  
    public void Move()
    {
        if (moveEnabled)
        {
            switch (mode)
            {
                case GraphicMoverMode.MoveTo:
                    iTween.MoveTo(gameObject, iTween.Hash(
                        "position", endXform.position,
                        "time", moveTime,
                        "delay", delay,
                        "easetype", easeType,
                        "looptype", loopType
                        ));
                    break;

                case GraphicMoverMode.ScaleTo:
                    iTween.ScaleTo(gameObject, iTween.Hash(
                        "scale", endXform.localScale,
                        "time", moveTime,
                        "delay", delay,
                        "easetype", easeType,
                        "looptype", loopType
                        ));
                    break;

                case GraphicMoverMode.MoveFrom:
                    iTween.MoveFrom(gameObject, iTween.Hash(
                        "position", startXform.position,
                        "time", moveTime,
                        "delay", delay,
                        "easetype", easeType,
                        "looptype", loopType
                        ));
                    break;

                case GraphicMoverMode.RotateFrom:
                    iTween.RotateTo(gameObject, iTween.Hash(
                        "rotation", new Vector3(0,0,0),
                        "time", moveTime,
                        "delay", delay,
                        "easetype", easeType,
                        "looptype", loopType
                        )); ; ; ;
                    break;
                case GraphicMoverMode.RotateTo:
                    iTween.RotateTo(gameObject, iTween.Hash(
                        "rotation", new Vector3(0, 0, -45),
                        "time", moveTime,
                        "delay", delay,
                        "easetype", easeType,
                        "looptype", loopType
                        ));
                    break;

            }

        }
       
    }

   
}
