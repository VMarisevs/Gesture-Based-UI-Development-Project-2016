using UnityEngine;
using System.Collections;

public class HitCollider : MonoBehaviour {

    public GameObject gamemanager;
    bool _triggered;

    void OnTriggerEnter(Collider other)
    {
        if (_triggered)
        {
            return;
        }
        _triggered = true;

        if (other.gameObject.tag.Equals("Enemy"))
        {
            //print("Hit");
            // Destroy(other.gameObject);
            GameManager gm = gamemanager.GetComponent<GameManager>();
            gm.loseLife();
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (!_triggered)
        {
            return;
        }
        _triggered = false;
        //print("exit");
    }
}
