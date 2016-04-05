using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameNotifications : MonoBehaviour {


    public GameObject notification;

    public Text[] flap;
    private bool green = true;
    private Color colorgreen;
    private Color colororange;

    private float flaptimer = 1.5f;

    void Start()
    {

        colorgreen = new Color();
        colororange = new Color();
        ColorUtility.TryParseHtmlString("#00FF8AFF", out colorgreen);
        ColorUtility.TryParseHtmlString("#FDD70BFF", out colororange);

        InvokeRepeating("changecolor", 0.2f, 0.2f);

        InvokeRepeating("timeit", 0.2f, 0.2f);
    }

    private void changecolor()
    {       
        // update color
        if (green)
        {
            for (int i = 0; i < flap.Length; i++)
            {
                flap[i].color = colororange;//"00FF8AFF"
            }
            green = false;
        }
        else
        {
            for (int i = 0; i < flap.Length; i++)
            {
                flap[i].color = colorgreen;
            }
            green = true;
        }
        
    }

    private void timeit()
    {
        flaptimer -= 0.2f;
        if (flaptimer < 0)
        {
            notification.SetActive(false);
            CancelInvoke("timeit");
            CancelInvoke("changecolor");
        }
    }

}
