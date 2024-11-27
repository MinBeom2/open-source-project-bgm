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
        DataManager.instance.nowSlot = number;

        DataManager.instance.Load();
    }


    public void BackToMain()
    {
        SceneManager.LoadScene("MAIN");
    }
}