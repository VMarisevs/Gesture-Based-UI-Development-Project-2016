using UnityEngine;
using System.Collections;

public class PipesMove : MonoBehaviour {

    public Vector3 velocity;
    public BirdGravity birdgravity;

    
    void FixedUpdate() {
        if (!birdgravity.dead)
            transform.position += velocity * Time.deltaTime;
    }
}
