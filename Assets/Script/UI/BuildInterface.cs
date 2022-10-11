using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildInterface: MonoBehaviour
{
    [SerializeField] public Text DebugText;
    [SerializeField] public Button SmallButton;
    [SerializeField] public Button MediumButton;
    [SerializeField] public Button LargeButton;
    [SerializeField] public GameObject RedTemplate;
    [SerializeField] public GameObject GreenTemplate;
    [SerializeField] public GameObject CurrentTemplate;
    private BuildLogick buildLogick;


    void Start()
    {        
        buildLogick = new BuildLogick(this);
    }

    public void ActiveSmall()
    {
        SmallButton.GetComponent<Outline>().enabled = true;
        MediumButton.GetComponent<Outline>().enabled = false;
        LargeButton.GetComponent<Outline>().enabled = false;
    }

    public void ActiveMedium()
    {
        SmallButton.GetComponent<Outline>().enabled = false;
        MediumButton.GetComponent<Outline>().enabled = true;
        LargeButton.GetComponent<Outline>().enabled = false;
    }

    public void ActiveLarge()
    {
        SmallButton.GetComponent<Outline>().enabled = false;
        MediumButton.GetComponent<Outline>().enabled = false;
        LargeButton.GetComponent<Outline>().enabled = true;
    }

    void Update()
    {        
        buildLogick.BuildLogickFunc();
    }

    public void Debug(float x, float z)
    {
        DebugText.gameObject.SetActive(true);
        DebugText.text = x + " " + z;
    }
}
