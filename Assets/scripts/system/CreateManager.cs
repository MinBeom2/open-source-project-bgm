using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using PimDeWitte.UnityMainThreadDispatcher;
using UnityEngine.SceneManagement;
using TMPro;

public class CreateManager : MonoBehaviour
{
    private FirebaseAuth auth;
    public TMP_InputField email;
    public TMP_InputField password;
    public TMP_InputField confirmPassword;


    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void Create()
    {
        if (password.text != confirmPassword.text)
        {
            Debug.LogError("비밀번호가 일치하지 않습니다.");
            return;
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

    //todo 뒤로가기


}
