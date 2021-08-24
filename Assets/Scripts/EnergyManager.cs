using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public int energyMax;
    private int energy;
    public Text energyText;

    float randomN;
   


    private void Awake()
    {
        energy = energyMax;
        energyText = GetComponent<Text>();
        UpdateEnergyText();
    }


    private void Update()
    {
        if(energy > energyMax)
        {
            energy = energyMax;
            UpdateEnergyText();
        }
    }


    private void UpdateEnergyText()
    {
        energyText.text = energy.ToString() + "/" + energyMax.ToString();
    }


    public void IncreaseEnergy(int amount)
    {
        if (energy < energyMax)
        {
            energy += amount;
            UpdateEnergyText();
        }
    }

    public void DecreaseEnergy(int amount)
    {
        if (energy > 0)
        {
            energy -= amount;
            UpdateEnergyText();
        }
    }

    public int IncreaseEnergyRandom(float p, int min, int max)
    {
        randomN = Random.Range(0f, 1f);

        Debug.Log(randomN);

        if (randomN < p)
        {
            energy += max;
            UpdateEnergyText();
            return max;
        }
        else
        {
            energy += min;
            UpdateEnergyText();
            return min;
        }

       
    }
    
}
