using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner S; 
    
    [Header("Spawn Properties")]
    public float spawnBeginningOffset = .75f;
    public float spawnTimeout = .5f;
    public int maxEnemiesInScene = 200;
    public bool limitEnemies = true;

    [Header("Enemy Spawn Properties")] 
    public Vector2 lastPointOffsetLimit = new Vector2(2f, 10f);
    public Vector2 lifeTimeLimit = new Vector2(2f, 10f);
    public Vector2 flyOffLimit = new Vector2(-4f, -6f);
    
    [SerializeField]
    private GameObject[] objectPrefabs;
    private GameObject _enemyAnchor;
    private BoundsCheck _bndCheck;
    private float xMin;
    private float xMax;

    void Start()
    {
        if (S == null) S = this;
        else Debug.LogError("Tried to create another instance of EnemySpawner");
        
        _bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", spawnBeginningOffset);
        _enemyAnchor = Instantiate(new GameObject());
        _enemyAnchor.name = "Enemies";
        xMin = -_bndCheck.camWidth;
        xMax = _bndCheck.camWidth;
    }

    private void SpawnEnemy()
    {
        if (limitEnemies && _enemyAnchor.transform.childCount >= maxEnemiesInScene)
        {
            Invoke("SpawnEnemy", spawnTimeout);
            return;
        }

        GameObject go = CreateObject();
        CreateEnemy(go);
        Invoke("SpawnEnemy", spawnTimeout);
    }  
    
    private GameObject CreateObject()
    {
        int index = Random.Range(0, objectPrefabs.Length);
        GameObject go = Instantiate<GameObject>(objectPrefabs[index]); 
        
        Vector3 position = new Vector3(Random.Range(xMin, xMax), _bndCheck.camHeight + 2f, 0.2f);
        go.transform.position = position;
        return go;
    }
    
    private void CreateEnemy(GameObject go)
    {
        go.transform.parent = _enemyAnchor.transform;
        Enemy enemy = go.GetComponent<Enemy>();
        enemy.lastPointOffset = Random.Range(lastPointOffsetLimit.x, lastPointOffsetLimit.y);
        enemy.lifeTime = Random.Range(lifeTimeLimit.x, lifeTimeLimit.y);
        enemy.height = Random.Range(flyOffLimit.x, flyOffLimit.y);
    }
}
