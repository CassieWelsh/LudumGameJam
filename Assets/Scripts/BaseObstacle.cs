using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle: MonoBehaviour
{
    public float fallingSpeed = 5f;    
    public float rotationDegrees = 50f;
    private int rotationSide;
    private BoundsCheck bndCheck;

    void Start()
    {
        rotationSide = Random.Range(0f, .5f) <= .25 ? 1 : -1;
        bndCheck = GetComponent<BoundsCheck>();
        // spriteRenderer.sprite = SpriteLists.S.bigSprites[Random.Range(0, SpriteLists.S.bigSprites.Count)];
    }

    void Update()
    {
        transform.position -= new Vector3(0, fallingSpeed * Time.deltaTime, 0);
        transform.Rotate(0, 0, rotationSide * rotationDegrees * Time.deltaTime);
        if (transform.position.y < -bndCheck.camHeight)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        print(collider.name);
    }
}
