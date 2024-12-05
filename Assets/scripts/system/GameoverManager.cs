using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverManager : MonoBehaviour
{
    public void Start()
    {

        //TODO 패널로 바꾸면 어떻게 수정하지?
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
