using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Google.MiniJSON;

public class PlayerData
{
    public string id;
    public int slot;
    public string stage;
    public string time;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public string previousScene;
    public PlayerData nowPlayer = new PlayerData();
    public string path;
    public int nowSlot;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/save/save";
    }
    void Start()
    {
        FirebaseAuthManager.Instance.Init();
    }

    public void Save()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + DataManager.instance.nowPlayer.slot.ToString(), data);
    }

    public void Load()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
