using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    public Text[] slotTime;
    public int slotStage;

    bool[] savefile = new bool[3];
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        for (int i = 0; i < 3; i++)
        {
            //TODO: 로컬에서 파이어베이스로 바꾸기
            if (File.Exists(DataManager.instance.path + $"{i + 1}"))
            {
                savefile[i] = true;
                DataManager.instance.nowSlot = i + 1;
                //DataManager.instance.Load();
            }
        }
    }


    public void Slot(int number)
    {
        DataManager.instance.nowSlot = number;

        DataManager.instance.nowPlayer.stage = DataManager.instance.previousScene;
        DataManager.instance.nowPlayer.time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        PlayerPos savedPosition = DataManager.instance.nowPos;
        Debug.Log($"슬롯 {number}에 데이터 저장: 스테이지 {DataManager.instance.nowPlayer.stage}, 시간 {DataManager.instance.nowPlayer.time}, 위치 ({savedPosition.positionX}, {savedPosition.positionY}, {savedPosition.positionZ}), 회전 Y: {savedPosition.rotationY}");

        DataManager.instance.Save();
    }

    public void BackToPreviousScene()
    {
        Debug.Log("1");
        SceneManager.LoadScene(DataManager.instance.previousScene);
    }
}
