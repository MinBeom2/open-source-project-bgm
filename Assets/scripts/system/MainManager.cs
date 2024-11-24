using UnityEngine;
using UnityEngine.SceneManagement;
using PimDeWitte.UnityMainThreadDispatcher;
using Firebase.Database;
using Firebase.Extensions;

public class MainManager : MonoBehaviour
{
    public void QuitGame()
    {
        // 게임을 종료
        Debug.Log("게임 종료");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("LOAD_SCENE");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("AISLE1");
    }

    public void LogOut()
    {
        string deviceId = SystemInfo.deviceUniqueIdentifier; // 현재 기기 ID 가져오기
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference.Child("device").Child(deviceId).Child("userId").RemoveValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("로그아웃 완료: Firebase에서 해당 기기의 id가 삭제되었습니다.");
                }
                else
                {
                    Debug.LogError("로그아웃 실패: " + task.Exception?.Message);
                }
            });

        UnityMainThreadDispatcher.Instance().Enqueue(() => SceneManager.LoadScene("Login_scene"));
    }
}
