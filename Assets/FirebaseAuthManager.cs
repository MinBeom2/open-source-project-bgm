using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using PimDeWitte.UnityMainThreadDispatcher;

public class FirebaseAuthManager : MonoBehaviour
{
    private FirebaseAuth auth;
    private FirebaseUser user;
    public TMP_InputField email;
    public TMP_InputField password;
    public TMP_InputField confirmPassword;

    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void Create()
    {

        if (password.text != confirmPassword.text)
        {
            Debug.LogError("비밀번호가 일치하지 않습니다.");
            return; // 일치하지 않으면 회원가입 진행하지 않음
        }


        auth.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("회원가입 취소");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("회원가입 실패");
                return;
            }

            AuthResult authResult = task.Result;
            FirebaseUser user = authResult.User;
            Debug.LogError("회원가입 성공");
            UnityMainThreadDispatcher.Instance().Enqueue(() => SceneManager.LoadScene("Login_scene"));
        });
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

            AuthResult authResult = task.Result;
            FirebaseUser user = authResult.User;
            Debug.LogError("로그인 성공");
            UnityMainThreadDispatcher.Instance().Enqueue(() => SceneManager.LoadScene("MAIN"));
        });
    }
    public void LoadCreate()
    {
        SceneManager.LoadScene("CREATE");
    }
}
