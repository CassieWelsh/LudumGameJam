using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject go = col.gameObject;
        switch (go.tag)
        {
            case "Player":
                print("Attacked Player");
                break;
        }
    }
}