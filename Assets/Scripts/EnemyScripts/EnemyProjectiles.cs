using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectiles : MonoBehaviour
{

    private float disableCountdown; //How long until the object is 'disabled'

    private bool isActive; //Tells anything that may be trying to use this if it is active or not
    private bool disable; //Tells the script to disable itself next update

    [SerializeField]
    private Rigidbody2D rigidbody;

    [SerializeField]
    private ParticleSystem particleSystem;
    [SerializeField]
    private ParticleSystem deathParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        disable = false;
        disableCountdown = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        disableCountdown -= Time.deltaTime;
        if(disableCountdown <= 0.0f)
        {
            deathParticleSystem.Play();
            if (disable)
            {
                disable = false;
                DisableSelf();
            }
            disable = true;
        }
    }

    public void SetProjectileActive(Vector3 position, Vector2 velocity)
    {
        gameObject.SetActive(true);
        isActive = true;
        rigidbody.velocity = velocity;
        transform.position = position;
        disableCountdown = 3.0f;
    }

    public bool IsActive()
    {
        return isActive;
    }

    private void DisableSelf()
    {


        transform.position = new Vector3(2000.0f, 2000.0f, 0.0f);
        rigidbody.velocity = new Vector2 (0.0f, 0.0f);

        //gameObject.SetActive(false);
        isActive = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        particleSystem.transform.position = collision.GetContact(0).point;
        particleSystem.Play();
    }
}
