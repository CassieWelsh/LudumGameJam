using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public static AsteroidSpawner S; 
    
    [Header("Sprites")]
    public Sprite[] pieceSprites;
    public Sprite[] meteoriteSprites;
    public Sprite[] explosiveSprites;
    
    [Header("Spawn Properties")]
    public float spawnBeginningOffset = .75f;
    public float spawnTimeout = .5f;
    public int maxObjectsInScene = 200;
    public bool limitObjects = true;
    
    [Header("Asteroid Spawn Properties")]
    public float minFallSpeed = 2f;
    public float maxFallSpeed = 10f;
    
    private float xMin;
    private float xMax;
    private BoundsCheck _bndCheck;
    
    public GameObject[] objectPrefabs;

    private GameObject _asteroidAnchor;

    void Start()
    {
        if (S == null) S = this;
        else Debug.LogError("Tried to create another instance of AsteroidSpawner");
        
        _bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnAsteroid", spawnBeginningOffset);
        _asteroidAnchor = Instantiate(new GameObject());
        _asteroidAnchor.name = "Asteroids";
        xMin = -_bndCheck.camWidth;
        xMax = _bndCheck.camWidth;
    }

    private void SpawnAsteroid()
    {
        if (limitObjects && _asteroidAnchor.transform.childCount >= maxObjectsInScene)
        {
            Invoke("SpawnAsteroid", spawnTimeout);
            return;
        }

        GameObject go = CreateObject();
        CreateAsteroid(go);
        Invoke("SpawnAsteroid", spawnTimeout);
    }

    private GameObject CreateObject()
    {
        int index = Random.Range(0, objectPrefabs.Length);
        GameObject go = Instantiate<GameObject>(objectPrefabs[index]); 
        
        Vector3 position = new Vector3(Random.Range(xMin, xMax), _bndCheck.camHeight + 2f, 0.2f);
        go.transform.position = position;
        return go;
    }

    private void CreateAsteroid(GameObject go)
    {
        go.transform.parent = _asteroidAnchor.transform;
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

        obst.fallingVelocity = Random.Range(minFallSpeed, maxFallSpeed);
    }
}
