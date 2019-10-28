using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainScript : MonoBehaviour
{

    private int lives;

    private Vector3 startLocation;
    private Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        startLocation = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetLivesRemaining()
    {
        return lives;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            lives -= 1;
        }
    }

    public void ResetObject()
    {
        transform.position = startLocation;
        transform.rotation = startRotation;
        lives = 3;
    }
}
