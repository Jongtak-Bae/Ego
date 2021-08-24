using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Text))]
public class LevelLabel : MonoBehaviour
{
    Text m_text;

    private void Awake()
    {
        m_text = GetComponent<Text>();

        if(m_text != null)
        {
            m_text.text = SceneManager.GetActiveScene().name;
        }
    }
}
