using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(MaskableGraphic))]


public class ScreenFader : MonoBehaviour
{
    public Color solidColor = Color.white; // new Color(1f,1f, 1f, 1f)
    public Color clearColor = new Color(1f, 1f, 1f, 0f);

    public float delay = 0.5f;
    public float timeToFade = 1f;
    public iTween.EaseType easeType = iTween.EaseType.easeOutExpo;

    public UnityEvent FadeOnEnvent;




    MaskableGraphic graphic;

    private void Awake()
    {
        graphic = GetComponent<MaskableGraphic>();
        

    }

    void UpdateColor(Color newColor)
    {
        graphic.color = newColor;
    }

    public void StartFadeOff()
    {
        StartCoroutine(FadeOff());
    }



    public IEnumerator FadeOff()
    {
      
        
            iTween.ValueTo(gameObject, iTween.Hash(
           "from", solidColor,
           "to", clearColor,
           "time", timeToFade,
           "delay", delay,
           "easetype", easeType,
           "onupdatetarget", gameObject,
           "onupdate", "UpdateColor"
           ));
            yield return new WaitForSeconds(1f);

            gameObject.SetActive(false);
        

       
        
        
    }


    public void ResetColor()
    {
        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
    }

    public void FadeOn()

    {
        
        
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", clearColor,
            "to", solidColor,
            "time", timeToFade,
            "delay", delay,
            "easetype", easeType,
            "onupdatetarget", gameObject,
            "onupdate", "UpdateColor"
            ));

        FadeOnEnvent.Invoke();
    }


}
