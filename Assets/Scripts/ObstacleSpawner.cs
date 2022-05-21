using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Sprite[] pieceSprites;
    public Sprite[] meteoriteSprites;
    public Sprite[] explosiveSprites;
    public float spawnBeginningOffset = .75f;
    public float spawnTimeout = .5f;
    public int maxObstaclesInFrame = 200;
    public bool limitObstacles = true;
    public float minFallSpeed = 2f;
    public float maxFallSpeed = 10f;
    private BoundsCheck _bndCheck;
    [SerializeField]
    private GameObject[] obstaclePrefabs;

    private GameObject _anchor;

    void Start()
    {
        _bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnObstacle", spawnBeginningOffset);
        _anchor = Instantiate(new GameObject());
        _anchor.name = "Asteroids";
    }

    private void SpawnObstacle()
    {
        if (limitObstacles && _anchor.transform.childCount >= maxObstaclesInFrame)
        {
            Invoke("SpawnObstacle", spawnTimeout);
            return;
        }

        int index = Random.Range(0, obstaclePrefabs.Length);
        GameObject go = Instantiate<GameObject>(obstaclePrefabs[index]); 
        go.transform.parent = _anchor.transform;
        BaseAsteroid obst = go.GetComponent<BaseAsteroid>(); 

        switch (obst.identifier)
        {
            case "Piece":
                int ndxSmall = Random.Range(0, pieceSprites.Length);
                go.GetComponent<SpriteRenderer>().sprite = pieceSprites[ndxSmall];
                break;
            
            case "Meteorite":
                int ndxBig = Random.Range(0, meteoriteSprites.Length);
                go.GetComponent<SpriteRenderer>().sprite = meteoriteSprites[ndxBig];
                break;

            case "Explosive":
                int ndxExplosive = Random.Range(0, explosiveSprites.Length);
                go.GetComponent<SpriteRenderer>().sprite = explosiveSprites[ndxExplosive];
                break;
        }

        float xMin = -_bndCheck.camWidth;
        float xMax = _bndCheck.camWidth;
        go.GetComponent<BaseAsteroid>().fallingVelocity = Random.Range(minFallSpeed, maxFallSpeed);
        Transform spawnPosition = go.GetComponent<Transform>();
        Vector3 position = new Vector3(Random.Range(xMin, xMax), _bndCheck.camHeight + 2f, 0.2f);
        spawnPosition.position = position;

        Invoke("SpawnObstacle", spawnTimeout);
    }
}
