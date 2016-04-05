using UnityEngine;
using System.Collections;

public class BirdGravity : MonoBehaviour {



    /*
        Class functionality
    */

    Vector3 velocity = Vector3.zero;

    public Vector3 gravity;
    public Vector3 flapVelocity;
    public float maxSpeed = 5f;

    private bool flap = false;

    private bool dead = false;

    public Sounds sounds;

    public GameScore score;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MakeAFlap();
        }
    }

	void FixedUpdate () {

        velocity += gravity * Time.deltaTime;

        if (flap && !dead)
        {
            flap = false;
            velocity += flapVelocity;
        }

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        if (transform.position.y > 3.0f)
            transform.position += velocity * Time.deltaTime;
        else
            Die();
	}

    public void Die()
    {
        if (!dead)
        {
            dead = true;
            score.setGameOver();
            sounds.playDie();
        }
    }

    public bool isDead()
    {
        return dead;
    }

    public void MakeAFlap()
    {
        if (!dead)
        {
            flap = true;
            sounds.playFlap();
        }
        
    }

    
}
