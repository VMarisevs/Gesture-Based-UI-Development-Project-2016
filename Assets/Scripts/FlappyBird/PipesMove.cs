using UnityEngine;
using System.Collections;

public class PipesMove : MonoBehaviour {

    public Vector3 velocity;
    public BirdGravity birdgravity;
    public bool gameover = false;

    void FixedUpdate() {
       if (!gameover)
            transform.position += velocity * Time.deltaTime;
    }
}
