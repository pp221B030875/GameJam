using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoerController : MonoBehaviour
{
    private Animator anim;
    public GameObject leftLight, rightLight;
    public GameObject minimap;
    public Transform minimapBoer;

    public GameObject infectionLight;
    public GameObject oxygenLight;
    public GameObject stopPanel;


    [SerializeField] private float minimapMoveX, minimapMoveY;

    private bool leftBut,rightBut;
    private int forward; //0-up , 1-down , 2-left , 3-right

    [SerializeField] private float screenX,screenY;
    [SerializeField] private float maxX,minX,maxY,minY;
    public Vector3 curPos;

    public int noAttack;
    public bool rootsAttacked, parasitesAttacked;


    private void Start()
    {
        noAttack = 0;
        forward= 3;
        anim = GetComponent<Animator>();
        curPos = transform.position;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            minimap.SetActive(!minimap.activeSelf);
        }
    }
    private bool goUp()
    {
        if(curPos.y == maxY)
        {
            Debug.Log("Error");
            return false;
        }
        else
        {
            transform.position = new Vector3(curPos.x, curPos.y + screenY, curPos.z);
            curPos = transform.position;
            return true;
        }
    }
    private bool goDown()
    {
        if (curPos.y == minY)
        {
            Debug.Log("Error");
            return false;
        }
        else
        {
            transform.position = new Vector3(curPos.x, curPos.y - screenY, curPos.z);
            curPos = transform.position;
            return true;
        }
    }
    private bool goLeft()
    {
        if (curPos.x == minX)
        {
            Debug.Log("Error");
            return false;
        }
        else
        {
            transform.position = new Vector3(curPos.x - screenX, curPos.y , curPos.z);
            curPos = transform.position;
            return true;
        }
    }
    private bool goRight()
    {
        if (curPos.x == maxX)
        {
            Debug.Log("Error");
            return false;
        }
        else
        {
            transform.position = new Vector3(curPos.x + screenX, curPos.y , curPos.z);
            curPos = transform.position;
            return true;
        }
    }

    public void leftButton()
    {
        if (!leftBut)
        {
            leftBut = true;
            rightBut = false;
        }else leftBut = false;
        leftLight.SetActive(!leftLight.activeSelf);

    }
    public void rightButton()
    {
        if (!rightBut)
        {
            leftBut = false;
            rightBut = true;
        }else rightBut = false;
        rightLight.SetActive(!rightLight.activeSelf);
    }

    public void moveBoer()
    {
        if (noAttack > 0) noAttack--;
        else
        {
            rootsAttacked= false;
            parasitesAttacked= false;
        }
        
        if (leftBut)
        {
            switch (forward)
            {
                case 0:
                    if (goLeft())
                    {
                        forward = 2;
                        minimapBoer.localEulerAngles = new Vector3(0,0,90);
                        minimapBoer.position = new Vector3(minimapBoer.position.x - minimapMoveX, minimapBoer.position.y, minimapBoer.position.z);
                    }
                    break;
                case 1:
                    if (goRight())
                    {
                        forward = 3;
                        minimapBoer.localEulerAngles = new Vector3(0, 0, -90);
                        minimapBoer.position = new Vector3(minimapBoer.position.x + minimapMoveX, minimapBoer.position.y, minimapBoer.position.z);
                    }
                    break;
                case 2:
                    if (goDown())
                    {
                        forward = 1;
                        minimapBoer.localEulerAngles = new Vector3(0, 0, 180);
                        minimapBoer.position = new Vector3(minimapBoer.position.x, minimapBoer.position.y - minimapMoveY, minimapBoer.position.z);
                    }
                    break;
                case 3:
                    if (goUp())
                    {
                        forward = 0;
                        minimapBoer.localEulerAngles = new Vector3(0, 0, 0);
                        minimapBoer.position = new Vector3(minimapBoer.position.x, minimapBoer.position.y + minimapMoveY, minimapBoer.position.z);
                    }
                    break;
            }
        }
        else if(rightBut)
        {
            switch (forward)
            {
                case 0:
                    if (goRight())
                    {
                        forward = 3;
                        minimapBoer.localEulerAngles = new Vector3(0, 0, -90);
                        minimapBoer.position = new Vector3(minimapBoer.position.x + minimapMoveX, minimapBoer.position.y, minimapBoer.position.z);
                    }
                    break;
                case 1:
                    if (goLeft())
                    {
                        forward = 2;
                        minimapBoer.localEulerAngles = new Vector3(0, 0, 90);
                        minimapBoer.position = new Vector3(minimapBoer.position.x - minimapMoveX, minimapBoer.position.y, minimapBoer.position.z);
                    }
                    break;
                case 2:
                    if (goUp())
                    {
                        forward = 0;
                        minimapBoer.localEulerAngles = new Vector3(0, 0, 0);
                        minimapBoer.position = new Vector3(minimapBoer.position.x, minimapBoer.position.y + minimapMoveY, minimapBoer.position.z);
                    }
                    break;
                case 3:
                    if (goDown())
                    {
                        forward = 1;
                        minimapBoer.localEulerAngles = new Vector3(0, 0, 180);
                        minimapBoer.position = new Vector3(minimapBoer.position.x, minimapBoer.position.y - minimapMoveY, minimapBoer.position.z);
                    }
                    break;
            }
        }
        else
        {
            switch (forward)
            {
                case 0:
                    if (goUp()) minimapBoer.position = new Vector3(minimapBoer.position.x, minimapBoer.position.y + minimapMoveY, minimapBoer.position.z);
                    break;
                case 1:
                    if (goDown()) minimapBoer.position = new Vector3(minimapBoer.position.x, minimapBoer.position.y - minimapMoveY, minimapBoer.position.z);
                    break;
                case 2:
                    if(goLeft()) minimapBoer.position = new Vector3(minimapBoer.position.x - minimapMoveX, minimapBoer.position.y, minimapBoer.position.z);
                    break;
                case 3:
                    if(goRight()) minimapBoer.position = new Vector3(minimapBoer.position.x + minimapMoveX, minimapBoer.position.y, minimapBoer.position.z);
                    break;
            }
        }
    }

    public void FixBoer()
    {
        stopPanel.SetActive(false);
        oxygenLight.SetActive(false);
        infectionLight.SetActive(false);
    }
    public void BreakBoer()
    {
        stopPanel.SetActive(true);
    }
}
