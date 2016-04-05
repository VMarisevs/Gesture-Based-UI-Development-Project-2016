using UnityEngine;
using System.Collections;

public class BirdGravity : MonoBehaviour {

    Vector3 velocity = Vector3.zero;

    public Vector3 gravity;

	void FixedUpdate () {
        velocity += gravity;

        transform.position += velocity * Time.deltaTime;
	}
}
