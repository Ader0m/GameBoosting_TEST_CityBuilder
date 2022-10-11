using UnityEngine;
using UnityEngine.UI;

internal class InfoPanel : MonoBehaviour
{
    [SerializeField] public Text TitleSizeEnum;
    [SerializeField] public Text TitleCountCell;
    [SerializeField] public Text SizeEnum;
    [SerializeField] public Text XCountCell;


    public void InitPanel(SizeBuildEnum size)
    {
        SizeEnum.text = size.ToString();
        XCountCell.text = (int) size + "X" + (int) size;
    }

    public void DestroyPanel()
    {
        Destroy(this.gameObject);
    }
}
