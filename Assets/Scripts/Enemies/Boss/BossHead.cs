using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossHead : MonoBehaviour
{
    public float pushForce;
    private Boss _boss;

    void Start() => _boss = transform.parent.gameObject.GetComponent<Boss>();
    
    void OnCollisionStay2D(Collision2D col)
    {
        GameObject go = col.gameObject;
        switch (go.tag)
        {
            case "Player":
                if (Time.time > Player.S.invincibleTill)
                {
                    Player.S.hp -= _boss.damage;
                    Player.S.invincibleTill = Time.time + Player.S.damageSplashTime;
                    Player.S.rigid.velocity += (Vector2) ((Player.S.transform.position - transform.position).normalized * pushForce);
                }
                break;
        }
    }
}
