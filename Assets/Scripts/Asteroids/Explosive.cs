using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : BaseAsteroid
{
    public float fieldOfImpact;
    public float force;
    public LayerMask layerToHit;

    public override void DestroyObstacle()
    {
        Explode();
        Destroy(this.gameObject);
    }

    private void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact);

        foreach (Collider2D obj in objects)
        {
            if (obj.tag == "Bonus") continue;
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().velocity = direction * force;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;

        if (go.tag == "Player")
        {
            // DestroyObstacle();
        }
    }
}
