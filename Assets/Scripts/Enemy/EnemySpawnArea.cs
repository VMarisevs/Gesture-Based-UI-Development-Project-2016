using UnityEngine;
using System.Collections;

public class EnemySpawnArea : MonoBehaviour {

    public bool busy = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            busy = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            busy = false;
        }
    }
}
