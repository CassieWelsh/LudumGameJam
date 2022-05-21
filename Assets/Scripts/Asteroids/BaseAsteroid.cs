using UnityEngine;

public class BaseAsteroid : MonoBehaviour
{
    public float fallingVelocity = 5f;    
    public float rotationDegrees = 50f;
    public float bonusDropChance = .5f;
    public string identifier;
    private int _rotationSide;
    private BoundsCheck _bndCheck;
    protected Rigidbody2D rigid;
    [SerializeField]
    private GameObject bonusPrefab;

    void Start()
    {
        _rotationSide = Random.Range(0f, .5f) <= .25 ? 1 : -1;
        _bndCheck = GetComponent<BoundsCheck>();
        rigid = GetComponent<Rigidbody2D>();
        // spriteRenderer.sprite = SpriteLists.S.bigSprites[Random.Range(0, SpriteLists.S.bigSprites.Count)];
    }

    void Update()
    {
        ApplyFallingVelocity();
        // if (!_bndCheck.isOnScreen)
        //     Destroy(gameObject);
    }

    private void ApplyFallingVelocity()
    {
        // transform.position -= new Vector3(0, fallingSpeed * Time.deltaTime, 0);
        rigid.velocity -= new Vector2(0, fallingVelocity) * Time.deltaTime;
        transform.Rotate(0, 0, _rotationSide * rotationDegrees * Time.deltaTime);
    }

    public virtual void DestroyObstacle()
    {
        DropBonus();
        Destroy(this.gameObject);
    }

    public void DropBonus()
    {
        if (Random.Range(0f, 1f) <= bonusDropChance)
        {
            GameObject go = Instantiate(bonusPrefab);
            go.transform.position = transform.position; 
        }
    }
}
