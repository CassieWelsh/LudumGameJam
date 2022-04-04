using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigObstacle : BaseObstacle
{
    public int pieceCountRange = 3;
    [SerializeField]
    private GameObject[] pieces; 

    public override void DestroyObject(GameObject go, GameObject projectile)
    {
        int pieceCount = Random.Range(0, pieceCountRange + 1);
        for (int i = 0; i < pieceCount; i++) 
        {
            Vector2 location = Random.insideUnitCircle;
            int indx = Random.Range(0, pieces.Length);
            GameObject piece = Instantiate(pieces[indx]);
            piece.transform.position = (Vector2) go.transform.position + location;
            piece.GetComponent<Rigidbody2D>().velocity = -rigid.velocity;
        }
        
        Destroy(projectile);
        Destroy(this.gameObject);
    }
}
