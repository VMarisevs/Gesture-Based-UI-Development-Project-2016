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

	void FixedUpdate () {
        velocity += gravity * Time.deltaTime;

        if (flap)
        {
            flap = false;
            velocity += flapVelocity;
        }

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        transform.position += velocity * Time.deltaTime;
	}

    public void MakeAFlap()
    {
        flap = true;   
    }
}
