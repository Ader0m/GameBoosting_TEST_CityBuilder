using UnityEngine;
using UnityEngine.EventSystems;

internal class TerraLogick
{
    private TerraInterface _terraInterface;
    private Camera _myCamera;
    private Ray _ray;
    private RaycastHit _hit;
    private Cell _cell;
    private uint _lastClick;


    public TerraLogick(TerraInterface terraInterface)
    {
        _terraInterface = terraInterface;
        _lastClick = 0;
    }

    public void TerraLogickFunc()
    {
        if (PlayerCamera.Instance.TryGetMyCamera(out _myCamera))
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
                if (_hit.collider.gameObject.TryGetComponent<Cell>(out _cell))
                {
                    if (_cell.TypeCell == TypeCellEnum.Water)
                    {
                        GameFieldLogick.Instance.SetCell(new Vector2(_cell.transform.position.x, _cell.transform.position.z), TypeCellEnum.Swamp);
                    }
                    else if (_cell.TypeCell == TypeCellEnum.Swamp)
                    {
                        GameFieldLogick.Instance.SetCell(new Vector2(_cell.transform.position.x, _cell.transform.position.z), TypeCellEnum.Sand);
                    }
                }
            }
        }
        _lastClick = InputListener.Instance.RaycastBytton;
    }
}

