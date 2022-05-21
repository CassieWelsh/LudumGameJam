using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BoundsCheck bndCheck;

    void Start()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    void OnCollisionEnter2D(Collision2D collidedObj)
    {
        GameObject go = collidedObj.gameObject;
        switch (go.tag)
        {
            case "Player":
                print("Collided with player");
                break;

            case "Asteroid":
                var obst = go.GetComponent<BaseAsteroid>();
                obst.DestroyObstacle();
                if (obst.identifier != "Piece")
                    Destroy(this.gameObject);
                break;
            default:
                Destroy(this.gameObject);
                break;
        }
    }
}