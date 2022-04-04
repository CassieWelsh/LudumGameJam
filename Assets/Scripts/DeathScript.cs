using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    private PlayerMovement player;    
    private MouseCrosshair crosshair;

    void Start()
    {
        player = GetComponent<PlayerMovement>();
        crosshair = GameObject.Find("Crosshair").GetComponent<MouseCrosshair>();
    }

    void Update()
    {
        if (player.hp <= 0)
            DeathMethod();
    }

    private void DeathMethod()
    {
        Cursor.visible = true;
        Destroy(player.gameObject);
        Destroy(crosshair.gameObject);
    }
}
