using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject confirmPanel; // 확인창 Panel
    [SerializeField] private GameObject pauseMenu;  // 기본 Pause 메뉴 Panel
    [SerializeField] private GameObject SavePanel;
    [SerializeField] private GameObject OptionPanel;

    private bool isPaused = false;
    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SavePanel.activeSelf)
            {
                CloseSavePanel();
                ResumeGame();
            }
            else if (confirmPanel.activeSelf)
            {
                Cancel();
                ResumeGame();
            }

            else if (OptionPanel.activeSelf)
            {
                OptionPanel.SetActive(false);
                pauseMenu.SetActive(true);
            }

            else
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }
    public void ShowConfirm()
    {
        confirmPanel.SetActive(true);
        pauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void Cancel()
    {
        confirmPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        audioManager.SetVolume(audioManager.pauseVolume);
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        audioManager.SetVolume(audioManager.normalVolume);
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void CloseSavePanel()
    {
        SavePanel.SetActive(false);
        ResumeGame();
    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        audioManager.SetVolume(audioManager.normalVolume);
        SceneManager.LoadScene("MAIN");
    }

    public void GoOption()
    {
        OptionPanel.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void OptionToPause()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        OptionPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void click()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
    }
}