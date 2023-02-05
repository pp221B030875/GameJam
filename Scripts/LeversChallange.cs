using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LeversChallange : MonoBehaviour
{
    [SerializeField] private Slider[] levers;
    private int[] keyNums;
    private int randNum;

    [SerializeField] private GameObject invisiblePanel;
    [SerializeField] private GameObject fixedText;
    [SerializeField] private BoerController boer;


    public void StartChallenge()
    {
        keyNums = new int[4];
        RestartLevers();

        while (!boer.hasLever)
        {
            for (int i = 0; i < 4; i++)
            {
                randNum = Random.Range(0, 2);
                if (randNum == 1)
                {
                    boer.hasLever = true;
                }
                keyNums[i] = randNum;
            }
            
        }
        for(int i = 0; i < 4; i++)
        {
            Debug.Log(keyNums[i]);
        }
        boer.createdArray = true;
    }

    private void Update()
    {
        if (boer.rightKeys)
        {
            fixedText.SetActive(true);
            invisiblePanel.SetActive(true);
            boer.FixBoer();
        }
        if(boer.createdArray && boer.hasLever)
        {
            for(int i = 0; i < 4; i++)
            {
                if (keyNums[i] == levers[i].value)
                {
                    boer.checkedKeys = true;
                }
                else
                {
                    boer.checkedKeys = false;
                    break;
                }
            }
            if (boer.checkedKeys){
                boer.rightKeys = true;
            }
        }
    }

    private void RestartLevers()
    {
        invisiblePanel.SetActive(false);
        fixedText.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            levers[i].value = 0;
        }
    }
}
