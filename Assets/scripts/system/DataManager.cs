using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Google.MiniJSON;
using Firebase;
using PimDeWitte.UnityMainThreadDispatcher;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

public class PlayerPos
{
    public float positionX;
    public float positionY;
    public float positionZ;
    public float rotationY;
    public PlayerPos(float x, float y, float z, float rotY)
    {
        positionX = x;
        positionY = y;
        positionZ = z;
        rotationY = rotY;
    }

}

public class PlayerData
{
    public string stage;
    public string time;
}

public class PlayerSlot
{
    public string Stage { get; set; }
    public string Time { get; set; }
}

public class PlayerSlots
{
    public List<PlayerSlot> Slots { get; set; } = new List<PlayerSlot>();

    public PlayerSlots()
    {
        for (int i = 0; i < 3; i++)
        {
            Slots.Add(new PlayerSlot());
        }
    }
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public string previousScene;
    public PlayerData nowPlayer = new PlayerData();
    public PlayerPos nowPos = new PlayerPos(1.1f, 0f, 11.13f, 0f);
    public PlayerSlots playerSlots = new PlayerSlots();

    public int nowSlot;
    private DatabaseReference reference;
    public string id;
    public Vector3 playerPosition;

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
        string json1 = JsonUtility.ToJson(nowPlayer);
        reference.Child("users").Child(id).Child("slots").Child(nowSlot.ToString()).Child("PlayerData").SetRawJsonValueAsync(json1)
            .ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("슬롯 데이터가 Firebase에 저장되었습니다.");
                }
                else
                {
                    Debug.LogError("슬롯 데이터 저장 실패: " + task.Exception?.Message);
                }
            });

        string json2 = JsonUtility.ToJson(nowPos);
        reference.Child("users").Child(id).Child("slots").Child(nowSlot.ToString()).Child("PlayerPos").SetRawJsonValueAsync(json2)
            .ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("슬롯 위치 데이터가 Firebase에 저장되었습니다.");
                }
                else
                {
                    Debug.LogError("슬롯 위치 데이터 저장 실패: " + task.Exception?.Message);
                }
            });
    }

    public void Load()
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
                    string playerDataJson = snapshot.Child("PlayerData").GetRawJsonValue();
                    if (!string.IsNullOrEmpty(playerDataJson))
                    {
                        nowPlayer = JsonUtility.FromJson<PlayerData>(playerDataJson);
                    }

                    string playerPosJson = snapshot.Child("PlayerPos").GetRawJsonValue();
                    if (!string.IsNullOrEmpty(playerPosJson))
                    {
                        nowPos = JsonUtility.FromJson<PlayerPos>(playerPosJson);
                    }

                    Debug.Log("Firebase에서 데이터 불러오기 완료");

                }

                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    if (nowPlayer?.stage != null)
                    {
                        SceneManager.LoadScene(nowPlayer.stage);
                    }
                    else
                    {
                        SceneManager.LoadScene("AISLE1");
                    }
                });
            }
        });
    }

    public void LoadSlotData(int slotNumber, System.Action<PlayerData> onComplete)
    {
        reference.Child("users").Child(id).Child("slots").Child(slotNumber.ToString()).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"슬롯 {slotNumber} 데이터를 불러오는데 실패했습니다.");
                onComplete?.Invoke(null);
                return;
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    PlayerData playerData = null;

                    string playerDataJson = snapshot.Child("PlayerData").GetRawJsonValue();

                    if (!string.IsNullOrEmpty(playerDataJson))
                        playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);

                    onComplete?.Invoke(playerData);
                }
                else
                {
                    Debug.Log($"슬롯 {slotNumber}에 데이터가 없습니다.");
                    onComplete?.Invoke(null);
                }
            }
        });
    }
}