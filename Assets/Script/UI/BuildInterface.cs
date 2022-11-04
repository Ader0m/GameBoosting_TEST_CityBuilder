using UnityEngine;
using UnityEngine.UI;

public class BuildInterface: MonoBehaviour
{
    [SerializeField] public Text DebugText;
    [SerializeField] public Button SmallButton;
    [SerializeField] public Button MediumButton;
    [SerializeField] public Button LargeButton;
    [SerializeField] public Material RedMaterial;
    [SerializeField] public Material GreenMaterial;
    [SerializeField] public GameObject CurrentTemplate;
    private BuildLogick _buildLogick;


    public void Awake()
    {        
        _buildLogick = new BuildLogick(this);
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

    public void OffButton()
    {
        SmallButton.GetComponent<Outline>().enabled = false;
        MediumButton.GetComponent<Outline>().enabled = false;
        LargeButton.GetComponent<Outline>().enabled = false;
    }

    void Update()
    {        
        _buildLogick.BuildLogickFunc();
    }
}
