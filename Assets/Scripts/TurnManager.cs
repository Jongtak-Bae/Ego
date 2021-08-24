﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    protected GameManager m_gameManager;

    protected bool m_isTurnComplete = false;

    public bool isTurnComplete { get { return m_isTurnComplete; } set { m_isTurnComplete = value; } }

    protected virtual void Awake()
    {
        m_gameManager = Object.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    // complete the turn and notify the GameManager
    public virtual void FinishTurn()
    {
        m_isTurnComplete = true;

        if (m_gameManager != null) {
            m_gameManager.UpdateTurn();

        }// update the GameManager
    }
}
