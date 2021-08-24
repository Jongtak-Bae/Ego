using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    public Text textContent;

    public string[] breathState = { "Hold", "Inhale","Hold", "Exhale" };
    public bool isBreathing = true;


  
    private void Awake()
    {
        textContent = gameObject.GetComponent<Text>();
    }


    public void ChangeTextContent()
    {
        isBreathing = true;
        StartCoroutine(ChangeTextContentRoutine());
    }

   IEnumerator ChangeTextContentRoutine()
    {
        while (isBreathing)
        {
            for (int i = 0; i < 4; i++)
            {
                if (isBreathing)
                {
                    textContent.text = breathState[i];
                    yield return new WaitForSeconds(4f);

                }

            }


        }


    }


  
    //IEnumerator  ChangeTextContentRoutine()
    //{

    //    while (isBreathing)
    //    {
            
    //        textContent.text = isInhale ? "exhale" : "inhale";
    //        isInhale = !isInhale;
    //        yield return new WaitForSeconds(2f);
    //    }    
        
    //}




    public void StopChangeText()
    {
        isBreathing = false;
    }
    public void ShowCompleteText()
    {
        textContent.text = "Completed!";
    }

}
