using UnityEngine;
using UnityEngine.UI;

public class InfoInterface : MonoBehaviour
{
    [SerializeField] public Button InfoButton;
    [SerializeField] public Button DeleteButton;
    [SerializeField] public RectTransform InfoMenuPanel;
    [SerializeField] public GameObject StatPanelPrefab;
    private InfoLogick _infoLogick;


    public void Awake()
    {
        _infoLogick = new InfoLogick(this);
    }

    void Update()
    {
        _infoLogick.InfoLogickFunc();
    }

    public void ShowInfo()
    {
        GameObject obj = Instantiate(StatPanelPrefab, transform);
        obj.GetComponent<InfoPanel>().InitPanel(_infoLogick.GetBuilding().GetSizeBuild());
    }

    public void DeleteBuilding()
    {
        GameFieldDraw.Instance.RemoveBuilding(_infoLogick.GetBuilding().GameObject());
        InfoMenuPanel.gameObject.SetActive(false);
    }
}
