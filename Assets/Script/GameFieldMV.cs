using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    

    //private void Start()
    //{
    //    _instance = this;
    //    _barrier = Instantiate(_barrier);
    //    _buildingLayer = Instantiate(_buildingLayer);
    //    _cellLayer = Instantiate(_cellLayer);
    //}

    private void Awake()
    {
        _instance = this;
        _barrier = Instantiate(_barrier);
        _buildingLayer = Instantiate(_buildingLayer);
        _cellLayer = Instantiate(_cellLayer);
    }

    public void AddBuilding(Vector2 point, SizeBuildEnum size)
    {
        GameFieldModel.Instance.SetTownCell(point, size);
        switch (size)
        {
            case SizeBuildEnum.SmallBuilding:
                {
                    GameObject obj = Instantiate(_smallBuild, _buildingLayer);
                    obj.GetComponent<Building>().SetPoint(point);
                    obj.GetComponent<Building>().SetSizeBuild(size);
                    obj.transform.position = new Vector3(obj.transform.position.x + point.x, obj.transform.position.y,
                        obj.transform.position.z + point.y);
                    GameFieldModel.Instance.BuildingList.Add(obj);
                    break;
                }
            case SizeBuildEnum.MediumBuilding:
                {
                    GameObject obj = Instantiate(_mediumBuild, _buildingLayer);
                    obj.GetComponent<Building>().SetPoint(point);
                    obj.GetComponent<Building>().SetSizeBuild(size);
                    obj.transform.position = new Vector3(obj.transform.position.x + point.x, obj.transform.position.y,
                        obj.transform.position.z + point.y);
                    GameFieldModel.Instance.BuildingList.Add(obj);
                    break;
                }
            case SizeBuildEnum.LargeBuilding:
                {
                    GameObject obj = Instantiate(_largeBuild, _buildingLayer);
                    obj.GetComponent<Building>().SetPoint(point);
                    obj.GetComponent<Building>().SetSizeBuild(size);
                    obj.transform.position = new Vector3(obj.transform.position.x + point.x, obj.transform.position.y,
                        obj.transform.position.z + point.y);
                    GameFieldModel.Instance.BuildingList.Add(obj);
                    break;
                }
        }
    }

    public void RemoveBuilding(GameObject building)
    {
        GameFieldModel.Instance.BuildingList.Remove(building);

        GameFieldModel.Instance.ClearTownCell(new Vector2(building.GetComponent<IBuilding>().GetPoint().x,
                                                building.GetComponent<IBuilding>().GetPoint().y), building.GetComponent<IBuilding>().GetSizeBuild());
        Destroy(building);        
    }

    public void DrawGameField(bool ReLoadFlag)
    { 
        for (int i = 0; i < GameFieldModel.Instance.TypeGameFieldMass.Length; i++)
        {
            GameObject obj = Instantiate(_cell, _cellLayer);
            obj.GetComponent<Cell>().InitCell(Game.Instance.CellSpriteMass[GameFieldModel.Instance.TypeGameFieldMass[i]],(TypeCellEnum) GameFieldModel.Instance.TypeGameFieldMass[i]);
            obj.transform.position = new Vector3(i / GameFieldModel.Instance.SizeGameField, 0, i % GameFieldModel.Instance.SizeGameField);
            GameFieldModel.Instance.CellsMass[i] = obj.GetComponent<Cell>();

            if (ReLoadFlag)
            {
                if(GameFieldModel.Instance.TypeGameFieldMass[i] == (int)TypeCellEnum.Grass ||
                     GameFieldModel.Instance.TypeGameFieldMass[i] == (int)TypeCellEnum.Sand
                    )
                {
                    GameFieldModel.Instance.BuildbleGameFieldMass[i] = true;
                }
                else
                {
                    GameFieldModel.Instance.BuildbleGameFieldMass[i] = false;
                }
            }      
        }

        _barrier.transform.position = new Vector3(GameFieldModel.Instance.SizeGameField / 2, GameFieldModel.Instance.SizeGameField / 4, GameFieldModel.Instance.SizeGameField / 2);
        _barrier.transform.localScale = new Vector3(GameFieldModel.Instance.SizeGameField, 15, GameFieldModel.Instance.SizeGameField);
    }

    private void OnDestroy()
    {
        _instance = null;
        _barrier = null;
        _buildingLayer = null;
        _cellLayer = null;
    }
}

