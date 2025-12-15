/// DDA FIREBASE PORTION
/// Script to handle creation and logging in of account using firebase auth 
/// Made by Gracie Arianne Peh (S10265899G) 12/12/25
/// 


using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;

public class AuthManager : MonoBehaviour
{
//FIELDS FOR SIGN IN
    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;
    public TMP_Text loginErrorText;

// FIELDS FOR SIGN UP
    public TMP_InputField signupEmail;
    public TMP_InputField signupPassword;
    public TMP_Text signupErrorText;

//TO CONNECT TO HOMEPAGE
    public UIManager uiManager;

    // LOG IN (CALL METHOD FROM FIREBASEPLAYERMANAGER TO LOAD DATABASE NODE USING AUTH GENERATED UID) THEN GO TO HOME PANEL 
    public void LogIn()
    {
        loginErrorText.text = "";

        FirebaseAuth.DefaultInstance
            .SignInWithEmailAndPasswordAsync(
                loginEmail.text,
                loginPassword.text
            )
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    loginErrorText.text = "Login failed.";
                    return;
                }

                FirebasePlayerManager.Instance.LoadExistingPlayer();
                uiManager.ShowHomePanel();
            });
    }

  
    // SIGN UP (CALL METHOD FROM FIREBASEPLAYERMANAGER TO CREATE NEW DATABASE NODE USING AUTH GENERATED UID) THEN GO TO HOME PANEL 
    public void CreateAccount()
    {
        signupErrorText.text = "";

        FirebaseAuth.DefaultInstance
            .CreateUserWithEmailAndPasswordAsync(
                signupEmail.text,
                signupPassword.text
            )
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    signupErrorText.text = "Signup failed.";
                    return;
                }

                FirebasePlayerManager.Instance.InitializeNewPlayer();
                uiManager.ShowHomePanel();
            });
    }
}
