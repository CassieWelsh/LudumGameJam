using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BoundsCheck bndCheck;

    void Start()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    void Update()
    {
        if (!bndCheck.isOnScreen)
            Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        GameObject go = collider.gameObject;
        switch (go.tag)
        {
            case "Player":
                print("Collided with player");
                break;

            case "SmallAsteroid":
            case "BigAsteroid":
                var obst = go.GetComponent<BaseObstacle>();
                obst.DropBonus();
                obst.DestroyObject(go, this.gameObject);
                break;
            case "ExplosiveAsteroid":
                var obst1 = go.GetComponent<ExplosiveObstacle>();
                obst1.DestroyObject(go, this.gameObject);
                break;
        }
    }
}
