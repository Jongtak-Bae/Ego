using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneTo2 : MonoBehaviour
{

    public void StartChangeScene()
    {

        SceneManager.LoadScene(2);

    }



    private void OnMouseDown()
    {
        SceneManager.LoadScene(2);
    }






}
