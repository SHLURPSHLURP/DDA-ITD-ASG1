/// 
/// Script to handle user authenthication and validation
/// Made by Gracie Arianne Peh (S10265899G) 9/12/25
/// 

using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using System.Collections.Generic;
using System;

public class AuthManager : MonoBehaviour
{
    DatabaseReference db;

// fields for UI linking
    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;
    public TMP_Text loginErrorText;

    public TMP_InputField signupEmail;
    public TMP_InputField signupPassword;
    public TMP_InputField signupConfirm;
    public TMP_Text signupErrorText;

    public UIManager uiManager;

    void Start()
    {
    
        db = FirebaseDatabase.DefaultInstance.RootReference; //initialise firebase
       
    }

    // function to log in using SignInWithEmailAndPasswordAsync and async 
    public void LogIn()
    {
        loginErrorText.text = ""; 

        string email = loginEmail.text;
        string password = loginPassword.text;

        var loginTask = FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password);
        loginTask.ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                loginErrorText.text = GetFirebaseError(task.Exception); //for detailed validation 
                return;
            }

            if (task.IsCompleted)
            {
                Debug.Log("Login Success!");
                uiManager.ShowHomePanel();
            }
        });
    }

    // function to create account using CreateUserWithEmailAndPasswordAsync and async 
    public void CreateAccount()
    {
        signupErrorText.text = "";

        string email = signupEmail.text;
        string password = signupPassword.text;

        if (password != signupConfirm.text) // to check if passwords match
        {
            signupErrorText.text = "Passwords do not match.";
            return;
        }

        var signupTask = FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, password);
        signupTask.ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                signupErrorText.text = GetFirebaseError(task.Exception); //validation
                return;
            }

            if (task.IsCompleted)
            {
                Debug.Log("Account created!"); 
                uiManager.ShowHomePanel();
            }
        });
    }

    // error handling, referred to school notes + chatgpt
    private string GetFirebaseError(System.AggregateException ex)
    {
        FirebaseException fbEx = ex.GetBaseException() as FirebaseException;
        AuthError errorCode = (AuthError)fbEx.ErrorCode;

        switch (errorCode)
        {
            case AuthError.InvalidEmail: return "Invalid email format.";
            case AuthError.WrongPassword: return "Wrong password.";
            case AuthError.UserNotFound: return "Account not found.";
            case AuthError.EmailAlreadyInUse: return "Email already registered.";
            case AuthError.WeakPassword: return "Password too weak.";
            default: return "Authentication failed.";
        }
    }
}
