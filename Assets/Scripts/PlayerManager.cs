using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using UnityEngine.UI;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerDeath))]

public class PlayerManager : TurnManager
{
    // reference to PlayerMover and PlayerInput components
    public PlayerMover playerMover;
    public PlayerInput playerInput;

    Board m_board;

    public UnityEvent deathEvent;

    public EnergyManager energyManager;

    private AudioManager audioManager;
    protected override void Awake()
    {
        base.Awake();

        // cache references to PlayerMover and PlayerInput
        playerMover = GetComponent<PlayerMover>();
        playerInput = GetComponent<PlayerInput>();

        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        // make sure that input is enabled when we begin
        playerInput.InputEnabled = true;
        energyManager = Object.FindObjectOfType<EnergyManager>().GetComponent<EnergyManager>();
        audioManager = Object.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }




    void Update()
    {
        // if the player is currently moving, ignore user input
        if (playerMover.isMoving || m_gameManager.CurrentTurn != Turn.Player)
        {
            return;
        }

        // get keyboard input
        playerInput.GetInput();

        // connect user input with PlayerMover's Move methods
        if (playerInput.V == 0)
        {
            if(playerInput.H < 0)
            {
                playerMover.MoveLeft();
                playerInput.ClearInput();
            }
            else if (playerInput.H > 0)
            {
                playerMover.MoveRight();
                playerInput.ClearInput();
            }

        } else if (playerInput.H == 0)
        {
            if(playerInput.V < 0)
            {
                playerMover.MoveBackward();
                playerInput.ClearInput();
            }
            else if(playerInput.V > 0)
            {
                playerMover.MoveForward();
                playerInput.ClearInput();
            }
        }
    }


    public void Die()
    {
        if(deathEvent != null)
        {
            deathEvent.Invoke();
        }
    }



    void CaptureEnemies()
    {
        if (m_board != null)
        {
            List<EnemyManager> enemies = m_board.FindEnemiesAt(m_board.PlayerNode);

            if(enemies.Count != 0)
            {
                foreach(EnemyManager enemy in enemies)
                {
                    if(enemy != null)
                    {
                        enemy.Die();
                        if(audioManager != null)
                        {
                            audioManager.Play("EnemyDie");
                        }
                        
                        energyManager.DecreaseEnergy(5);
                    }
                }
            }


        }
    }

    public override void FinishTurn()
    {
        CaptureEnemies();
        base.FinishTurn();
    }
}
