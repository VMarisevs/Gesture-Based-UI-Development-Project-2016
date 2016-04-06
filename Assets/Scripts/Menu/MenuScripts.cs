using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScripts : MonoBehaviour {

    public ZigMenu zigmenu;
    public Text score;

	// Use this for initialization
	void Start () {
        zigmenu._openMainTab();

        score.text = "High Score: " + PlayerPrefs.GetFloat("HiScore");

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
