using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventsSystem : MonoBehaviour
{
    [SerializeField] private int chanceRoots = 2;
    [SerializeField] private int chanceOxygen = 20;
    [SerializeField] private int chanceInfection = 20;

    [SerializeField] private BoerController boer;
    [SerializeField] private Text eventText;
    private Vector3 screenPos;

    private bool visited; //did we just move to this tail?
    private bool changedColorMap;
    [SerializeField] private Map tailMap;
    //[SerializeField] private int idOfEvent; 
    // 0-НападениеКорней; 1-УтечкаКислорода; 2-НападениеПаразитов
    // 3-ЗаражениеКабинки

    [SerializeField] private bool tailMaterial,highTempTail,saveTail,finishTail;


    private void Start()
    {
        visited = false;
        changedColorMap = false;
        screenPos = new Vector3(transform.position.x, transform.position.y, -10);
    }

    private void Update()
    {
        if(boer.curPos == screenPos && !visited)
        {
            visited = true;
            if (finishTail && boer.journal.numberMaterials == 4)
            {
                boer.FinishGame();
                return;
            }
            if (tailMaterial)
            {
                boer.highTemperature = false;
                boer.getMaterialLight.SetActive(false);
            }
            else if (highTempTail)
            {
                HighTemperature();
            }
            else if(!saveTail)
            {
                boer.highTemperature = false;
                boer.getMaterialLight.SetActive(true);
                StartEvent();
            }
            else
            {
                visited = true;
            }
        }
        else if(boer.curPos != screenPos)
        {
            visited = false;
        }
        if(boer.gotMaterial == true)
        {
            tailMaterial = false;
            if (tailMap.hasMaterial)
            {
                tailMap.GotMaterial();
            }
            boer.gotMaterial = false;
        }

        if(!changedColorMap && visited)
        {
            changedColorMap = true;
            tailMap.ChangeColor();
        }
    }



    public void StartEvent()
    {
        int randNum = Random.Range(0,chanceRoots);

        if (randNum == 0 && boer.noAttack==0)
        {
            RootsAttack();
            boer.noAttack = 5;
            return;
        }
        randNum = Random.Range(0, chanceOxygen);

        if (randNum == 0 || boer.rootsAttacked && randNum < 2)
        {
            Oxygen();
            boer.oxygenLight.SetActive(true);
            return;
        }
        randNum = Random.Range(0, chanceInfection);

        if (randNum == 0 || boer.rootsAttacked && randNum < 2)
        {
            Infection();
            boer.infectionLight.SetActive(true);
            return;
        }

    }

    private void RootsAttack()
    {
        boer.roots.SetActive(true);
        boer.alarm.Play();
        boer.timerToFixBoer = 40;
        Debug.Log("You are attacking by roots");
        eventText.text = "You are attacking by roots";
        boer.rootsAttacked = true;
        boer.rootsEvent = true;
    }
    
    private void Oxygen()
    {
        boer.alarm.Play();
        boer.timerToFixBoer = 40;
        Debug.Log("Oxygen Leak");
        boer.oxygenLight.SetActive(!boer.oxygenLight.activeSelf);
        boer.oxygenBar.fillAmount = 0;
        boer.oxygenNormalModeText.text = "Danger Mode";
        boer.filtersEvent = true;   
    }

    private void Infection()
    {
        boer.alarm.Play();
        boer.timerToFixBoer = 60;
        Debug.Log("The infection penetrates inside");
        boer.infectionLight.SetActive(!boer.infectionLight.activeSelf);
        boer.toxicBar.fillAmount = 0;
        boer.toxicNormalModeText.text = "Danger Mode";
        boer.filtersEvent = true;
    }

    private void HighTemperature()
    {
        boer.indicatorTemp.SetActive(true);
        boer.temperatureBar.fillAmount = 0;
        boer.highTemperature = true;
    }
}
