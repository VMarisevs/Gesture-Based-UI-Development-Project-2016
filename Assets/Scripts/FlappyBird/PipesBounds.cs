using UnityEngine;
using System.Collections;

public class PipesBounds : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
