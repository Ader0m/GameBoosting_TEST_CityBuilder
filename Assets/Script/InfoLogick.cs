using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

internal class InfoLogick
{
    private InfoInterface _infoInterface;
    private Ray _ray;
    private RaycastHit _hit;
    private IBuilding building;
    private uint _lastClick;

    #region Get/Set

    public IBuilding GetBuilding()
    {
        return building;
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
            _ray = PlayerCamera.Instance.GetMyCamera().ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, 30f))
            {
                if (_hit.collider.gameObject.TryGetComponent<IBuilding>(out building))
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

        _lastClick = InputListener.Instance.RaycastBytton;
    }


}
