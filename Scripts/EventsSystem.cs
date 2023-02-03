using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsSystem : MonoBehaviour
{
    [SerializeField] private int chanceRoots = 2;
    [SerializeField] private bool chanceParasites;
    [SerializeField] private int chanceOxygen = 20;
    [SerializeField] private int chanceInfection = 20;

    [SerializeField] private BoerController boer;
    private Vector3 screenPos;

    private bool visited;
    //[SerializeField] private int idOfEvent; 
    // 0-НападениеКорней; 1-УтечкаКислорода; 2-НападениеПаразитов
    // 3-ЗаражениеКабинки

    private void Start()
    {
        visited = false;
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
        boer.rootsAttacked = true;
    }

    private void ParasitesAttack()
    {
        Debug.Log("You are attacking by parasites");
        boer.parasitesAttacked = true;
    }
    
    private void Oxygen()
    {
        Debug.Log("Oxygen Leak");
        boer.oxygenLight.SetActive(!boer.oxygenLight.activeSelf);
    }

    private void Infection()
    {
        Debug.Log("The infection penetrates inside");
        boer.infectionLight.SetActive(!boer.infectionLight.activeSelf);
    }

    private void TurnOfBoer()
    {

    }
}
