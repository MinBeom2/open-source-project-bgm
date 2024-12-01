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
using Firebase;

public class LoginSystem : MonoBehaviour
{
    private FirebaseAuth auth;
    public TMP_InputField email;
    public TMP_InputField password;
    private DatabaseReference reference;
    private string deviceId;
    public TMP_Text errorMessageText;

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
                Debug.Log("Firebase 데이터 가져오기 완료");
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    string savedUserId = snapshot.Value.ToString();
                    Debug.Log($"Firebase에서 저장된 userId: {savedUserId}");
                    DataManager.instance.id = savedUserId;
                    PlayerSync();
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
            if (task.IsFaulted)
            {
                FirebaseException firebaseEx = task.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                string errorMessage = GetErrorMessage(errorCode);
                DisplayErrorMessage(errorMessage);
                return;
            }

            FirebaseUser user = task.Result.User;
            SaveDeviceUserMapping(user.UserId);
            DataManager.instance.id = user.UserId;
            Debug.Log("datamanager id" + DataManager.instance.id);
            PlayerSync();
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

    public void PlayerSync()
    {
        for (int i = 0; i < 3; i++)
        {
            int slotIndex = i + 1;
            DataManager.instance.LoadSlotData(slotIndex, (playerData) =>
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    if (playerData != null)
                    {
                        DataManager.instance.playerSlots.Slots[slotIndex - 1].Stage = playerData.stage;
                        DataManager.instance.playerSlots.Slots[slotIndex - 1].Time = playerData.time;
                        Debug.Log($"슬롯 {slotIndex}: 스테이지 {playerData.stage}, 시간 {playerData.time}");
                    }
                    else
                    {
                        DataManager.instance.playerSlots.Slots[slotIndex - 1].Stage = null;
                        DataManager.instance.playerSlots.Slots[slotIndex - 1].Time = "";
                    }
                });
            });
        }
    }

    private void DisplayErrorMessage(string message)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            errorMessageText.text = message;
        });
    }

    private string GetErrorMessage(AuthError errorCode)
    {
        switch (errorCode)
        {
            case AuthError.MissingEmail:
                return "Email is required.";
            case AuthError.MissingPassword:
                return "Password is required.";
            case AuthError.InvalidEmail:
                return "Invalid email address.";
            default:
                return "Login failed. Please try again.";
        }
    }
}