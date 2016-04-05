using UnityEngine;
using System.Collections;

public class BirdGravity : MonoBehaviour {



    /*
        Class functionality
    */

    Vector3 velocity = Vector3.zero;

    public Vector3 gravity;
    public Vector3 flapVelocity;

    private bool flap = false;

	void FixedUpdate () {
        velocity += gravity * Time.deltaTime;

        if (flap)
        {
            flap = false;
            velocity += flapVelocity;
        }

        transform.position += velocity * Time.deltaTime;
	}

    public void MakeAFlap()
    {
        flap = true;   
    }
}
