using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoginSystem : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;
    public TMP_InputField confirmPassword;

    void Start()
    {
        FirebaseAuthManager.Instance.Init();
    }

    public void Create()
    {
        FirebaseAuthManager.Instance.Create(email.text, password.text, confirmPassword.text);
    }

    public void Login()
    {
        FirebaseAuthManager.Instance.Login(email.text, password.text);
    }

    public void LogOut()
    {
        FirebaseAuthManager.Instance.LogOut();
    }

    public void LoadCreate()
    {
        SceneManager.LoadScene("CREATE");
    }
}
