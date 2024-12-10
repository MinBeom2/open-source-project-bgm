using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverManager : MonoBehaviour
{
    public void Start()
    {


    }

    public void Restart()
    {
        Debug.Log(DataManager.instance.nowPos.positionX);
        SceneManager.LoadScene(DataManager.instance.nowPlayer.stage);
    }
    public void QuitToMain()
    {
        SceneManager.LoadScene("MAIN");
    }
}
