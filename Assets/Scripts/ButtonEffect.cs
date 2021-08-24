using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{
    bool showBorder = true;
    public GameObject borderImageObject;
    private Image borderImage;

    private GraphicMover graphicMover;
    private void Awake()
    {
        borderImage = borderImageObject.GetComponent<Image>();
        graphicMover = gameObject.transform.parent.GetComponent<GraphicMover>();
    }

    public void ShowButtonEffect()
    {
   

        if (showBorder)
        {
            borderImage.enabled = true;
            showBorder = false;

            graphicMover.moveEnabled = true;

            graphicMover.mode = GraphicMoverMode.RotateTo;
            graphicMover.Move();
           
        }
        else
        {
           
            borderImage.enabled = false;
            showBorder = true;
            graphicMover.mode = GraphicMoverMode.RotateFrom;
            graphicMover.moveEnabled = true;
          
            graphicMover.Move();
           
        }
    }






  
}
