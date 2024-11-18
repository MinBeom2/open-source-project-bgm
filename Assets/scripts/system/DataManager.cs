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
    private DatabaseReference reference;

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
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            reference = FirebaseDatabase.DefaultInstance.RootReference;
            FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(true);  // 오프라인 캐시 활성화
        });
    }
    void Start()
    {
        FirebaseAuthManager.Instance.Init();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(nowPlayer);
        reference.Child("users").Child(FirebaseAuthManager.Instance.GetUserID()).Child("slots").Child(nowPlayer.slot.ToString()).SetRawJsonValueAsync(json);
    }

    public void Load()
    {
        reference.Child("users").Child(FirebaseAuthManager.Instance.GetUserID()).Child("slots").Child(nowSlot.ToString()).GetValueAsync().ContinueWithOnMainThread(task =>
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
            }
        });
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
