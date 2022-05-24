using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Utils;
using Cursor = UnityEngine.Cursor;

public class Main : MonoBehaviour
{
    public static Main S;

    public TMP_Text deathText, bestScoreText, hpText;
    public GameState currentGameState;
    public GameObject background;
    [SerializeField] private GameObject stats;
    [SerializeField] private GameObject deathScreen;
    private MouseCrosshair crosshair;
    private AsteroidSpawner _asteroidSpawner;
    private EnemySpawner _enemySpawner;
    private GameState _previousGameState;
    private Scroll scroll;

    public List<SpawnProperties> spawnProperties;

    void Start()
    {
        if (S == null) S = this;
        else Debug.LogError("Tried to create another instance of Main");

        Application.targetFrameRate = 60;
        crosshair = GameObject.Find("Crosshair").GetComponent<MouseCrosshair>();
        stats.SetActive(true);
        deathScreen.SetActive(false);

        scroll = background.GetComponent<Scroll>();

        currentGameState = GameState.Normal;
        _asteroidSpawner = GetComponent<AsteroidSpawner>();
        _enemySpawner = GetComponent<EnemySpawner>();
        _previousGameState = currentGameState;
    }

    void Update()
    {
        if (Player.S.hp <= 0)
            currentGameState = GameState.GameOver;

        if (_previousGameState != currentGameState)
        {
            switch (currentGameState)
            {
                case GameState.Normal:
                    SwitchToNormal();
                    break;

                case GameState.BossFight:
                    SwitchToBoss();
                    break;

                case GameState.GameOver:
                    DeathMethod();
                    break;
            }

            _previousGameState = currentGameState;
        }

        UpdateHpText();
    }

    private void SwitchToNormal()
    {
        ChangeSpawnProperties(currentGameState);
    }

    private void SwitchToBoss()
    {
        ChangeSpawnProperties(currentGameState);
    }

    private void ChangeSpawnProperties(GameState state)
    {
        var properties = spawnProperties.Find(prop => prop.state == state); 
        
        _asteroidSpawner.spawnBeginningOffset = properties.aSpawnBeginningOffset;
        _asteroidSpawner.spawnTimeout = properties.aSpawnTimeout;
        _asteroidSpawner.maxObjectsInScene = properties.aMaxObjectsInScene;
        _asteroidSpawner.limitObjects = properties.aLimitObjects;
        _asteroidSpawner.objectPrefabs = properties.asteroidPrefabs;

        _enemySpawner.spawnBeginningOffset = properties.eSpawnBeginningOffset;
        _enemySpawner.spawnTimeout = properties.eSpawnTimeout;
        _enemySpawner.maxEnemiesInScene = properties.eMaxEnemiesInScene;
        _enemySpawner.limitEnemies = properties.eLimitEnemies;
        _enemySpawner.objectPrefabs = properties.enemyPrefabs;

        scroll.scrollSpeed = properties.backgroundScrollSpeed;
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