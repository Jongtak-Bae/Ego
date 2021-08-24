using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    public Vector3 directionToSearch = new Vector3(0f, 0f, 2f);

    Node m_nodeToSearch;
    Board m_board;

    public bool isEnabled = true;
    bool m_foundPlayer = false;
    public bool FoundPlayer { get { return m_foundPlayer; } }

    private void Awake()
    {
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
    }


    public void EnableSensor()
    {
        isEnabled = true;
    }

    public void DisableSensor()
    {
        isEnabled = false;
    }
    public void UpdateSensor(Node enemyNode)
    {
        if (isEnabled)
        {
            Vector3 worldSpacePositionToSearch = transform.TransformVector(directionToSearch) + transform.position;

            if (m_board != null)
            {
                m_nodeToSearch = m_board.FindNodeAt(worldSpacePositionToSearch);
                if (!enemyNode.LinkedNodes.Contains(m_nodeToSearch))
                {

                    m_foundPlayer = false;
                    return;
                }
                if (m_nodeToSearch == m_board.PlayerNode)
                {
                    m_foundPlayer = true;
                }
            }
        }

       
    }
  
    // for testing only
    //void Update()
    //{
    //    UpdateSensor();
    //}


}
