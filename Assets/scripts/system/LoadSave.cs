using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.EventSystems;

public class LoadSave : MonoBehaviour
{
    public TextMeshProUGUI[] slotTime;
    public TextMeshProUGUI[] slotStage;


    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);

        for (int i = 0; i < 3; i++)
        {
            if (!string.IsNullOrEmpty(DataManager.instance.playerSlots.Slots[i].Time))
            {
                slotTime[i].text = DataManager.instance.playerSlots.Slots[i].Time;
                if (DataManager.instance.playerSlots.Slots[i].Stage == "AISLE1")
                    slotStage[i].text = "ENDLESS PASSAGE";

                else if (DataManager.instance.playerSlots.Slots[i].Stage == "AISLE2")
                    slotStage[i].text = "PLAYGROUND";

                else if (DataManager.instance.playerSlots.Slots[i].Stage == "AISLE3")
                    slotStage[i].text = "ABRUPTIVE ATTACK";

                else
                    slotStage[i].text = "???";

            }
            else
            {
                slotStage[i].text = "NEW GAME";
                slotTime[i].text = "????-??-?? ??:??:??";
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MAIN");
        }
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