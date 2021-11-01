using UnityEngine;

public class GameSession : MonoBehaviour
{
    private int currentScore;
    
    private void Awake() => SetUpSingleton();
    
    public int GetScore() => currentScore;

    public void AddScore(int score) => currentScore += score;

    public void RestartScore() => currentScore = 0;
    
    private void SetUpSingleton()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
