using UnityEngine;
using System.Collections;

public class PipesMove : MonoBehaviour {

    public Vector3 velocity;

    
    void FixedUpdate() {
        transform.position += velocity * Time.deltaTime;
    }
}
