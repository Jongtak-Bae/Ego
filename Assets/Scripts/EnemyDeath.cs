using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemyDeath : MonoBehaviour
{
    public Vector3 offscreenOffset = new Vector3(0f, 10f, 0f);

    Board m_board;

    public float deathDelay = 0f;
    public float offscreenDelay = 1f;

    public float iTweenDelay = 0f;
    public iTween.EaseType EaseType = iTween.EaseType.easeInOutQuint;
    public float moveTime = 0.5f;

    private List<GameObject> childrens = new List<GameObject>();

    bool isDead = false;

   
    void Awake()
    {
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        Transform[] allchildren = this.transform.GetComponentsInChildren<Transform>(true);
       foreach(Transform child in allchildren)
        {
            childrens.Add(child.gameObject);
        }
    }

  

    public void MoveOffBoard(Vector3 target)
    {
        iTween.MoveTo(gameObject, iTween.Hash(
            "x", target.x,
            "y", target.y,
            "z", target.z,
            "delay", iTweenDelay,
            "easetype", EaseType,
            "time", moveTime

            ));
    }



    public void Die()
    {
        StartCoroutine(DieRoutine());
        
    }

    IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(deathDelay);

        Vector3 offscreenPos = transform.position + offscreenOffset;

        MoveOffBoard(offscreenPos);
        yield return new WaitForSeconds(moveTime + offscreenDelay);
        if(m_board.capturePositions.Count != 0 && m_board.CurrentCapturePosition<m_board.capturePositions.Count)
        {
            Vector3 capturePos = m_board.capturePositions[m_board.CurrentCapturePosition].position;
            transform.position = capturePos + offscreenOffset;

            MoveOffBoard(capturePos);
            yield return new WaitForSeconds(moveTime);

            m_board.CurrentCapturePosition++;
            m_board.CurrentCapturePosition = Mathf.Clamp(m_board.CurrentCapturePosition,
                0, m_board.capturePositions.Count - 1);


        }
        

        childrens[0].SetActive(true);
        childrens[1].SetActive(false);
        childrens[2].SetActive(false);
        childrens[3].SetActive(false);
    }
}
