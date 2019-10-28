using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Vector2 target;
    private float attackCountDown;
    public float maxVelocity;
    private float chargeNum;
    public float attackCountdownUpperRange;
    public float attackCountdownLowerRange;

    private bool attackStarted;
    private bool alligned;

    private GameObject player;

    private Rigidbody2D rb;

    [SerializeField]
    private ParticleSystem particleSystem;

    private Vector3 startLocation;
    private Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        attackStarted = false;
        alligned = false;

        attackCountDown = 0.0f;

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        startLocation = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        attackCountDown -= Time.deltaTime;

        if(attackCountDown <= 0.1f && !attackStarted)
        {
            ChargeAttack();
        }

        if(alligned == true)
        {
            FireAttack();
        }

        if(rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        particleSystem.transform.position = collision.GetContact(0).point;
        particleSystem.Play();
    }

    public void ChargeAttack()
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

    public void FireAttack()
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
    }
}
