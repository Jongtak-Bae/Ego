using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public UnityEvent EnergyRefillingScreen;

    public UnityEvent BreathScreen;

    public UnityEvent MeditationScreen;

    public UnityEvent BackToGame;

    public UnityEvent StopMeditation;

    public UnityEvent StopBreath;


    [Header("Change Order")]
    public GameObject child;
    public Transform oldParent;
    public Transform newParent;

    private int currentScreen;

    public void StartEnergyRefilling()
    {
        EnergyRefillingScreen.Invoke();
        SetNewParent(newParent);
        currentScreen = 0;
    }


    public void SetNewParent(Transform newParent)
    {
        child.transform.SetParent(newParent);
    }


    public void StartBreath()
    {
        BreathScreen.Invoke();
        currentScreen = 1;
    }

    public void StartMeditation()
    {
        MeditationScreen.Invoke();
        currentScreen = 2;
    }

 

   


    public void Back()
    {
        switch (currentScreen)
        {
            case 0:
                Debug.Log("Stop Energy Refilling");
                BackToGame.Invoke();
                SetNewParent(oldParent);
                break;
            case 1:
                Debug.Log("Stop Breath");
                StopBreath.Invoke();
                currentScreen = 0;
                break;
            case 2:
                Debug.Log("Stop Meditation");
                StopMeditation.Invoke();
                currentScreen = 0;
                break;


        }
    }

}
