using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner S;
    public Sprite[] bigSprites;
    public Sprite[] smallSprites;
    public float spawnRate = .5f;
    public float minFallSpeed = 2f;
    public float maxFallSpeed = 10f;
    private BoundsCheck bndCheck;
    [SerializeField]
    private GameObject obstaclePrefab;

    void Start()
    {
        bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnObstacle", .25f);
    }

    private void SpawnObstacle()
    {
        GameObject go = Instantiate<GameObject>(obstaclePrefab);

        int ndx = Random.Range(0, bigSprites.Length);
        go.GetComponent<SpriteRenderer>().sprite = bigSprites[ndx];

        float xMin = -bndCheck.camWidth;
        float xMax = bndCheck.camWidth;
        go.GetComponent<BaseObstacle>().fallingSpeed = Random.Range(minFallSpeed, maxFallSpeed);
        Transform spawnPosition = go.GetComponent<Transform>();
        Vector3 position = new Vector3(Random.Range(xMin, xMax), bndCheck.camHeight + 2f, 0);
        spawnPosition.position = position;

        Invoke("SpawnObstacle", spawnRate);
    }
}
