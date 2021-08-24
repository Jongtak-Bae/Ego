using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    bool timerActive = true;
    private float minutes = 0f;
    private float seconds;

    public Text timerText;
    public Image progressBar;

    private float speed;
    private float endSecond;

    public UnityEvent timerFinish;
   private DataHolder dataHolder;

    private void Awake()
    {
        dataHolder = this.GetComponent<DataHolder>();
        if(dataHolder!= null)
        {
            endSecond = dataHolder.time;
            Debug.Log(endSecond);
        }
        else
        {
            Debug.LogWarning("dataHolder not found!");
        }
        
    }
    public void StartTimer()
    {
        timerActive = true;
        StartCoroutine(timerCounting());
    }
        
    public void StopTimer()
    {
        timerActive = false;
        seconds = 0;
        minutes = 0;
    }

   IEnumerator timerCounting()
    {


        while (timerActive)
        {
            yield return new WaitForSeconds(1f);
           
            seconds++;
            speed = (minutes * 60 + seconds) / endSecond;
            progressBar.transform.localScale = new Vector3(speed, 0.1f, 1f);
            if (seconds == 60)
            {
                minutes++;
                seconds = 0;
            }
            timerText.text = minutes.ToString() + " min " + seconds.ToString() + " s";

            if(minutes * 60 + seconds == endSecond)
            {
                timerActive = false;
                timerFinish.Invoke();
            }


        }        
              
     
    
    }

}
