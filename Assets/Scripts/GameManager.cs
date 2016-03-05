using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject star;

    public List<GameObject> life = new List<GameObject>();

    void Start()
    {
        for (int i=0; i<5; i++)
        {
            insertLife();
        }
    }

    public void insertLife()
    {
        Vector3 position = new Vector3((life.Count - 12), 0,-2);

        GameObject instance = Instantiate(star, position, Quaternion.identity) as GameObject;
        instance.transform.SetParent(gameObject.transform);
        instance.transform.Rotate(new Vector3(300, 0, 0));
        life.Add(instance);
    }

    public void loseLife()
    {        

        if (life.Count > 0)
        {
            GameObject lastStar = life[life.Count - 1];
            life.Remove(lastStar);
            Destroy(lastStar);
        }
        else
        {
            print("Game over");

            // Application.LoadLevel("GameOver"); depricated method
            //SceneManager.LoadScene(1);
        }
    }

}
