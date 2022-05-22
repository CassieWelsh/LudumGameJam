using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 10;
    public int projectileDamage = 1;
    public float damageSplashTime = 2f;
    public float lastPointOffset = 2f;
    public float lifeTime = 2f;
    public float height = -4f;
    public float projectileVelocity = 7f;
    public float shootingCooldown = .7f;
    public GameObject projectilePrefab;
    public Transform canonTransform;
    public Transform shootingPoint;

    public float invincibleTill;
    private SpriteRenderer[] _spriteRenderers;
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

        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        
        Invoke("ShootPlayer", .1f);
    }

    void Update()
    {
        if (hp <= 0) 
            Destroy(this.gameObject);
        
        Accelerate();
        TwistCanon();
        CheckDamagedState();
    }

    private void Accelerate()
    {
        _currentTime += Time.deltaTime;
        transform.position = Bezier(_p0, _p1, _p2, _currentTime / lifeTime);
    }

    private void TwistCanon()
    {
        if (Main.S.currentGameState == GameState.GameOver) return; 
        
        Vector2 direction = Player.S.transform.position - canonTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        canonTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    private void ShootPlayer()
    {
        if (Main.S.currentGameState == GameState.GameOver) return; 
        
        GameObject go = Instantiate(projectilePrefab);
        Rigidbody2D goRigid = go.GetComponent<Rigidbody2D>();
        EnemyProjectile projectile = go.GetComponent<EnemyProjectile>();
        Vector2 direction = (Vector2) (Player.S.transform.position - shootingPoint.position);

        projectile.damage = projectileDamage;
        goRigid.position = shootingPoint.position;
        goRigid.velocity = direction.normalized * projectileVelocity;

        Invoke("ShootPlayer", shootingCooldown);
    } 
    
    Vector2 Bezier(Vector2 p0, Vector2 p1, Vector2 p2, float t)
    {
        Vector2 p0p1 = (1 - t) * p0 + t * p1;
        Vector2 p1p2 = (1 - t) * p1 + t * p2;
        Vector2 result = (1 - t) * p0p1 + t * p1p2;
        return result;
    }

    private void CheckDamagedState()
    {
        if (Time.time < invincibleTill)    
            foreach (var mat in _spriteRenderers)
                mat.material.color = Color.red;
        else
            foreach (var mat in _spriteRenderers)
                mat.material.color = Color.white;
    }
}
