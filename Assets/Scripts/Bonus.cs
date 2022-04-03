using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public int healUp = 2;    
    public float velocity = 2f;
    private Rigidbody2D rigid;
    private BoundsCheck bndCheck;
    private float rotationSide;
    [SerializeField]
    private float rotationDegrees = 20;

    void Start()
    {
        rotationSide = Random.Range(0f, .5f) <= .25 ? 1 : -1;
        rigid = GetComponent<Rigidbody2D>();
        bndCheck = GetComponent<BoundsCheck>();
    }    

    void Update()
    {
        Vector2 direction = (Vector2) (transform.position + Random.onUnitSphere).normalized;
        rigid.velocity = direction.normalized * velocity * Time.deltaTime;
        transform.Rotate(0, 0, rotationSide * rotationDegrees * Time.deltaTime);

        if(!bndCheck.isOnScreen)
            Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        GameObject go = collider.gameObject;

        switch (go.tag)
        {
            case "Player":
                var player = go.GetComponent<PlayerMovement>();
                player.hp += healUp;
                Destroy(this.gameObject);
                break;
        }
    }
}