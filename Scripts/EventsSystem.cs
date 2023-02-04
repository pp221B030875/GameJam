using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventsSystem : MonoBehaviour
{
    [SerializeField] private int chanceRoots = 2;
    [SerializeField] private bool chanceParasites;
    [SerializeField] private int chanceOxygen = 20;
    [SerializeField] private int chanceInfection = 20;

    [SerializeField] private BoerController boer;
    [SerializeField] private LeversChallange leversChallange;
    [SerializeField] private Text eventText;
    private Vector3 screenPos;

    private bool visited; //did we just move to this tail?
    private bool changedColorMap;
    [SerializeField] private Map tailMap;
    //[SerializeField] private int idOfEvent; 
    // 0-НападениеКорней; 1-УтечкаКислорода; 2-НападениеПаразитов
    // 3-ЗаражениеКабинки

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
            StartEvent();
        }
        else if(boer.curPos != screenPos)
        {
            visited = false;
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

        if (chanceParasites)
        {
            ParasitesAttack();
            boer.noAttack = 5;
            boer.BreakBoer();
            return;
        }

        if (randNum == 0 && boer.noAttack==0)
        {
            RootsAttack();
            boer.noAttack = 5;
            boer.BreakBoer();
            return;
        }
        randNum = Random.Range(0, chanceOxygen);

        if (randNum == 0 || boer.rootsAttacked && randNum < 2)
        {
            Oxygen();
            boer.oxygenLight.SetActive(true);
            boer.BreakBoer();
            return;
        }
        randNum = Random.Range(0, chanceInfection);

        if (randNum == 0 || boer.parasitesAttacked && randNum < 2)
        {
            Infection();
            boer.infectionLight.SetActive(true);
            boer.BreakBoer();
            return;
        }

    }

    private void RootsAttack()
    {
        Debug.Log("You are attacking by roots");
        eventText.text = "You are attacking by roots";
        leversChallange.StartChallenge();
        boer.rootsAttacked = true;
    }

    private void ParasitesAttack()
    {
        Debug.Log("You are attacking by parasites");
        eventText.text = "You are attacking by parasites";
        leversChallange.StartChallenge();
        boer.parasitesAttacked = true;
    }
    
    private void Oxygen()
    {
        Debug.Log("Oxygen Leak");
        leversChallange.StartChallenge();
        boer.oxygenLight.SetActive(!boer.oxygenLight.activeSelf);
    }

    private void Infection()
    {
        Debug.Log("The infection penetrates inside");
        leversChallange.StartChallenge();
        boer.infectionLight.SetActive(!boer.infectionLight.activeSelf);
    }

    private void TurnOfBoer()
    {

    }
}
