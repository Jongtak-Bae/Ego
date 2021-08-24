using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;

[System.Serializable]
public enum Turn
{
    Player,
    Enemy
}

public class GameManager : MonoBehaviour
{
 
    Board m_board;
    PlayerManager m_player;
    List<EnemyManager> m_enemies;
    Turn m_currentTurn = Turn.Player;
  
    public Turn CurrentTurn { get { return m_currentTurn; } }


    bool m_hasLevelStarted = false;
    public bool HasLevelStarted { get => m_hasLevelStarted; set => m_hasLevelStarted = value; }

    bool m_isGamePlaying = false;
    public bool IsGamePlaying { get => m_isGamePlaying; set => m_isGamePlaying = value; }

    bool m_isGameOver = false;
    public bool IsGameOver { get => m_isGameOver; set => m_isGameOver = value; }

    bool m_hasLevelFinished = false;
    public bool HasLevelFinished { get => m_hasLevelFinished; set => m_hasLevelFinished = value; }
    

    bool m_isStartButtonCliked = false;
    public bool IsStartButtonCliked { get => m_isStartButtonCliked; set => m_isStartButtonCliked = value; }
    

    public float delay = 0f;

    public bool m_isGamePause = false;

    int playerTurnCount = 0;
    public int PlayerTurnCount { get => playerTurnCount; }



    public UnityEvent winLevelEvent;


    public AudioManager audioManager;
    private AbilityManager abilityManager;


    private void Awake()
    {
       

        // Avoid duplicate GameManager
       
        //if(gameManagerInstance == null)
        //{
        //    gameManagerInstance = this;
        //}
        //else
        //{
        //    DestroyObject(gameObject);
        //}

        //DontDestroyOnLoad(this.gameObject);

        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        m_player = Object.FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
        EnemyManager[] enemies = Object.FindObjectsOfType<EnemyManager>() as EnemyManager[];
        m_enemies = enemies.ToList();
        abilityManager = Object.FindObjectOfType<AbilityManager>().GetComponent<AbilityManager>();
        audioManager = Object.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

    }


    void Start()
    {
        if(m_player != null && m_board != null)
        {
            StartCoroutine(PlayLevelRoutine());
        }
        else
        {
            Debug.LogWarning("GAMEMANAGER Error: no player or board found!");
        }
    }


 
   
    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
       
        StartCoroutine(PlayLevelRoutine());
    }

 

    public void LoseLevel()
    {
        StartCoroutine(LoseLevelRoutine());

    }

    
   public void GoToLevelSelector()
    {
       
        SceneManager.LoadScene(1);
    }

    public void GoToStartScreen()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator PlayLevelRoutine()
    {
        

        Debug.Log("PLAY LEVEL");

       
        m_isGamePlaying = true;

        yield return new WaitForSeconds(delay);

        if(m_player != null)
        {
            m_player.playerInput.InputEnabled = true;

        }
        else
        {
            Debug.LogWarning("Player is not found");
        }
        

           
            if (m_board != null)
            {
                m_board.InitBoard();

                yield return new WaitForSeconds(1f);
                m_board.DrawGoal();
            }
            else
            {
                Debug.LogWarning("Board is not found!");
            }
            
            audioManager.Play("Birds");
        

        while (!m_isGameOver)
        {
            yield return null;
            m_isGameOver = IsWinner();

        }
        Debug.Log("WIN! ==========");
        winLevelEvent.Invoke();
        audioManager.Play("Win");

    }
    IEnumerator LoseLevelRoutine()
    {
        m_isGameOver = true;

        yield return new WaitForSeconds(1.5f);


        Debug.Log("LOSE! ==================");

        RestartLevel();
    }

    //IEnumerator EndLevelRoutine()
    //{
    //    Debug.Log("END LEVEL");
    //    m_player.playerInput.InputEnabled = false;

    //    if(endLevelEvent != null)
    //    {
    //        endLevelEvent.Invoke();
    //    }

   
    //    while (!m_hasLevelFinished)
    //    {
         
    //        yield return null;
    //    }

    //    yield return new WaitForSeconds(1f);
    //    m_hasLevelStarted = true;

    //    RestartLevel();
    //}

 

    bool IsWinner()
    {
        if(m_board.PlayerNode != null)
        {
            return (m_board.PlayerNode == m_board.GoalNode);
        }
        return false;
    }

    

    //--------Turn Management --------//

    void PlayPlayerTurn()
    {
        m_currentTurn = Turn.Player;
        m_player.isTurnComplete = false;
       
        playerTurnCount++;//count player turn
        Debug.Log(playerTurnCount);
        // allow Player to move
    }

    void PlayEnemyTurn()
    {
        m_currentTurn = Turn.Enemy;

        foreach (EnemyManager enemy in m_enemies)
        {
            if (PlayerTurnCount-abilityManager.PlayerTurnCountSilence == 2 && abilityManager.PlayerTurnCountSilence != 0 && enemy.IsEnemySilenced)
            {
               
               
                if (enemy != null && !enemy.IsDead )
                {
                    enemy.GetComponent<EnemyMover>().movementType = enemy.GetComponent<EnemyManager>().movementType;
                
                    enemy.GetComponent<EnemySensor>().isEnabled = true;
                    enemy.gameObject.transform.Find("SleepDialog").gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    enemy.IsEnemySilenced = false;
                    enemy.isTurnComplete = false;
                    Debug.Log(enemy);
                   
                    enemy.PlayTurn();// play each enemy's turn
                   
                }

            }
            else
            {
                if (enemy != null && !enemy.IsDead)
                {
                    
                    enemy.isTurnComplete = false;
                   
                    enemy.PlayTurn();// play each enemy's turn
                   
                }

            }
           
        }


    }

    bool IsEnemyTurnComplete()
    {
        foreach (EnemyManager enemy in m_enemies)
        {
            if (enemy.IsDead)
            {
                continue;
            }
            if (!enemy.isTurnComplete)
            {
                return false;
            }
        }
        return true;

    }

    bool AreEnemiesAllDead()
    {
        foreach(EnemyManager enemy in m_enemies)
        {
            if (!enemy.IsDead)
            {
                return false;
            }
        }
        return true;
    }

    public void UpdateTurn()
    {
        if(m_currentTurn == Turn.Player && m_player != null)
        {
            if (m_player.isTurnComplete && !AreEnemiesAllDead())
            {
                PlayEnemyTurn();// switch to EnemyTurn and play enemies
            }
        }

        else if (m_currentTurn == Turn.Enemy)
        {
            if (IsEnemyTurnComplete())
            {
                PlayPlayerTurn(); //if enemy turn is complete, play player turn
            }
            
        }
    }

   


   
}
