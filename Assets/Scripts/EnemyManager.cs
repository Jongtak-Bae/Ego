using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemySensor))]
[RequireComponent(typeof(EnemyAttack))]

public class EnemyManager : TurnManager
{
    EnemySensor m_enemySensor;
    EnemyMover m_enemyMover;

    EnemyAttack m_enemyAttack;
    Board m_board;

    bool m_isDead = false;

    public bool IsDead { get => m_isDead; }

    public UnityEvent deathEvent;

    public MovementType movementType;
    private bool isEnemySilenced = false;
    public bool IsEnemySilenced { get => isEnemySilenced; set => isEnemySilenced = value; }



    // use this for initialization
    protected override void Awake()
    {
        base.Awake();
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        m_enemyMover = GetComponent<EnemyMover>();
        m_enemySensor = GetComponent<EnemySensor>();
        m_enemyAttack = GetComponent<EnemyAttack>();
    }

    public void PlayTurn()
    {
        if (m_isDead)
        {
            FinishTurn();
            return;
        }
        StartCoroutine(PlayTurnRoutine());
    }

    IEnumerator PlayTurnRoutine()
    {
        if(m_gameManager != null && !m_gameManager.IsGameOver)
        {
            // detect player
            m_enemySensor.UpdateSensor(m_enemyMover.CurrentNode);

            // wait
            yield return new WaitForSeconds(0f);

            if (m_enemySensor.FoundPlayer)
            {
                // notify the GameManager to lose the level
                m_gameManager.LoseLevel();

                Vector3 playerPostion = new Vector3(m_board.PlayerNode.Coordinate.x, 0f, m_board.PlayerNode.Coordinate.y);

                m_enemyMover.Move(playerPostion, 0f);

                while (m_enemyMover.isMoving)
                {
                    yield return null;
                }
                // attack player

                m_enemyAttack.Attack();
               
            }
            else
            {
                // movement
                m_enemyMover.MoveOneTurn();// m_enemyMover. Some move method..
            }
        }
       
       
        

    }


    public void Die()
    {
        if (m_isDead)
        {
            return;
        }
        m_isDead = true;

        if(deathEvent!= null)
        {
            deathEvent.Invoke();
        }
    }
}
