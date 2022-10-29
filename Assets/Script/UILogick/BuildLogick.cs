using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal class BuildLogick
{
    private SizeBuildEnum _sizeBuild;
    private Camera _myCamera;
    private BuildInterface _buildInterface;
    private Cell _cell;
    private Ray _ray;
    private RaycastHit _hit;
    private Vector3 _pos;
    private Vector2 _lastCell;
    private uint _lastClick;
    private bool _accept;


    public BuildLogick(BuildInterface buildInterface)
    {
        _buildInterface = buildInterface;
        _lastCell = new Vector2(-1, -1);
        _accept = false;
        _buildInterface.CurrentTemplate = MonoBehaviour.Instantiate(_buildInterface.GreenTemplate);
        _buildInterface.CurrentTemplate.transform.position = new Vector3(-100, 0.5f, -100); // недостижимая точка при люом размере карты
    }

    public void BuildLogickFunc()
    {
        if (_myCamera == null)
        {
            PlayerCamera.Instance.TryGetMyCamera(out _myCamera);
        }
        else
        {
            _ray = _myCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, 30f))
                PrepaireBuild();
        }                   
      
        if (InputListener.Instance.RaycastBytton != _lastClick && _accept && !EventSystem.current.IsPointerOverGameObject())
        {
            Build();
            _accept = false;
        }

        _lastClick = InputListener.Instance.RaycastBytton;
    }

    public void Build()
    {
        GameFieldDraw.Instance.AddBuilding(_lastCell, _sizeBuild);
    }

    public void PrepaireBuild()
    {
        if (_hit.collider.gameObject.TryGetComponent<Cell>(out _cell))
        {
            _pos = _cell.gameObject.transform.position;

            if ((_lastCell.x != _pos.x || _lastCell.y != _pos.z) && _pos != null)
            {
                _lastCell.x = _pos.x;
                _lastCell.y = _pos.z;

                MonoBehaviour.Destroy(_buildInterface.CurrentTemplate.gameObject);
                switch (_buildInterface.SmallButton.GetComponent<Outline>().enabled,
                                _buildInterface.MediumButton.GetComponent<Outline>().enabled,
                                _buildInterface.LargeButton.GetComponent<Outline>().enabled
                                )
                {
                    case (true, false, false):
                        {
                            SmallBuildGoust();
                            break;
                        }
                    case (false, true, false):
                        {
                            MediumBuildGoust();
                            break;
                        }
                    case (false, false, true):
                        {
                            LargeBuildGoust();
                            break;
                        }
                }
            }
        }
    }

    private void SmallBuildGoust()
    {
        GoustTypeAndBuildAccess(SizeBuildEnum.SmallBuilding);

        _buildInterface.CurrentTemplate.transform.position = new Vector3(_lastCell.x, 0.5f, _lastCell.y);       
    }

    private void MediumBuildGoust()
    {
        GoustTypeAndBuildAccess(SizeBuildEnum.MediumBuilding);

        _buildInterface.CurrentTemplate.transform.position = new Vector3(_lastCell.x + 0.5f, 1f, _lastCell.y + 0.5f);
    }

    private void LargeBuildGoust()
    {       
        GoustTypeAndBuildAccess(SizeBuildEnum.LargeBuilding);

        _buildInterface.CurrentTemplate.transform.position = new Vector3(_lastCell.x, 1.5f, _lastCell.y);
    }

    /// <summary>
    /// Проверяет можно ли построить данное здание.
    /// Спавнит призрака.
    /// Выставляет ему размер, соответстующий строению.
    /// </summary>
    /// <param name="sizeBuild"></param>
    private void GoustTypeAndBuildAccess(SizeBuildEnum sizeBuild)
    {
        _sizeBuild = sizeBuild;

        if (GameFieldLogick.Instance.CheckClearSpaceForBuilding(_lastCell, sizeBuild))
        {

            _buildInterface.CurrentTemplate = MonoBehaviour.Instantiate(_buildInterface.GreenTemplate);
            _accept = true;
        }
        else
        {
            _buildInterface.CurrentTemplate = MonoBehaviour.Instantiate(_buildInterface.RedTemplate);
            _accept = false;
        }
        _buildInterface.CurrentTemplate.transform.localScale = Vector3.one * (int)sizeBuild;
    }
}