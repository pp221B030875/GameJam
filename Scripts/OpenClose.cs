using UnityEngine;

public class OpenClose : MonoBehaviour
{
    public AudioSource button;

    [SerializeField] private GameObject minimap;
    [SerializeField] private GameObject menuOfFixes;
    [SerializeField] private GameObject filterOxygen;
    [SerializeField] private GameObject filterToxic;
    [SerializeField] private GameObject console;
    [SerializeField] private GameObject jouranal;
    [SerializeField] private GameObject mission;
    [SerializeField] private GameObject info;


    public void OpenCloseMap()
    {
        button.Play();
        minimap.SetActive(!minimap.activeSelf);
    }

    public void OpenCloseConsole()
    {
        button.Play();
        console.SetActive(!console.activeSelf);
    }
    public void OpenCloseMenuFixes()
    {
        button.Play();
        menuOfFixes.SetActive(!menuOfFixes.activeSelf);
    }
    public void OpenCloseFilterOxygen()
    {
        button.Play();
        filterOxygen.SetActive(!filterOxygen.activeSelf);
    }
    public void OpenCloseFilterToxic()
    {
        button.Play();
        filterToxic.SetActive(!filterToxic.activeSelf);
    }
    public void OpenCloseMission()
    {
        button.Play();
        mission.SetActive(!mission.activeSelf);
    }

    public void OpenCloseJournal()
    {
        button.Play();
        jouranal.SetActive(!jouranal.activeSelf);
    }
    public void OpenCloseInfo()
    {
        button.Play();
        info.SetActive(!info.activeSelf);
    }
}
