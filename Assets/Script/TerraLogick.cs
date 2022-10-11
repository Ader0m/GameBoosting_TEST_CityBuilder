using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


internal class TerraLogick
{
    private TerraInterface _terraInterface;
    private Ray _ray;
    private RaycastHit _hit;
    private Cell cell;
    private uint _lastClick;

    public TerraLogick(TerraInterface terraInterface)
    {
        _terraInterface = terraInterface;
        _lastClick = 0;
    }

    public void TerraLogickFunc()
    {
        _ray = PlayerCamera.Instance.GetMyCamera().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, 30f))
            StartTerraforming();
    }

    private void StartTerraforming()
    {
        if (InputListener.Instance.RaycastBytton != _lastClick && !EventSystem.current.IsPointerOverGameObject())
        {
            if (_hit.collider.gameObject.TryGetComponent<Cell>(out cell))
            {
                if (cell.TypeCell == TypeCellEnum.Water)
                {
                    GameFieldModel.Instance.SetSwampCell(new Vector2(cell.transform.position.x, cell.transform.position.z));
                }
                else if (cell.TypeCell == TypeCellEnum.Swamp)
                {
                    GameFieldModel.Instance.SetSandCell(new Vector2(cell.transform.position.x, cell.transform.position.z));
                }
            }
        }
        _lastClick = InputListener.Instance.RaycastBytton;
    }
}

