using Firebase.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    private DatabaseReference dbReference;

    void Start()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveHighScoreFunc(int score)
    {
        string userId = FirebaseAuthManager.UserID;

        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogError("🔥 Error: User ID is empty! Unable to query firebase.");
            return;
        }

        dbReference.Child("users").Child(userId).Child("highscore").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Firebase GetValueAsync Error: " + task.Exception);
                return;
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int currentHighScore = snapshot.Exists ? int.Parse(snapshot.Value.ToString()) : 0;
                if (score > currentHighScore)
                {
                    dbReference.Child("users").Child(userId).Child("highscore").SetValueAsync(score)
                        .ContinueWith(setTask =>
                        {
                            if (setTask.IsCompleted)
                            {
                                Debug.Log("New highscore is saved : " + score);
                            }
                            else if (setTask.IsFaulted)
                            {
                                Debug.LogError("Firebase SetValueAsync Error: " + setTask.Exception);
                            }
                        });
                }
                else
                {
                    Debug.Log("New score must be higher than highscore. Not save!");
                }
            }
        });
    }

    public async Task<List<HighscoreModel>> LoadHighScores()
    {
        List<HighscoreModel> highScores = new List<HighscoreModel>();
        DataSnapshot snapshot = await dbReference.Child("users").OrderByChild("highscore").LimitToLast(10).GetValueAsync();

        if (snapshot.Exists)
        {
            foreach (DataSnapshot userSnapshot in snapshot.Children)
            {
                string username = userSnapshot.Child("username").Value?.ToString() ?? "Unknown";
                int score = int.TryParse(userSnapshot.Child("highscore").Value?.ToString(), out int parsedScore) ? parsedScore : 0;
                HighscoreModel listedHs = new HighscoreModel
                {
                    Username = username,
                    Score = score
                };
                highScores.Add(listedHs);
            }

            highScores.Sort((a, b) => b.Score.CompareTo(a.Score));
        }
        else
        {
            Debug.Log("Highscore list is empty!");
        }

        return highScores;
    }


}
