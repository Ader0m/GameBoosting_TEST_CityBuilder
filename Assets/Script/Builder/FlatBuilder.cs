using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatBuilder : IBuilder
{
    private int _townProcent = 10;
    private int _waterProcent = 5;
    private int _swampProcent = 5;
    private int _size;
    private int _currentCountSpawnObject;
    private int _targetCountSpawnObject;


    public FlatBuilder()
    {
        _size = GameFieldModel.Instance.SizeGameField;

    }

    public void BuildWaterCell()
    {
        _targetCountSpawnObject = _size * _size * _waterProcent / 100;
        _currentCountSpawnObject = 0;

        while (_currentCountSpawnObject < _targetCountSpawnObject)
        {
            Vector2 point = new Vector2(Random.Range(0, _size), Random.Range(0, _size));

            if (GameFieldModel.Instance.TypeGameFieldMass[(int)(point.x * _size + point.y)] == (int)TypeCellEnum.Empty)
            {
                GameFieldModel.Instance.SetWaterCell(point);
                _currentCountSpawnObject++;
                SpawnCellRecursion(point, TypeCellEnum.Water, 4);
            }
        }
    }

    public void BuildTownCell()
    {
        _targetCountSpawnObject = _size * _size * _townProcent / 100;
        _currentCountSpawnObject = 0;

        while (_currentCountSpawnObject < _targetCountSpawnObject)
        {
            Vector2 point = new Vector2(Random.Range(0, _size), Random.Range(0, _size));
            int type = Random.Range(0, 5);
            switch (type)
            {
                case 0:
                case 1:
                    {
                        if (GameFieldModel.Instance.CheckClearSpaceForBuilding(point, SizeBuildEnum.SmallBuilding))
                        {
                            GameFieldMV.Instance.AddBuilding(point, SizeBuildEnum.SmallBuilding);
                            _currentCountSpawnObject++;
                        }
                        break;
                    }
                case 2:
                case 3:
                    {
                        if (GameFieldModel.Instance.CheckClearSpaceForBuilding(point, SizeBuildEnum.MediumBuilding))
                        {
                            GameFieldMV.Instance.AddBuilding(point, SizeBuildEnum.MediumBuilding);
                            _currentCountSpawnObject += 4;
                        }
                        break;
                    }
                case 4:
                    {
                        if (GameFieldModel.Instance.CheckClearSpaceForBuilding(point, SizeBuildEnum.LargeBuilding))
                        {
                            GameFieldMV.Instance.AddBuilding(point, SizeBuildEnum.LargeBuilding);
                            _currentCountSpawnObject += 9;
                            break;
                        }

                        break;
                    }
            }
        }
    }

    public void BuildSendCell()
    {
        _targetCountSpawnObject = _size * _size;
        _currentCountSpawnObject = 0;


        for (int i = 0; i < _size * _size; i++)
        {
            if (GameFieldModel.Instance.TypeGameFieldMass[i] == (int)TypeCellEnum.Water)
            {
                if (i + _size < _size * _size)
                {
                    if (GameFieldModel.Instance.TypeGameFieldMass[i + _size] == (int)TypeCellEnum.Empty)
                    {
                        GameFieldModel.Instance.SetSandCell(new Vector2(i / 100, i % 100));
                        SpawnCellRecursion(new Vector2((i + _size) / _size, (i + _size) % _size), TypeCellEnum.Sand, 1);
                    }
                }

                if (i - _size > 0)
                {
                    if (GameFieldModel.Instance.TypeGameFieldMass[i - _size] == (int)TypeCellEnum.Empty)
                    {
                        GameFieldModel.Instance.SetSandCell(new Vector2(i / 100, i % 100));
                        SpawnCellRecursion(new Vector2((i - _size) / _size, (i - _size) % _size), TypeCellEnum.Sand, 1);
                    }
                }

                if (i + 1 < _size * _size)
                {
                    if (GameFieldModel.Instance.TypeGameFieldMass[i + 1] == (int)TypeCellEnum.Empty)
                    {
                        GameFieldModel.Instance.SetSandCell(new Vector2(i / 100, i % 100));
                        SpawnCellRecursion(new Vector2((i + 1) / _size, (i + 1) % _size), TypeCellEnum.Sand, 1);
                    }
                }

                if (i - 1 > 0)
                {
                    if (GameFieldModel.Instance.TypeGameFieldMass[i - 1] == (int)TypeCellEnum.Empty)
                    {
                        GameFieldModel.Instance.SetSandCell(new Vector2(i / 100, i % 100));
                        SpawnCellRecursion(new Vector2((i - 1) / _size, (i - 1) % _size), TypeCellEnum.Sand, 1);
                    }
                }
            }
        }
    }

    public void BuildGrassCell()
    {
        _targetCountSpawnObject = _size * _size;
        _currentCountSpawnObject = 0;


        for (int i = 0; i < _size * _size; i++)
        {
            if (GameFieldModel.Instance.TypeGameFieldMass[i] == (int)TypeCellEnum.Empty)
            {
                GameFieldModel.Instance.SetGrassCell(new Vector2(i / 100, i % 100));
            }
        }
    }



    public void BuildSwampCell()
    {
        _targetCountSpawnObject = _size * _size * _swampProcent / 100;
        _currentCountSpawnObject = 0;

        while (_currentCountSpawnObject < _targetCountSpawnObject)
        {
            Vector2 point = new Vector2(Random.Range(0, _size), Random.Range(0, _size));
            if (GameFieldModel.Instance.TypeGameFieldMass[(int)(point.x * _size + point.y)] == (int)TypeCellEnum.Empty)
            {
                GameFieldModel.Instance.TypeGameFieldMass[(int)(point.x * _size + point.y)] = (int)TypeCellEnum.Swamp;
                _currentCountSpawnObject++;
                SpawnCellRecursion(point, TypeCellEnum.Swamp, 3);
            }
        }
    }

    /// <summary>
    /// Рандомит спавн подобного себе блока вокруг, и запускает в нем новый шаг рекурсии
    /// </summary>
    /// <param name="point"> Координата точки </param> 
    /// <param name="typeCellEnum"> Тип блока </param> 
    /// <param name="chanse"> Шанс в процентах * 10</param> 
    private void SpawnCellRecursion(Vector2 point, TypeCellEnum typeCellEnum, int chanse)
    {
        #region LocalFunc

        void SpawnCell()
        {
            if (Random.Range(0, 9) < chanse && _currentCountSpawnObject < _targetCountSpawnObject)
            {
                if (GameFieldModel.Instance.TypeGameFieldMass[(int)(point.x * _size + point.y)] == (int)TypeCellEnum.Empty)
                {
                    GameFieldModel.Instance.TypeGameFieldMass[(int)(point.x * _size + point.y)] = (int)typeCellEnum;

                    if (typeCellEnum == TypeCellEnum.Sand || typeCellEnum == TypeCellEnum.Grass)
                    {
                        GameFieldModel.Instance.BuildbleGameFieldMass[(int)(point.x * _size + point.y)] = true;
                    }
                    else
                    {
                        GameFieldModel.Instance.BuildbleGameFieldMass[(int)(point.x * _size + point.y)] = false;
                    }

                    _currentCountSpawnObject++;
                    SpawnCellRecursion(point, typeCellEnum, chanse);
                }
            }
        }

        #endregion

        if (point.x + 1 < _size)
        {
            point.x += 1;
            SpawnCell();
            point.x -= 1;
        }

        if (point.x - 1 > 0)
        {
            point.x -= 1;
            SpawnCell();
            point.x += 1;
        }

        if (point.y + 1 < _size - 1)
        {
            point.y += 1;
            SpawnCell();
            point.y -= 1;
        }

        if (point.y - 1 > 0)
        {
            point.y -= 1;
            SpawnCell();
            point.y += 1;
        }
    }
}
