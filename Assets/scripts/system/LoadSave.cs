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
        //TODO
        Debug.Log("datamanager id" + DataManager.instance.id);

    }

    public void Slot(int number)
    {
        Debug.Log(DataManager.instance.id);
        DataManager.instance.nowSlot = number;

        DataManager.instance.Load(() =>
        {
            if (DataManager.instance.nowPlayer.stage != null)
            {
                // 로드된 스테이지로 이동
                SceneManager.LoadScene(DataManager.instance.nowPlayer.stage);

                // 씬이 로드된 이후 위치와 회전을 복원
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else
            {
                Debug.Log("1");
                SceneManager.LoadScene("AISLE1");
            }
        });
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            DataManager.instance.RestorePlayerPosition(player);
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }




    public void BackToMain()
    {
        SceneManager.LoadScene("MAIN");
    }
}