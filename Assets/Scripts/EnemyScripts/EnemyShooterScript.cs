using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterScript : MonoBehaviour
{
    private float attackCountDown;
    public float maxVelocity;
    private float releaseCountdown; //Counts down until another pair of projectiles are launched
    public float attackCountdownUpperRange;
    public float attackCountdownLowerRange;

    private bool attackStarted;
    private bool alligned;

    private GameObject player;

    private Rigidbody2D rb;

    public ParticleSystem particleSystem;

    private EnemyManager enemyManager;

    private EnemyProjectiles projA, projB;

    private Vector2 target;
    private Vector3 startLocation;
    private Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        attackStarted = false;
        alligned = false;

        attackCountDown = 0.0f;

        releaseCountdown = 1.0f;

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        enemyManager = Object.FindObjectOfType<EnemyManager>();

        startLocation = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        attackCountDown -= Time.deltaTime;
        releaseCountdown -= Time.deltaTime;

        if (attackCountDown <= 0.1f && !attackStarted)
        {
            ChargeAttack();
        }

        if (alligned == true)
        {
            FireAttack();
        }

        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
        }

        if(releaseCountdown <= 0 && rb.velocity.magnitude > 5.0f)
        {
            releaseCountdown = 1.0f;

            FireProjectile();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        particleSystem.transform.position = collision.GetContact(0).point;
        particleSystem.Play();
    }

    void ChargeAttack()
    {
        if (rb.velocity.magnitude <= 0.1f)
        {
            target = player.transform.position - transform.position;

            transform.up = Vector2.Lerp(transform.up, target, 0.002f);

            if (Vector2.Dot(target.normalized, transform.up.normalized) > 0.999f)
            {
                alligned = true;

                attackStarted = true;
            }
        }
        else
        {
            rb.velocity = rb.velocity * 0.9f;
            if (rb.velocity.magnitude <= 0.1f)
            {
                rb.velocity.Set(0.0f, 0.0f);
            }
        }
    }

    void FireAttack()
    {
        rb.velocity = transform.up * maxVelocity;

        attackCountDown = Random.Range(attackCountdownLowerRange, attackCountdownUpperRange);

        attackStarted = false;
        alligned = false;
    }

    public void ResetObject()
    {
        transform.position = startLocation;
        transform.rotation = startRotation;
        rb.velocity = new Vector2(0.0f, 0.0f);
        particleSystem.Clear();
        attackCountDown = 0.0f;
        releaseCountdown = 0.5f;
    }

    void FireProjectile()
    {
        projA = enemyManager.GetProjectileToShoot();

        projA.SetProjectileActive(transform.position + transform.right, new Vector2(0.0f, 5.0f) * transform.right);

        projB = enemyManager.GetProjectileToShoot();

        projB.SetProjectileActive(transform.position - transform.right, new Vector2(0.0f, 5.0f) * -transform.right);
    }
}
