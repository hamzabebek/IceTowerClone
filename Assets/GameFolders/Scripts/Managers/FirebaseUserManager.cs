using Firebase.Database;
using TMPro;
using UnityEngine;

public class FirebaseUserManager : MonoBehaviour
{
    private DatabaseReference dbReference;
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private TMP_InputField usernameInputField;
    void Start()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        usernameInputField.onEndEdit.AddListener(delegate { SaveUsername(usernameInputField.text); });
        if (PlayerPrefs.HasKey("Username"))
        {
            Debug.Log("Mevcut Kullanýcý Adý: " + PlayerPrefs.GetString("Username"));
            usernameText.text = PlayerPrefs.GetString("Username");
        }
        else
        {
            string newUsername = "User_" + Random.Range(1000, 9999);
            SaveUsername(newUsername);
        }
    }

    public void SaveUsername(string username)
    {
        string userId = FirebaseAuthManager.UserID;
        dbReference.Child("users").Child(userId).Child("username").SetValueAsync(username);
        PlayerPrefs.SetString("Username", username);
        usernameText.text = username;
        Debug.Log("Yeni Username Kaydedildi: " + username);
    }
}

