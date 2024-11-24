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
            //TODO: 로컬에서 파이어베이스로 바꾸기
            if (File.Exists(DataManager.instance.path + $"{i+1}"))
            {
                savefile[i] = true;
                DataManager.instance.nowSlot = i+1;
                //DataManager.instance.Load();
            }
        }
        DataManager.instance.DataClear();
    }

    
    public void Slot(int number)
    {
        DataManager.instance.nowPlayer.slot = number; 
        DataManager.instance.nowPlayer.stage = DataManager.instance.previousScene; 
        DataManager.instance.nowPlayer.time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        DataManager.instance.Save();
    }

    public void BackToPreviousScene()
    {
        SceneManager.LoadScene(DataManager.instance.previousScene);
    }
}
