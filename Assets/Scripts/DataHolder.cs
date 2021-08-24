using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class DataHolder: MonoBehaviour
{
    [Header("Energy Increase")]
    public int min;
    public int max;
    public float p;
    public EnergyManager energyManager;
    public Text completeEnergyText;
    public Text energyGainText;

    [Header("Time")]
    public float time;
    public Text timeText;

    private void Awake()
    {
        SetTime();
        SetEnergyGain();
    }

    private int increseEnergyText;

    public void IncreaseEnergyRandom()
    {
        if (energyManager!= null && completeEnergyText != null)
        {
            increseEnergyText = energyManager.IncreaseEnergyRandom(p, min, max);
            completeEnergyText.text = "+" + increseEnergyText.ToString();
        }
        else
        {
            Debug.LogWarning("energyManger and/or completeEnergyText not found!");
        }
      
    }

    void SetTime()
    {
        timeText.text = time.ToString() + " s";
    }

    void SetEnergyGain() { energyGainText.text = "+ "+ min.ToString() + " or " + max.ToString(); }

   


}
