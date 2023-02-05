using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Journal : MonoBehaviour
{
    //Mission block
    [SerializeField] private Text missionText;

    //Info block
    [SerializeField] private GameObject eventsText;
    [SerializeField] private GameObject tempetureText;
    [SerializeField] private GameObject finishText;
    public int numberMaterials;

    private void Start()
    {
        numberMaterials = 0;
        missionText.text = string.Format("Required :\r\n-Alumnium {0}/4", numberMaterials);
    }

    public void ShowEventsText()
    {
        tempetureText.SetActive(false);
        finishText.SetActive(false);
        eventsText.SetActive(!eventsText.activeSelf);
    }

    public void ShowTemperatureText()
    {
        eventsText.SetActive(false);
        finishText.SetActive(false);
        tempetureText.SetActive(!tempetureText.activeSelf);
    }

    public void UpdateMission()
    {
        numberMaterials++;
        missionText.text = string.Format("Required :\r\n-Alumnium {0}/4", numberMaterials);
    }
    public void ShowFinishMission()
    {
        tempetureText.SetActive(false);
        eventsText.SetActive(false);
        finishText.SetActive(!finishText.activeSelf);
    }
}
