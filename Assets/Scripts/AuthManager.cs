using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;

public class AuthManager : MonoBehaviour
{
    [Header("Login")]
    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;
    public TMP_Text loginErrorText;

    [Header("Signup")]
    public TMP_InputField signupEmail;
    public TMP_InputField signupPassword;
    public TMP_Text signupErrorText;

    public UIManager uiManager;

    // =========================
    // LOGIN
    // =========================
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

                Debug.Log("Login success.");

                FirebasePlayerManager.Instance.LoadExistingPlayer();
                uiManager.ShowHomePanel();
            });
    }

    // =========================
    // SIGN UP
    // =========================
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

                Debug.Log("Account created.");

                FirebasePlayerManager.Instance.InitializeNewPlayer();
                uiManager.ShowHomePanel();
            });
    }
}
