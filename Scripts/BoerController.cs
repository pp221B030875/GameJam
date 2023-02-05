using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoerController : MonoBehaviour
{

    public AudioSource button;
    public AudioSource gettingMatSound;
    public AudioSource alarm;
    public AudioSource boerSoundA;

    private Animator anim;
    public GameObject leftLight, rightLight;
    public GameObject leversChallengeMenu;
    public Transform minimapBoer;

    [SerializeField] private GameObject startMessage;
    [SerializeField] private GameObject finishMessage;
    [SerializeField] private GameObject pauseMenu;
    public GameObject roots;

    public GameObject stopPanel;
    [SerializeField] private GameObject illuminationOff;
    [SerializeField] private LeversChallange leversChallange;
    [SerializeField] private Text eventText;

    [SerializeField] private Slider lever;
    [SerializeField] private float timerBeforeGo;
    public float timerToFixBoer;

    [SerializeField] private float minimapMoveX, minimapMoveY;

    private bool leftBut,rightBut;
    private int forward; //0-up , 1-down , 2-left , 3-right

    [SerializeField] private float screenX,screenY;
    [SerializeField] private float maxX,minX,maxY,minY;
    public Vector3 curPos;


    private bool startFilterForOxygen, startFilterForToxic;
    public GameObject infectionLight;
    public GameObject oxygenLight;
    public Text oxygenNormalModeText;
    public Text toxicNormalModeText;
    public Image oxygenBar;
    public Image toxicBar;

    public int noAttack;
    public bool rootsAttacked;
    public bool filtersEvent, rootsEvent;

    public bool hasLever, rightKeys, checkedKeys, createdArray;

    public Journal journal;

    public GameObject getMaterialLight;
    [SerializeField] private bool gettingMaterial;
    public bool gotMaterial;
    [SerializeField] private float timerBeforeGetMaterial;


    public bool highTemperature;
    public GameObject indicatorTemp;
    public Image temperatureBar;

    private void Start()
    {
        roots.SetActive(false);
        finishMessage.SetActive(false);
        startMessage.SetActive(true);

        journal = GetComponent<Journal>();
        getMaterialLight.SetActive(true);
        gettingMaterial = false;
        gotMaterial = false;

        filtersEvent = false;
        rootsEvent = false;

        startFilterForOxygen = false;
        startFilterForToxic = false;
        oxygenNormalModeText.text = "Normal Mode";
        toxicNormalModeText.text = "Normal Mode";

        indicatorTemp.SetActive(false);
        highTemperature = false;
        illuminationOff.SetActive(false);
        timerBeforeGo = 7;
        noAttack = 5;
        forward= 3;
        anim = GetComponent<Animator>();
        curPos = transform.position;
    }

    private void Update()
    {
        if(lever.value == 0)
        {
            if (boerSoundA.volume > 0)
            {
                boerSoundA.volume -= (float)(Time.deltaTime*0.1);
                boerSoundA.Stop();
            }
            timerBeforeGo = 7;
        }
        if(timerBeforeGo<=0 && lever.value == 1)
        {
            moveBoer();
            lever.value = 0;
        }
        else if(timerBeforeGo>0 && lever.value == 1)
        {
            if (boerSoundA.volume < 0.5)
            {
                boerSoundA.volume += (float)(Time.deltaTime);
                boerSoundA.Play();
            }
            timerBeforeGo -= Time.deltaTime;
        }

        if (rootsAttacked || oxygenLight.activeSelf || infectionLight.activeSelf)
        {
            if(timerToFixBoer <= 0)
            {
                YouAreDead();
            }

            timerToFixBoer -= Time.deltaTime;
        }

        if (startFilterForOxygen && filtersEvent && oxygenLight.activeSelf)
        {
            oxygenBar.fillAmount += (float)(Time.deltaTime * 0.1);

            if(oxygenBar.fillAmount == 1)
            {
                startFilterForOxygen = false;
                oxygenNormalModeText.text = "Normal Mode";
                FixBoer();
                filtersEvent = false;
            }
        }
        if (startFilterForToxic && filtersEvent && infectionLight.activeSelf)
        {
            toxicBar.fillAmount += (float)(Time.deltaTime * 0.1);

            if (toxicBar.fillAmount == 1)
            {
                startFilterForToxic = false;
                toxicNormalModeText.text = "Normal Mode";
                FixBoer();
                filtersEvent = false;
            }
        }

        if (gettingMaterial)
        {
            BreakBoer();
            timerBeforeGetMaterial -= Time.deltaTime;
            if (timerBeforeGetMaterial < 0)
            {
                gettingMatSound.Stop();
                FixBoer();  
                gettingMaterial= false;
                gotMaterial = true;
                getMaterialLight.SetActive(true);
                journal.UpdateMission();
            }
        }

        if (highTemperature)
        {
            temperatureBar.fillAmount += (float)(Time.deltaTime * 0.05);
            if (temperatureBar.fillAmount == 1)
            {
                YouAreDead();
            }
        }
        else
        {
            indicatorTemp.SetActive(false);
            temperatureBar.fillAmount = 0;
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
        button.Play();
        if (!leftBut)
        {
            leftBut = true;
            rightBut = false;
            rightLight.SetActive(false);
        }
        else leftBut = false;
        leftLight.SetActive(!leftLight.activeSelf);

    }
    public void rightButton()
    {
        button.Play();
        if (!rightBut)
        {
            leftBut = false;
            rightBut = true;
            leftLight.SetActive(false);
        }
        else rightBut = false;
        rightLight.SetActive(!rightLight.activeSelf);
    }

    public void moveBoer()
    {
        temperatureBar.fillAmount = 0;
        if (noAttack > 0) noAttack--;
        else
        {
            rootsAttacked= false;
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

    public void OpenCloseLeversChallenge()
    {
        button.Play();
        leversChallengeMenu.SetActive(!leversChallengeMenu.activeSelf);
        if (!createdArray && illuminationOff.activeSelf)
        {
            leversChallange.StartChallenge();
        }
    }

    public void FixBoer()
    {
        alarm.Stop();
        eventText.text = "";
        illuminationOff.SetActive(false);
        stopPanel.SetActive(false);
        oxygenLight.SetActive(false);
        infectionLight.SetActive(false);

        rootsAttacked = false;
        rootsEvent = false;
    }
    public void BreakBoer()
    {
        lever.value = 0;
        stopPanel.SetActive(true);
    }

    public void TurnOfBoer()
    {
        roots.SetActive(false);

        hasLever = false;
        checkedKeys = false;
        createdArray = false;
        rightKeys = false;

        BreakBoer();
        gettingMaterial = false;
        lever.value = 0;
        illuminationOff.SetActive(true);
        leversChallange.StartChallenge();
    }

    public void StartFiltersForOxygen()
    {
        button.Play();
        startFilterForOxygen = true;
    }
    public void StartFiltersForToxic()
    {
        button.Play();
        startFilterForToxic = true; 
    }

    public void GetMaterial()
    {
        button.Play();
        gettingMatSound.Play();
        gettingMaterial= true;
        timerBeforeGetMaterial = 10;
    }

    private void YouAreDead()
    {
        Debug.Log("You are dead");
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        button.Play();
        startMessage.SetActive(false);
    }

    public void FinishGame()
    {
        finishMessage.SetActive(true);
    }
    public void StartAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenClosePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
