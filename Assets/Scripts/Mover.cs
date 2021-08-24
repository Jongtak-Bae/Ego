using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mover : MonoBehaviour
{
    // where the player is currently headed
    public Vector3 destination;

    public bool faceDestination = false;

    // is the player currently moving?
    public bool isMoving = false;

    // what easetype to use for iTweening
    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;

    // how fast we move
    public float moveSpeed = 1.5f;

    // time to rotate to face destination
    public float rotateTime = 0.5f;

    // delay to use before any call to iTween
    public float iTweenDelay = 0f;

    // reference to the Board component
    protected Board m_board; 


    protected Node currentNode;
    public  Node CurrentNode { get => currentNode; }

    public UnityEvent finishMovementEvent;

    public AudioManager audioManager;

    protected virtual void Awake()
    {
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        audioManager = Object.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    protected virtual void Start()
    {
        UpdateCurrentNode();

    }



    // public method to invole the MoveRoutine
    public void Move(Vector3 destinationPos, float delayTime = 0.25f)
    {
        if (isMoving)
        {
            return;
        }
        if (m_board != null)
        {
            // check if a Node ecists at the destination position
            Node targetNode = m_board.FindNodeAt(destinationPos);

            // ... and only move id there IS a Node AND the target Node is linked to the PlayerNode
            if (targetNode != null && CurrentNode != null)
            {
                if(CurrentNode.LinkedNodes.Contains(targetNode))
                {
                    // start the coroutine MoveRoutine
                    StartCoroutine(MoveRoutine(destinationPos, delayTime));
                }
                else
                {
                    Debug.Log("MOVER:" + CurrentNode.name + "not connected" + targetNode.name);
                }
                
            }

        }



    }

    // coroutine used to move the player
    protected virtual IEnumerator MoveRoutine(Vector3 destinationPos, float delayTime)
    {
        
        // we are moving
        isMoving = true;

        // set the destination to the destinationPos being passed into the coroutine
        destination = destinationPos;

        //  optional turn to face destination
        if (faceDestination)
        {
            FaceDestination();
        }

        // pause the coroutine for a brief period
        yield return new WaitForSeconds(delayTime);


        audioManager.Play("Move");
        // move toward the destinationPos using the caseType and moveSpeed variables
        iTween.MoveTo(gameObject, iTween.Hash(
            "x", destinationPos.x,
            "y", destinationPos.y,
            "z", destinationPos.z,
            "delay", iTweenDelay,
            "easetype", easeType,
            "speed", moveSpeed
            ));

        while (Vector3.Distance(destinationPos, transform.position) > 0.01f)
        {
            yield return null;

        }
        // stop the iTween immdiately
        iTween.Stop(gameObject);

        // set the player position to the destination explicitly
        transform.position = destinationPos;

        // we are not moving
        isMoving = false;

        UpdateCurrentNode();

        

       


    }

    public void MoveLeft()
    {
        Vector3 newPostion = transform.position + new Vector3(-Board.spacing, 0f, 0f);
        Move(newPostion, 0);
    }


    public void MoveRight()
    {
        Vector3 newPostion = transform.position + new Vector3(Board.spacing, 0f, 0f);
        Move(newPostion, 0);
    }


    public void MoveForward()
    {

        Vector3 newPostion = transform.position + new Vector3(0f, 0f, Board.spacing);
        Move(newPostion, 0);
    }

    public void MoveBackward()
    {
        Vector3 newPostion = transform.position + new Vector3(0f, 0f, -Board.spacing);
        Move(newPostion, 0);
    }


    protected void UpdateCurrentNode()
    {
        if(m_board != null)
        {
            currentNode = m_board.FindNodeAt(transform.position);
        }
    }


    protected void FaceDestination()
    {
        Vector3 relativePosition = destination - transform.position;

        Quaternion newRotation = Quaternion.LookRotation(relativePosition, Vector3.up);

        float newY = newRotation.eulerAngles.y;

        iTween.RotateTo(gameObject, iTween.Hash(
            "y", newY,
            "delay", 0f,
            "easetype", easeType,
            "time", rotateTime
            ));
    }
}
