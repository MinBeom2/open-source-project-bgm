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
            savefile[i] = false;  //todo
            LoadFromFirebase(i + 1);
        }
    }
    private void LoadFromFirebase(int slot)
    {
        DataManager.instance.nowSlot = slot;
        DataManager.instance.Load();
    }

    public void Slot(int number)
    {
        DataManager.instance.nowSlot = number;
        DataManager.instance.Load();  // Firebase에서 데이터 로드

        // 데이터가 있으면 게임을 로드
        if (DataManager.instance.nowPlayer != null)
        {
            Debug.Log(DataManager.instance.nowPlayer.stage);
            SceneManager.LoadScene(DataManager.instance.nowPlayer.stage);
        }
        else
        {
            // 세이브가 없다면 새로운 게임 시작
            SceneManager.LoadScene("AISLE1");
        }
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MAIN");
    }
}