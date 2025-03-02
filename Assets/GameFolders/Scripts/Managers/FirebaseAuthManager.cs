using Firebase.Auth;
using UnityEngine;

public class FirebaseAuthManager : MonoBehaviour
{
    private FirebaseAuth auth;
    public static string UserID { get; private set; }

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        if (auth.CurrentUser != null)
        {
            UserID = auth.CurrentUser.UserId;
            Debug.Log("Eski kullanýcý giriþ yaptý: " + UserID);
        }
        else
        {
            SignInAnonymously();
        }
    }

    void SignInAnonymously()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCompleted && task.Result != null)
            {
                UserID = task.Result.User.UserId;
            }
        });
    }
}
