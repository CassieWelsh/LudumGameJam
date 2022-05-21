using UnityEngine;
using TMPro;

public class Main : MonoBehaviour
{
    public TMP_Text deathText, bestScoreText, hpText;
    private MouseCrosshair crosshair;
    [SerializeField] private GameObject stats;
    [SerializeField] private GameObject deathScreen;

    void Start()
    {
        Application.targetFrameRate = 60;
        crosshair = GameObject.Find("Crosshair").GetComponent<MouseCrosshair>();
        stats.SetActive(true);
        deathScreen.SetActive(false);
    }

    void Update()
    {
        if (Player.S.hp <= 0)
            DeathMethod();

        UpdateHpText();
    }

    private void UpdateHpText()
    {
        string hpString = $"HP\n{Player.S.hp}/{Player.S.maxHp}";
        hpText.text = hpString;
    }

    private void DeathMethod()
    {
        Cursor.visible = true;
        stats.SetActive(false);
        deathScreen.SetActive(true);

        int bestscore = PlayerPrefs.GetInt("BestScore");
        if (bestscore < Score.S.score) 
        {
            PlayerPrefs.SetInt("BestScore", Score.S.score);
            bestscore = Score.S.score;
        } 

        string deathString = $"YOUR\nSCORE\n{Score.S.score}";
        string bestScoreString = $"BEST:  {bestscore}";
        deathText.text = deathString; 
        bestScoreText.text = bestScoreString;

        Destroy(Player.S.gameObject);
        Destroy(crosshair.gameObject);
        Destroy(this);
    }
}
