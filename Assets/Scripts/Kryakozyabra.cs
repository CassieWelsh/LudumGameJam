using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kryakozyabra : MonoBehaviour
{
    public int damage = 2;
    public float throwForce = 5f;
    public float amplitude = 1f;
    public float speed = 1f;

    void Update()
    {
        Vector2 currentFrameHeight = transform.position;
        currentFrameHeight.y += amplitude * speed * Mathf.Cos(Mathf.PI * Time.time);
        transform.position = currentFrameHeight;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;

        switch (go.tag)
        {
            case "Player":
                DamagePlayer(collision);
                break;
        }
    }

    private void DamagePlayer(Collision2D player)
    {
        var lastContact = player.GetContact(0);
        Vector2 throwDirection = ((Vector2) player.transform.position - lastContact.point).normalized;
        Rigidbody2D playerRigid = player.gameObject.GetComponent<Rigidbody2D>();
        playerRigid.velocity += throwDirection * throwForce;

        var playerGO = player.gameObject.GetComponent<Player>();
        if (Time.time > playerGO.invincibleTill)
        {
            playerGO.hp -= damage;
            playerGO.invincibleTill = Time.time + playerGO.damageSplashTime;
        }
    }
}
