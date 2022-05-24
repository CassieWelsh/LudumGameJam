using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int maxHp = 20;
    public int hp = 20;
    public float easing = .2f;
    public int damage = 1;
    public float projectileSpeed = 5f;
    public float projectileCooldown = .7f;
    public float topShootingSpeed = 5f;
    public int projectileRound = 3;
    public float damageSplashTime = .7f;
    public float phaseTwoPercentage = .9f;
    public float phaseThreePercentage = .5f;
    public GameObject projectilePrefab;
    [HideInInspector] public float invincibleTill;
    private Animator _animator;
    private List<SpriteRenderer> _sprites;
    private GameObject _top;
    private Transform _shootingPoint;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _sprites = GetComponentsInChildren<SpriteRenderer>().ToList();
        _animator.SetInteger("MaxHp", hp);
        _top = transform.Find("Head").gameObject;
        _shootingPoint = transform.Find("ShootingPoint");

        hp = maxHp;
    }

    void Update()
    {
        MoveToCenter();
        RotateTowardsPlayer();
        CheckDamagedState();
    }

    private void CheckDamagedState()
    {
        if (Time.time < invincibleTill)    
            foreach (var mat in _sprites)
                mat.material.color = Color.red;
        else
            foreach (var mat in _sprites)
                mat.material.color = Color.white;
    }
    
    private void MoveToCenter()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(0, 0), easing);
    }

    private void RotateTowardsPlayer()
    {
        if (Main.S.currentGameState == GameState.GameOver) return;
        
        Vector3 targ = Player.S.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void ShootHead()
    {
        _top.transform.SetParent(null);
        _top.GetComponent<Rigidbody2D>().velocity = (Player.S.transform.position - _top.transform.position).normalized * topShootingSpeed;
        _sprites.Remove(_top.GetComponent<SpriteRenderer>());
    }

    public void Shoot()
    {
        if (Main.S.currentGameState == GameState.GameOver) return;
        
        GameObject go = Instantiate(projectilePrefab);
        Rigidbody2D goRigid = go.GetComponent<Rigidbody2D>();
        EnemyProjectile projectile = go.GetComponent<EnemyProjectile>();

        projectile.damage = damage;
        var direction = (Player.S.transform.position - _shootingPoint.position).normalized * projectileSpeed;
        goRigid.position = _shootingPoint.position;
        goRigid.velocity = direction;
    }

    void OnDestroy()
    {
        Main.S.currentGameState = GameState.Normal;
    }
}