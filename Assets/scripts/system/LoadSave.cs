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
    private void LoadFromFirebase(int slot)
    {
        DataManager.instance.nowSlot = slot;
        //DataManager.instance.Load();
    }

    public void Slot(int number)
    {
        Debug.Log(DataManager.instance.id);
        DataManager.instance.nowSlot = number;

        DataManager.instance.Load(() =>
        {
            if (DataManager.instance.nowPlayer.stage != null)
            {
                SceneManager.LoadScene(DataManager.instance.nowPlayer.stage);
            }
            else
            {
                Debug.Log("1");
                SceneManager.LoadScene("AISLE1");
            }
        });
    }
    public void BackToMain()
    {
        SceneManager.LoadScene("MAIN");
    }
}