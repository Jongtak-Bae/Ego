using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    Stationary,
    Patrol,
    Spin
}

public class EnemyMover : Mover
{

    public Vector3 directionToMove = new Vector3(0f, 0f, 2f);

    public MovementType movementType = MovementType.Stationary;
    public float standTime = 1f;


    public AudioManager audioManager;
    protected override void Awake()
    {
        base.Awake();
        faceDestination = true;
        audioManager = Object.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }
    protected override void Start()
    {
        base.Start();
        //StartCoroutine(TestMovementRoutine());
    }


    public void MoveOneTurn()
    {
        switch (movementType)
        {
            case MovementType.Patrol:
                audioManager.Play("Move");
                Patrol();
                break;
            case MovementType.Stationary:
                Stand();
                break;

            case MovementType.Spin:
                audioManager.Play("Move");
                Spin();
                break;
        }
    }

    void Patrol()
    {
        StartCoroutine(PatrolRoutine());

       
    }

    IEnumerator PatrolRoutine() {

        Vector3 startPos = new Vector3(CurrentNode.Coordinate.x, 0f, CurrentNode.Coordinate.y);


        // one space forward
        Vector3 newDest = startPos + transform.TransformVector(directionToMove);
        // two spaces forward
        Vector3 nextDest = startPos + transform.TransformVector(directionToMove * 2f);

        Move(newDest, 0f);

        
        while (isMoving)
        {
            yield return null;
        }

        if(m_board != null)
        {

            Node newDestNode = m_board.FindNodeAt(newDest);
            Node nextDestNode = m_board.FindNodeAt(nextDest);

            if(nextDestNode == null || !newDestNode.LinkedNodes.Contains(nextDestNode))
            {
                destination = startPos;
                FaceDestination();
            }
        }

        base.finishMovementEvent.Invoke();



    }


    void Stand()
    {
        StartCoroutine(StandRoutine());

    }

    IEnumerator StandRoutine() {

        yield return new WaitForSeconds(standTime);
        base.finishMovementEvent.Invoke();

    }


    void Spin()
    {
        StartCoroutine(SpinRoutine());
        
    }

    IEnumerator SpinRoutine()
    {
        Vector3 localForward = new Vector3(0f, 0f, Board.spacing);
        destination = transform.TransformVector(localForward * -1f) + transform.position;

        FaceDestination();
       

        yield return new WaitForSeconds(rotateTime);

        base.finishMovementEvent.Invoke();
    }
 
  


}
 