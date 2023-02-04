using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    //0-red, 1-green, 2-blue, 3-yellow, 4-dark red, 5-white, 6-bro you dont need to go there, 7-orange
    [SerializeField] private int intColorOfTail;

    public void ChangeColor()
    {
        switch (intColorOfTail)
        {
            case 0:
                gameObject.GetComponent<Image>().color = Color.red;
                break;
            case 1:
                gameObject.GetComponent<Image>().color = Color.green;
                break;
            case 2:
                gameObject.GetComponent<Image>().color = Color.blue;
                break;
            case 3:
                gameObject.GetComponent<Image>().color = Color.yellow;
                break;
            case 4:
                gameObject.GetComponent<Image>().color = new Color32(180,0,0,255);
                break;
            case 5:
                gameObject.GetComponent<Image>().color = Color.white;
                break;
            case 6:
                gameObject.GetComponent<Image>().color = new Color32(120, 0, 0, 100);
                break;
            case 7:
                gameObject.GetComponent<Image>().color = new Color32(255, 160, 0, 255);
                break;
        }
    }
}
