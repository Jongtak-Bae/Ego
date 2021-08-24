using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public class AbilityManager : MonoBehaviour
{
    List<EnemyManager> m_enemies;
   MeshRenderer sightIcon;
   PlayerManager playerManager;
   GameManager m_gameManager;
   Text m_energyText;
   EnemyManager[] enemyManagers;

   
    public UnityEvent StartArrow;
    public UnityEvent CancelArrow;
    public UnityEvent StartSilence;
    public UnityEvent CancelSilence;
    public UnityEvent Slience;

    SpriteRenderer currentSleepDialog;

    EnemyMover enemyMover;

   EnemySensor enemySensor;
   EnemyManager enemyManager;


    public Material redSightMaterial;
    public Material blackSightMaterial;

    private MovementType enemyMovementType;
    public MovementType EnemyMovementType { get => enemyMovementType; set => enemyMovementType = value; }

 

    private int playerTurnCountSilence;
    public int PlayerTurnCountSilence { get => playerTurnCountSilence; set => playerTurnCountSilence = value; }
   

    private BoxCollider boxCollider;
    public EnergyManager energyManager;


    private string mode = "default";

    AudioManager audioManager;
 
  
    private void Awake()
    {
       
        playerManager = Object.FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
        m_gameManager = Object.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        energyManager = Object.FindObjectOfType<EnergyManager>().GetComponent<EnergyManager>();
        EnemyManager[] enemies = Object.FindObjectsOfType<EnemyManager>() as EnemyManager[];
        m_enemies = enemies.ToList();
        audioManager = Object.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();


    }

    public void ArrowAttack()
    {

        switch (mode)
        {
            case "arrow":
                //Debug.Log("Cancel arrow attack");
                CancelArrow.Invoke();
                mode = "default";
                break;


            case "default":
                //Debug.Log("Arrow attack");
                foreach (EnemyManager enemy in m_enemies)
                {
                    if (!enemy.IsDead)
                    {
                     
                       sightIcon = enemy.gameObject.transform.Find("Sight").gameObject.GetComponent<MeshRenderer>();
                        sightIcon.material = redSightMaterial;
                        sightIcon.enabled = true;
                      
                        boxCollider = enemy.GetComponentInParent<BoxCollider>();
                        boxCollider.enabled = true;
                    }
                }

                StartArrow.Invoke();

                if (audioManager != null)
                {
                    audioManager.Play("RollUp");
                }
               

                mode = "arrow";
                break;


            case "silence":
                //Debug.Log("Do nothing");
                break;
        }
       
    }

    public void Silence()
    {

        switch (mode)
        {
            case "silence":
                //Debug.Log("Cancel silence");
                CancelSilence.Invoke();         
                mode = "default";
                break;


            case "default":
                //Debug.Log("Silence");
                
                foreach(EnemyManager enemy in m_enemies)
                {
                    if (!enemy.IsDead)
                    {
                        
                        sightIcon = enemy.gameObject.transform.Find("Sight").gameObject.GetComponent<MeshRenderer>();
                        sightIcon.material = blackSightMaterial;
                        sightIcon.enabled = true;
                      
                        boxCollider = enemy.GetComponentInParent<BoxCollider>();
                        boxCollider.enabled = true;
                      

                    }
                }
                StartSilence.Invoke();
                if (audioManager != null)
                {
                    audioManager.Play("RollUp");
                }
                
                mode = "silence";
                break;


            case "arrow":
                //Debug.Log("Do nothing");
                break;
        }
    }

    public void ExecuteAbility(GameObject enemy)
    {
       
        switch (mode)
        {
            case "arrow":
               // Debug.Log("Attack Enemy");

                 sightIcon.enabled = false;
                

                if (enemy != null && playerManager != null && !m_gameManager.m_isGamePause)
                {
                    enemy.GetComponent<EnemyManager>().Die();
                   
                    energyManager.DecreaseEnergy(5);
                    audioManager.Play("Shoot");
                    playerManager.FinishTurn();
                    CancelArrow.Invoke();
                    mode = "default";
                }
                else
                {
                    Debug.Log("REFERENCE NOT FOUND");
                }
                break;




            case "silence":
                //Debug.Log("Slience Enemy");
                Slience.Invoke();
                mode = "default";
                currentSleepDialog = enemy.gameObject.transform.Find("SleepDialog").gameObject.GetComponent<SpriteRenderer>();


                enemyMover = enemy.GetComponent<EnemyMover>();
                MovementType movementType = enemyMover.movementType;
                enemyManager = enemy.GetComponent<EnemyManager>();
                enemyManager.movementType = movementType;
                enemyMover.movementType = MovementType.Stationary;

                enemySensor = enemy.GetComponent<EnemySensor>();
                enemySensor.isEnabled = false;

                enemyManager.IsEnemySilenced = true;
                PlayerTurnCountSilence = m_gameManager.PlayerTurnCount;

                if (currentSleepDialog != null)
                {
                    currentSleepDialog.enabled = true;
                }
                else
                {
                    Debug.Log("Current Sleep Dialog is not found");
                }
                CancelSilence.Invoke();
                energyManager.DecreaseEnergy(3);
                audioManager.Play("Snore");
                playerManager.FinishTurn();


                break;
        }
    }
}
