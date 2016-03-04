using UnityEngine;
using System.Collections;

public class EnemySpawnArea : MonoBehaviour {

    public bool busy = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            busy = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            busy = false;
        }
    }
}
