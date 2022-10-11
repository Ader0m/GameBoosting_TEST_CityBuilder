using UnityEngine;

internal class GameFieldMV : MonoBehaviour
{
    #region Singleton

    public static GameFieldMV Instance => _instance;
    private static GameFieldMV _instance;

    #endregion

    [SerializeField] private GameObject _smallBuild;
    [SerializeField] private GameObject _mediumBuild;
    [SerializeField] private GameObject _largeBuild;
    [SerializeField] private GameObject _cell;
    [SerializeField] private GameObject _barrier;
    [SerializeField] private Transform _buildingLayer;
    [SerializeField] private Transform _cellLayer;
 

    private void Awake()
    {
        _instance = this;
        _barrier = Instantiate(_barrier);
        _buildingLayer = Instantiate(_buildingLayer);
        _cellLayer = Instantiate(_cellLayer);
    }

    /// <summary>
    /// Производит все необходимые действия для появлеия здания в игре. Регистрирует здание с помощью функции 
    /// GameFieldLogick.Instance.SetTownCell(point, size);
    /// </summary>
    /// <param name="point"> Точка применения </param>
    /// <param name="size"> Тип здания </param>
    public void AddBuilding(Vector2 point, SizeBuildEnum size)
    {
        GameFieldLogick.Instance.SetTownCell(point, size);
        switch (size)
        {
            case SizeBuildEnum.SmallBuilding:
                {
                    GameObject obj = Instantiate(_smallBuild, _buildingLayer);
                    obj.GetComponent<Building>().SetPoint(point);
                    obj.GetComponent<Building>().SetSizeBuild(size);
                    obj.transform.position = new Vector3(obj.transform.position.x + point.x, obj.transform.position.y,
                        obj.transform.position.z + point.y);
                    GameFieldLogick.Instance.BuildingList.Add(obj);
                    break;
                }
            case SizeBuildEnum.MediumBuilding:
                {
                    GameObject obj = Instantiate(_mediumBuild, _buildingLayer);
                    obj.GetComponent<Building>().SetPoint(point);
                    obj.GetComponent<Building>().SetSizeBuild(size);
                    obj.transform.position = new Vector3(obj.transform.position.x + point.x, obj.transform.position.y,
                        obj.transform.position.z + point.y);
                    GameFieldLogick.Instance.BuildingList.Add(obj);
                    break;
                }
            case SizeBuildEnum.LargeBuilding:
                {
                    GameObject obj = Instantiate(_largeBuild, _buildingLayer);
                    obj.GetComponent<Building>().SetPoint(point);
                    obj.GetComponent<Building>().SetSizeBuild(size);
                    obj.transform.position = new Vector3(obj.transform.position.x + point.x, obj.transform.position.y,
                        obj.transform.position.z + point.y);
                    GameFieldLogick.Instance.BuildingList.Add(obj);
                    break;
                }
        }
    }

    /// <summary>
    /// Производит все необходимые действия для удаления здания в игре. Регистрирует удаление здания с помощью функции 
    /// GameFieldLogick.Instance.ClearTownCell(point, size);
    /// </summary>
    /// <param name="building"> Игровой объект содержащий интерфейс IBuilding </param>
    public void RemoveBuilding(GameObject building)
    {
        GameFieldLogick.Instance.BuildingList.Remove(building);

        GameFieldLogick.Instance.ClearTownCell(new Vector2(building.GetComponent<IBuilding>().GetPoint().x,
                                                building.GetComponent<IBuilding>().GetPoint().y), building.GetComponent<IBuilding>().GetSizeBuild());
        Destroy(building);        
    }

    /// <summary>
    /// По !Заполненной! информации из GameFieldLogick отрисовывает клетки игрового поля
    /// Создает объекты Сell и помещает их в массив GameFieldLogick.Instance.CellsMass
    /// Если игра была загружена с файла запускает заполнение BuildbleGameFieldMass
    /// </summary>
    /// <param name="ReLoadFlag"> Загрузка с файла </param>
    public void DrawGameField(bool ReLoadFlag)
    { 
        for (int i = 0; i < GameFieldLogick.Instance.TypeGameFieldMass.Length; i++)
        {
            GameObject obj = Instantiate(_cell, _cellLayer);
            obj.GetComponent<Cell>().InitCell(Game.Instance.CellSpriteMass[GameFieldLogick.Instance.TypeGameFieldMass[i]],(TypeCellEnum) GameFieldLogick.Instance.TypeGameFieldMass[i]);
            obj.transform.position = new Vector3(i / GameFieldLogick.Instance.SizeGameField, 0, i % GameFieldLogick.Instance.SizeGameField);
            GameFieldLogick.Instance.CellsMass[i] = obj.GetComponent<Cell>();

            if (ReLoadFlag)
            {
                if(GameFieldLogick.Instance.TypeGameFieldMass[i] == (int)TypeCellEnum.Grass ||
                     GameFieldLogick.Instance.TypeGameFieldMass[i] == (int)TypeCellEnum.Sand
                    )
                {
                    GameFieldLogick.Instance.BuildbleGameFieldMass[i] = true;
                }
                else
                {
                    GameFieldLogick.Instance.BuildbleGameFieldMass[i] = false;
                }
            }      
        }

        _barrier.transform.position = new Vector3(GameFieldLogick.Instance.SizeGameField / 2, GameFieldLogick.Instance.SizeGameField / 4, GameFieldLogick.Instance.SizeGameField / 2);
        _barrier.transform.localScale = new Vector3(GameFieldLogick.Instance.SizeGameField, GameFieldLogick.Instance.SizeGameField / 2, GameFieldLogick.Instance.SizeGameField);
    }

    private void OnDestroy()
    {
        _instance = null;
        _barrier = null;
        _buildingLayer = null;
        _cellLayer = null;
    }
}

