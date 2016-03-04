using UnityEngine;
using System.Collections;

public class HitCollider : MonoBehaviour {

    public GameObject gamemanager;

    void OnTriggerEnter(Collider other)
    {

        
        if (other.gameObject.tag.Equals("Enemy"))
        {
            print("Hit");
            GameManager gm = gamemanager.GetComponent<GameManager>();
            gm.loseLife();
        }
    }
}
