using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScript : MonoBehaviour
{
    public TMP_Text deathText, bestScoreText;
    private PlayerMovement player;    
    private MouseCrosshair crosshair;
    [SerializeField]
    private GameObject stats;
    [SerializeField]
    private GameObject deathScreen;

    void Start()
    {
        player = GetComponent<PlayerMovement>();
        crosshair = GameObject.Find("Crosshair").GetComponent<MouseCrosshair>();
        stats.SetActive(true);
        deathScreen.SetActive(false);
    }

    void Update()
    {
        if (player.hp <= 0)
            DeathMethod();
    }

    private void DeathMethod()
    {
        Cursor.visible = true;
        stats.SetActive(false);
        deathScreen.SetActive(true);

        int bestscore = PlayerPrefs.GetInt("BestScore");
        if (bestscore < player.score) 
        {
            PlayerPrefs.SetInt("BestScore", player.score);
            bestscore = player.score;
        } 

        string deathString = $"YOUR\nSCORE\n{player.score}";
        string bestScoreString = $"BEST:  {bestscore}";
        deathText.text = deathString; 
        bestScoreText.text = bestScoreString;

        Destroy(player.gameObject);
        Destroy(crosshair.gameObject);
    }
}
