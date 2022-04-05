using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public int healUp = 2;    
    public float velocity = 2f;
    public float timeout = 2f;
    public float fallingSpeed = 5f;
    private Rigidbody2D rigid;
    private BoundsCheck bndCheck;
    private float rotationSide;
    [SerializeField]
    private float rotationDegrees = 20;
    private float curTime;

    void Start()
    {
        rotationSide = Random.Range(0f, .5f) <= .25 ? 1 : -1;
        rigid = GetComponent<Rigidbody2D>();
        bndCheck = GetComponent<BoundsCheck>();
        curTime = Time.time + timeout;
    }    

    void Update()
    {
        // Vector2 direction = (Vector2) (transform.position + Random.onUnitSphere).normalized;
        // rigid.velocity = direction.normalized * velocity * Time.deltaTime;
        transform.Rotate(0, 0, rotationSide * rotationDegrees * Time.deltaTime);

        rigid.velocity -= new Vector2(0, fallingSpeed) * Time.deltaTime;

        if (Time.time > curTime)
            Destroy(this.gameObject);

        if (!bndCheck.isOnScreen)
            Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        GameObject go = collider.gameObject;

        switch (go.tag)
        {
            case "Player":
                var player = go.GetComponent<PlayerMovement>();
                // if (player.hp < player.maxHp)
                // {
                //     player.hp += healUp;
                // }
                player.hp = Mathf.Min(player.hp + healUp, player.maxHp);
                Destroy(this.gameObject);
                break;
        }
    }
}
