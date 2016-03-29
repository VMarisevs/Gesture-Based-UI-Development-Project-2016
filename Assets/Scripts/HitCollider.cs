using UnityEngine;
using System.Collections;

public class HitCollider : MonoBehaviour {

    public GameObject gamemanager;
    public GameObject explosion;
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
            Destroy(other.gameObject);

            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

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
