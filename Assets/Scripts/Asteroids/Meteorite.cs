using UnityEngine;

public class Meteorite : BaseAsteroid
{
    public int pieceCountRange = 3;
    [SerializeField]
    private GameObject[] pieces; 

    public override void DestroyObstacle()
    {
        int pieceCount = Random.Range(0, pieceCountRange + 1);
        for (int i = 0; i < pieceCount; i++) 
        {
            Vector2 location = Random.insideUnitCircle;
            int indx = Random.Range(0, pieces.Length);
            GameObject piece = Instantiate(pieces[indx]);
            piece.transform.position = (Vector2) this.transform.position + location;
            piece.GetComponent<Rigidbody2D>().velocity = -rigid.velocity;
        }
        
        Destroy(this.gameObject);
    }
}
