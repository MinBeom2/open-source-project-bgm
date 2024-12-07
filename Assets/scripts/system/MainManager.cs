using UnityEngine;
using UnityEngine.SceneManagement;
using PimDeWitte.UnityMainThreadDispatcher;
using Firebase.Database;
using Firebase.Extensions;
using Unity.VisualScripting;

public class MainManager : MonoBehaviour
{
    [SerializeField] private GameObject confirmPanel;
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject OptionPanel;

    AudioManager audioManager;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        audioManager.musicSource.clip = audioManager.mainBackground;
        audioManager.musicSource.Play();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (confirmPanel.activeSelf)
            {
                Cancel();
            }

            else if (OptionPanel.activeSelf)
            {
                OptionToMain();
            }

            else
            {
                ShowConfirm();
            }
        }
    }

    public void QuitGame()
    {
        ShowConfirm();
    }

    public void RealQuit()
    {
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
        DataManager.instance.nowPlayer.stage = "AISLE1";
        DataManager.instance.nowPos.positionX = 1.1f;
        DataManager.instance.nowPos.positionY = 0;
        DataManager.instance.nowPos.positionZ = 11.13f;
        DataManager.instance.nowPos.rotationY = 0;
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

    public void ShowConfirm()
    {
        confirmPanel.SetActive(true);
        MainPanel.SetActive(false);
    }

    public void Cancel()
    {
        confirmPanel.SetActive(false);
        MainPanel.SetActive(true);
    }

    public void GoOption()
    {
        OptionPanel.SetActive(true);
        MainPanel.SetActive(false);
    }

    public void OptionToMain()
    {
        OptionPanel.SetActive(false);
        MainPanel.SetActive(true);
    }

    public void click()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
    }
}