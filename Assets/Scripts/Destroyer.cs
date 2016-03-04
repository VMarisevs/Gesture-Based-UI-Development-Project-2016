using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
