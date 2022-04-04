using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle: MonoBehaviour
{
    public float fallingSpeed = 5f;    
    public float rotationDegrees = 50f;
    public float bonusDropChance = .5f;
    private int rotationSide;
    private BoundsCheck bndCheck;
    protected Rigidbody2D rigid;
    [SerializeField]
    private GameObject bonusPrefab;

    void Start()
    {
        rotationSide = Random.Range(0f, .5f) <= .25 ? 1 : -1;
        bndCheck = GetComponent<BoundsCheck>();
        rigid = GetComponent<Rigidbody2D>();
        // spriteRenderer.sprite = SpriteLists.S.bigSprites[Random.Range(0, SpriteLists.S.bigSprites.Count)];
    }

    void Update()
    {
        // transform.position -= new Vector3(0, fallingSpeed * Time.deltaTime, 0);
        rigid.velocity -= new Vector2(0, fallingSpeed) * Time.deltaTime;
        transform.Rotate(0, 0, rotationSide * rotationDegrees * Time.deltaTime);
        if (!bndCheck.isOnScreen)
            Destroy(gameObject);
    }

    public virtual void DestroyObject(GameObject obj, GameObject projectile)
    {
        Destroy(obj);
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
