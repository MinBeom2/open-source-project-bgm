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
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i+1}"))
            {
                savefile[i] = true;
                DataManager.instance.nowSlot = i+1;
                DataManager.instance.Load();
            }
        }
        DataManager.instance.DataClear();
    }
    public void Slot(int number)
    {
        DataManager.instance.nowPlayer.id = FirebaseAuthManager.Instance.GetUserID(); // 사용자 ID 저장
        DataManager.instance.nowPlayer.slot = number; // 슬롯 번호 저장
        DataManager.instance.nowPlayer.stage = DataManager.instance.previousScene; // 현재 스테이지 번호 저장
        DataManager.instance.nowPlayer.time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // 저장 시간 기록

        DataManager.instance.Save();
    }

    public void BackToPreviousScene()
    {
        string previousScene = DataManager.instance.previousScene;
        SceneManager.LoadScene(previousScene);
    }
}
