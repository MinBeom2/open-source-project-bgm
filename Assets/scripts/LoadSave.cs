using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class LoadSave : MonoBehaviour
{
    public Text[] slotTime;
    public Text[] text;


    bool[] savefile = new bool[3];

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(DataManager.instance.path + $"{i + 1}");
            if (File.Exists(DataManager.instance.path + $"{i + 1}"))
            {
                Debug.Log("0");
                savefile[i] = true;
                DataManager.instance.nowSlot = i + 1;
                DataManager.instance.Load();

            }
            else
            {
                Debug.Log("1");

            }
        }
        DataManager.instance.DataClear();
    }

    public void Slot(int number)
    {
        DataManager.instance.nowSlot = number;
        if (savefile[number-1]) //세이브가 있을 경우
        {
            DataManager.instance.Load();
            Debug.Log(DataManager.instance.nowPlayer.stage);
            SceneManager.LoadScene(DataManager.instance.nowPlayer.stage);
        }
        else
        {
            SceneManager.LoadScene("AISLE1");
        }
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MAIN");
    }
}