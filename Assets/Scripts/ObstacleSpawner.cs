using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Sprite[] bigSprites;
    public Sprite[] smallSprites;
    public float spawnTimeout = .5f;
    public int maxObstaclesInFrame = 200;
    public bool limitObstacles = true;
    public float minFallSpeed = 2f;
    public float maxFallSpeed = 10f;
    private BoundsCheck bndCheck;
    [SerializeField]
    private GameObject[] obstaclePrefabs;

    private GameObject anchor;

    void Start()
    {
        bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnObstacle", .25f);
        anchor = Instantiate(new GameObject());
        anchor.name = "Anchor";
    }

    private void SpawnObstacle()
    {
        if (limitObstacles && anchor.transform.childCount >= maxObstaclesInFrame)
        {
            Invoke("SpawnObstacle", spawnTimeout);
            return;
        }

        int index = Random.Range(0, obstaclePrefabs.Length);
        GameObject go = Instantiate<GameObject>(obstaclePrefabs[index]); 
        go.transform.parent = anchor.transform;

        switch (go.tag)
        {
            case "BigAsteroid":
                int ndxBig = Random.Range(0, bigSprites.Length);
                go.GetComponent<SpriteRenderer>().sprite = bigSprites[ndxBig];
                break;
            case "SmallAsteroid":
                int ndxSmall = Random.Range(0, smallSprites.Length);
                go.GetComponent<SpriteRenderer>().sprite = smallSprites[ndxSmall];
                break;
        }

        float xMin = -bndCheck.camWidth;
        float xMax = bndCheck.camWidth;
        go.GetComponent<BaseObstacle>().fallingSpeed = Random.Range(minFallSpeed, maxFallSpeed);
        Transform spawnPosition = go.GetComponent<Transform>();
        Vector3 position = new Vector3(Random.Range(xMin, xMax), bndCheck.camHeight + 2f, 0.2f);
        spawnPosition.position = position;

        Invoke("SpawnObstacle", spawnTimeout);
    }
}
