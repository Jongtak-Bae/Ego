using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackManager : MonoBehaviour
{
    

    private AbilityManager abilityManager;
 

    private void Awake()
    {
       

        abilityManager = Object.FindObjectOfType<AbilityManager>().GetComponent<AbilityManager>();
        
    }
    private void OnMouseDown()
    {

        abilityManager.ExecuteAbility(gameObject);

       

    }
}


