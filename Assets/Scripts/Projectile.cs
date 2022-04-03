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
}
