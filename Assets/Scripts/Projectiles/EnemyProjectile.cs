using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 1; 
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject go = col.gameObject;
        switch (go.tag)
        {
            case "Player":
                if (Time.time > Player.S.invincibleTill)
                {
                    Player.S.hp -= damage;
                    Player.S.invincibleTill = Time.time + Player.S.damageSplashTime;
                }
                Destroy(this.gameObject);
                break;
        }
    }
}