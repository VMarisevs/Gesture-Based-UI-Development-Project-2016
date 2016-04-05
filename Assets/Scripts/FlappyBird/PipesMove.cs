using UnityEngine;
using System.Collections;

public class PipesMove : MonoBehaviour {

    public Vector3 velocity;
    public BirdGravity birdgravity;
    public bool dead;

    void FixedUpdate() {
       // if (!birdgravity.isDead())
            transform.position += velocity * Time.deltaTime;
    }
}
