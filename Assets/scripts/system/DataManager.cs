using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Google.MiniJSON;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class PlayerData
{
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
    private DatabaseReference reference;
    public string id;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else if (instance != this)
        {
            return;
        }
        
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            reference = FirebaseDatabase.DefaultInstance.RootReference;
            FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(true);
        });
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(nowPlayer);
        reference.Child("users").Child(id).Child("slots").Child(nowPlayer.slot.ToString()).SetRawJsonValueAsync(json);
    }

    public void Load(System.Action callback)
    {
        reference.Child("users").Child(id).Child("slots").Child(nowSlot.ToString()).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Firebase에서 데이터를 불러오는데 오류 발생");
            }

            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string json = snapshot.GetRawJsonValue();
                    nowPlayer = JsonUtility.FromJson<PlayerData>(json);
                    Debug.Log("Firebase에서 데이터 불러오기 완료");
                }
                callback?.Invoke();
            }
        });
    }


    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
