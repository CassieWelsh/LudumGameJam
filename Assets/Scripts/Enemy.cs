using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float lastPointOffset = 2f;
    public float lifeTime = 2f;
    public float height = -4f;
    private float _currentTime = 0f;
    private Vector2 _p0;
    private Vector2 _p1;
    private Vector2 _p2;
    private BoundsCheck _bndCheck;

    void Start()
    {
        _bndCheck = GetComponent<BoundsCheck>();
        _p0 = transform.position;
        _p1 = new Vector2(Random.Range(-_bndCheck.camWidth, _bndCheck.camWidth), height);
        // float x = Random.Range(0f, .5f) < .25f ? -_bndCheck.camWidth - lastPointOffset : _bndCheck.camWidth + lastPointOffset;
        float x = _p0.x > 0 ? -_bndCheck.camWidth - lastPointOffset : _bndCheck.camWidth + lastPointOffset;
        _p2 = new Vector2(x, _p1.y);

        print(this.transform.position);
    }

    void Update()
    {
        Accelerate();
    }

    public void Accelerate()
    {
        _currentTime += Time.deltaTime;
        this.transform.position = Bezier(_p0, _p1, _p2, _currentTime / lifeTime);
    }
    
    Vector2 Bezier(Vector2 p0, Vector2 p1, Vector2 p2, float t)
    {
        Vector2 p0p1 = (1 - t) * p0 + t * p1;
        Vector2 p1p2 = (1 - t) * p1 + t * p2;
        Vector2 result = (1 - t) * p0p1 + t * p1p2;
        return result;
    }

}
