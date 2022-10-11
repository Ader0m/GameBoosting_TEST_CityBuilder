using UnityEngine;
using UnityEngine.EventSystems;

internal class InfoLogick
{
    private InfoInterface _infoInterface;
    private Camera _myCamera;
    private Ray _ray;
    private RaycastHit _hit;
    private IBuilding _building;
    private uint _lastClick;

    #region Get/Set

    public IBuilding GetBuilding()
    {
        return _building;
    }

    #endregion

    public InfoLogick(InfoInterface infoInterface)
    {
        _infoInterface = infoInterface;
    }

    public void InfoLogickFunc()
    {
        if (InputListener.Instance.RaycastBytton != _lastClick && !EventSystem.current.IsPointerOverGameObject())
        {
            if (PlayerCamera.Instance.TryGetMyCamera(out _myCamera))
            {
                _ray = _myCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(_ray, out _hit, 30f))
                {
                    if (_hit.collider.gameObject.TryGetComponent<IBuilding>(out _building))
                    {
                        _infoInterface.InfoMenuPanel.gameObject.SetActive(true);
                    }
                    else
                    {
                        _infoInterface.InfoMenuPanel.gameObject.SetActive(false);
                    }
                }
                else
                {
                    _infoInterface.InfoMenuPanel.gameObject.SetActive(false);
                }
            }
        }

        _lastClick = InputListener.Instance.RaycastBytton;
    }
}
