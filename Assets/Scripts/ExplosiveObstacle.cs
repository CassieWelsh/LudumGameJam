using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObstacle : BaseObstacle
{
    public float fieldOfImpact;
    public float force;
    public LayerMask layerToHit;



    public override void DestroyObject(GameObject obj, GameObject projectile)
    {
        Explode();
        if (projectile != null)
            Destroy(projectile);
        Destroy(this.gameObject);
    }

    public void DestroyObject(GameObject obj)    
    {
        DestroyObject(obj, null);
    }

    private void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact);

        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().velocity += direction * force;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;

        if (go.tag == "Player")
        {
            DestroyObject(this.gameObject);
        }
    }
}
