using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score S; 
    
    public int score;
    public int increaseValue = 100;
    [SerializeField] private float increaseIntensity = .5f;
    [SerializeField] private TMP_Text text;
    void Start()
    {
        if (S == null) S = this;
        else Debug.LogError("Tried to create another instance of Score");
        
        score = 0;
        if (!PlayerPrefs.HasKey("BestScore"))
            PlayerPrefs.SetInt("BestScore", 0);            
        UpdateScoreText();
        
        Invoke("ScoreIncrease", increaseIntensity);
    }

    private void ScoreIncrease()
    {
        score += increaseValue;
        UpdateScoreText();
        Invoke("ScoreIncrease", increaseIntensity);
    }

    private void UpdateScoreText()
    {
        string scoreString = $"Score\n{score}";
        text.text = scoreString;
    }
}
