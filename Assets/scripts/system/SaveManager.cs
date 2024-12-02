using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using PimDeWitte.UnityMainThreadDispatcher;
using UnityEngine.EventSystems;


public class SaveManager : MonoBehaviour
{
    public TextMeshProUGUI[] slotTime;
    public TextMeshProUGUI[] slotStage;


    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EventSystem.current.SetSelectedGameObject(null);

        for (int i = 0; i < 3; i++)
        {
            if (!string.IsNullOrEmpty(DataManager.instance.playerSlots.Slots[i].Time))
            {
                slotTime[i].text = DataManager.instance.playerSlots.Slots[i].Time;
                if (DataManager.instance.playerSlots.Slots[i].Stage == "AISLE1")
                    slotStage[i].text = "ENDLESS PASSAGE";

                if (DataManager.instance.playerSlots.Slots[i].Stage == "AISLE2")
                    slotStage[i].text = "PLAYGROUND";

                if (DataManager.instance.playerSlots.Slots[i].Stage == "AISLE3")
                    slotStage[i].text = "ABRUPTIVE ATTACK";
            }
            else
            {
                slotStage[i].text = "NEW GAME";
                slotTime[i].text = "????-??-?? ??:??:??";
            }
        }
    }


    public void Slot(int number)
    {
        DataManager.instance.nowSlot = number;

        DataManager.instance.nowPlayer.stage = DataManager.instance.previousScene;
        DataManager.instance.nowPlayer.time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        DataManager.instance.playerSlots.Slots[number - 1].Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        DataManager.instance.playerSlots.Slots[number - 1].Stage = DataManager.instance.previousScene;

        slotTime[number - 1].text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        if (DataManager.instance.playerSlots.Slots[number - 1].Stage == "AISLE1")
            slotStage[number - 1].text = "ENDLESS PASSAGE";

        if (DataManager.instance.playerSlots.Slots[number - 1].Stage == "AISLE2")
            slotStage[number - 1].text = "PLAYGROUND";

        if (DataManager.instance.playerSlots.Slots[number - 1].Stage == "AISLE3")
            slotStage[number - 1].text = "ABRUPTIVE ATTACK";

        DataManager.instance.Save();
    }


    public void BackToPreviousScene()
    {
        SceneManager.LoadScene(DataManager.instance.previousScene);
    }
}
