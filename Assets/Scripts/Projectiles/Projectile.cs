using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1; 
    
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
            
            case "Enemy":
                var enemy = go.GetComponent<Enemy>();
                if (Time.time > enemy.invincibleTill)
                {
                    enemy.hp -= damage;
                    enemy.invincibleTill = Time.time + Player.S.damageSplashTime;
                }
                Destroy(this.gameObject);
                break;
            
            default:
                Destroy(this.gameObject);
                break;
        }
    }
}