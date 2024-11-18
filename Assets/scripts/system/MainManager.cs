using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    // Quit Game 버튼이 눌리면 호출될 함수
    public void QuitGame()
    {
        // 게임을 종료
        Debug.Log("게임 종료");
        Application.Quit();

        // 에디터에서 테스트 중일 경우 에디터도 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Load Game 버튼이 눌리면 호출될 함수
    public void LoadGame()
    {
        // "aisle1" 씬을 로드
        SceneManager.LoadScene("LOAD_SCENE");
    }
}
