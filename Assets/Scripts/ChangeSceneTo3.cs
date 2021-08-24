using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneTo3 : MonoBehaviour
{

    public void StartChangeScene()
    {

        SceneManager.LoadScene(3);

    }



    private void OnMouseDown()
    {
        StartChangeScene();
    }



}
