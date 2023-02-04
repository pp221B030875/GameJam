using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LeversChallange : MonoBehaviour
{
    [SerializeField] private List<Slider> levers;
    private int[] keyNums = new int[4];
    private int randNum;

    private bool hasLever;
    private bool rightKeys;
    private bool checkedKeys;

    [SerializeField] private GameObject invisiblePanel;
    [SerializeField] private GameObject fixedText;
    [SerializeField] private BoerController boer;

    private void Start()
    {
        hasLever = false;
        rightKeys = false;
        checkedKeys = false;
    }

    public void StartChallenge()
    {
        RestartLevers();

        while (!hasLever)
        {
            for (int i = 0; i < 4; i++)
            {
                randNum = Random.Range(0, 2);
                if (randNum == 1)
                {
                    hasLever = true;
                }
                keyNums[i] = randNum;
            }
        }
        
    }

    private void Update()
    {
        if (rightKeys)
        {
            fixedText.SetActive(true);
            invisiblePanel.SetActive(true);
            boer.FixBoer();
        }
        else
        {
            for(int i = 0; i < 4; i++)
            {
                if (keyNums[i] == levers[i].value)
                {
                    checkedKeys= true;
                }
                else checkedKeys = false;
            }
            if (checkedKeys){
                rightKeys= true;
            }
        }
    }

    private void RestartLevers()
    {
        rightKeys = false;
        invisiblePanel.SetActive(false);
        fixedText.SetActive(false);
        hasLever = false;
        for (int i = 0; i < 4; i++)
        {
            levers[i].value = 0;
        }
    }
}
