using UnityEngine;

internal class Cell: MonoBehaviour
{
    public SpriteRenderer SpriteRender;
    public TypeCellEnum TypeCell;


    public void InitCell(Sprite sprite, TypeCellEnum typeCell)
    {
        TypeCell = typeCell;
        SpriteRender.sprite = sprite;
    }

    private void Awake()
    {        
        SpriteRender = GetComponent<SpriteRenderer>();
    }

    public void Refresh()
    {           
        if (SpriteRender != null)
        {
            SpriteRender.sprite = Game.Instance.CellSpriteMass[GameFieldLogick.Instance.TypeGameFieldMass[(int)(transform.position.x * 100 + transform.position.z % 100)]];
            TypeCell = (TypeCellEnum) GameFieldLogick.Instance.TypeGameFieldMass[(int)(transform.position.x * 100 + transform.position.z % 100)];
        }
    }           
}

