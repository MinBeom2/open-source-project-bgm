using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using PimDeWitte.UnityMainThreadDispatcher;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase;
public class CreateManager : MonoBehaviour
{
    private FirebaseAuth auth;
    public TMP_InputField email;
    public TMP_InputField password;
    public TMP_InputField confirmPassword;
    public TMP_Text errorMessageText;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Login_scene");
        }
    }

    public void Create()
    {
        if (password.text != confirmPassword.text)
        {
            DisplayErrorMessage("Passwords do not match.");
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                FirebaseException firebaseEx = task.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                string errorMessage = GetErrorMessage(errorCode);
                DisplayErrorMessage(errorMessage);
                return;
            }

            AuthResult authResult = task.Result;
            FirebaseUser user = authResult.User;
            Debug.LogError("회원가입 성공");
            UnityMainThreadDispatcher.Instance().Enqueue(() => SceneManager.LoadScene("Login_scene"));
        });
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
            case AuthError.WeakPassword:
                return "Password is too weak.";
            case AuthError.EmailAlreadyInUse:
                return "This email is already in use.";
            case AuthError.InvalidEmail:
                return "Invalid email address.";
            default:
                return "Sign-up failed. Please try again.";
        }
    }

    public void BackToLogin()
    {
        SceneManager.LoadScene("Login_SCENE");
    }
}