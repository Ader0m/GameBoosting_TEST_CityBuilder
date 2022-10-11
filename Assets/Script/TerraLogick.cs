using UnityEngine;
using UnityEngine.EventSystems;

internal class TerraLogick
{
    private TerraInterface _terraInterface;
    private Camera _myCamera;
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
        if(PlayerCamera.Instance.TryGetMyCamera(out _myCamera))
        {
            _ray = _myCamera.ScreenPointToRay(Input.mousePosition);                       
            StartTerraforming();
        }
    }

    private void StartTerraforming()
    {
        if (InputListener.Instance.RaycastBytton != _lastClick && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Physics.Raycast(_ray, out _hit, 30f))
            {
                if (_hit.collider.gameObject.TryGetComponent<Cell>(out cell))
                {
                    if (cell.TypeCell == TypeCellEnum.Water)
                    {
                        GameFieldLogick.Instance.SetSwampCell(new Vector2(cell.transform.position.x, cell.transform.position.z));
                    }
                    else if (cell.TypeCell == TypeCellEnum.Swamp)
                    {
                        GameFieldLogick.Instance.SetSandCell(new Vector2(cell.transform.position.x, cell.transform.position.z));
                    }
                }
            }
        }
        _lastClick = InputListener.Instance.RaycastBytton;
    }
}

