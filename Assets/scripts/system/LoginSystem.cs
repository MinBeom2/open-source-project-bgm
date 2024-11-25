using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using PimDeWitte.UnityMainThreadDispatcher;
using Firebase.Database;
using Firebase.Extensions;


public class LoginSystem : MonoBehaviour
{
    private FirebaseAuth auth;
    public TMP_InputField email;
    public TMP_InputField password;
    private DatabaseReference reference;
    private string deviceId;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        deviceId = SystemInfo.deviceUniqueIdentifier;

        if (DataManager.instance == null)
        {
            Debug.LogError("DataManager 인스턴스가 null입니다.");
            return;
        }

        reference.Child("device").Child(deviceId).Child("userId").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Firebase에서 데이터를 불러오는데 오류 발생");
                return;
            }

            if (task.IsCompleted)
            {
                // 비동기 작업이 완료된 경우
                Debug.Log("Firebase 데이터 가져오기 완료");
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    string savedUserId = snapshot.Value.ToString();
                    Debug.Log($"Firebase에서 저장된 userId: {savedUserId}");
                    DataManager.instance.id = savedUserId;

                    UnityMainThreadDispatcher.Instance().Enqueue(() => SceneManager.LoadScene("MAIN"));
                }
                else
                {
                    Debug.Log("Firebase에 저장된 userId가 없습니다.");
                }
            }
        });
        Debug.Log("?");

    }



    public void Login()
    {
        auth.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("로그인 취소");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("로그인 실패");
                return;
            }

            FirebaseUser user = task.Result.User;
            SaveDeviceUserMapping(user.UserId);
            DataManager.instance.id = user.UserId;
            Debug.Log("datamanager id" + DataManager.instance.id);
            UnityMainThreadDispatcher.Instance().Enqueue(() => SceneManager.LoadScene("MAIN"));
        });
    }

    public void LoadCreate()
    {
        SceneManager.LoadScene("CREATE");
    }

    private void SaveDeviceUserMapping(string userId)
    {
        reference.Child("device").Child(deviceId).Child("userId").SetValueAsync(userId).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log($"deviceId와 userId 매핑 저장 완료: {deviceId} -> {userId}");
            }
            else
            {
                Debug.LogError("deviceId와 userId 매핑 저장 실패: " + task.Exception?.Message);
            }
        });
    }
}
