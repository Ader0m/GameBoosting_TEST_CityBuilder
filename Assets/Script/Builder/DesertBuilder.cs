using UnityEngine;

public class DesertBuilder : IBuilder
{
    private int _townProcent = 10;
    private int _waterProcent = 5;
    private int _swampProcent = 5;
    private int _size;
    private int _currentCountSpawnObject;
    private int _targetCountSpawnObject;


    public DesertBuilder()
    {
        _size = GameFieldLogick.Instance.SizeGameField;
    }

    public void BuildWaterCell()
    {
        _targetCountSpawnObject = _size * _size * _waterProcent / 100;
        _currentCountSpawnObject = 0;

        while (_currentCountSpawnObject < _targetCountSpawnObject)
        {
            Vector2 point = new Vector2(Random.Range(0, _size), Random.Range(0, _size));

            if (GameFieldLogick.Instance.TypeGameFieldMass[(int)(point.x * _size + point.y)] == (int)TypeCellEnum.Empty)
            {
                GameFieldLogick.Instance.SetWaterCell(point);               
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
                        if (GameFieldLogick.Instance.CheckClearSpaceForBuilding(point, SizeBuildEnum.SmallBuilding))
                        {
                            GameFieldDraw.Instance.AddBuilding(point, SizeBuildEnum.SmallBuilding);
                            _currentCountSpawnObject++;
                        }
                        break;
                    }
                case 2:
                case 3:
                    {
                        if (GameFieldLogick.Instance.CheckClearSpaceForBuilding(point, SizeBuildEnum.MediumBuilding))
                        {
                            GameFieldDraw.Instance.AddBuilding(point, SizeBuildEnum.MediumBuilding);
                            _currentCountSpawnObject += 4;
                        }
                        break;
                    }
                case 4:
                    {
                        if (GameFieldLogick.Instance.CheckClearSpaceForBuilding(point, SizeBuildEnum.LargeBuilding))
                        {
                            GameFieldDraw.Instance.AddBuilding(point, SizeBuildEnum.LargeBuilding);
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
            if (GameFieldLogick.Instance.TypeGameFieldMass[i] == (int)TypeCellEnum.Empty)
            {
                GameFieldLogick.Instance.SetSandCell(new Vector2(i / 100, i % 100)); 
            }
        }

    }

    public void BuildGrassCell()
    {
        _targetCountSpawnObject = _size * _size;
        _currentCountSpawnObject = 0;


        for (int i = 0; i < _size * _size; i++)
        {
            if (GameFieldLogick.Instance.TypeGameFieldMass[i] == (int)TypeCellEnum.Water)
            {
                if (i + _size < _size * _size)
                {
                    if (GameFieldLogick.Instance.TypeGameFieldMass[i + _size] == (int)TypeCellEnum.Sand)
                    {
                        GameFieldLogick.Instance.SetGrassCell(new Vector2(i / 100, i % 100));
                        SpawnCellRecursion(new Vector2((i + _size) / _size, (i + _size) % _size), TypeCellEnum.Grass, 1);
                    }
                }

                if (i - _size > 0)
                {
                    if (GameFieldLogick.Instance.TypeGameFieldMass[i - _size] == (int)TypeCellEnum.Sand)
                    {
                        GameFieldLogick.Instance.TypeGameFieldMass[i - _size] = (int)TypeCellEnum.Grass;
                        GameFieldLogick.Instance.SetGrassCell(new Vector2(i / 100, i % 100));
                        SpawnCellRecursion(new Vector2((i - _size) / _size, (i - _size) % _size), TypeCellEnum.Grass, 1);
                    }
                }

                if (i + 1 < _size * _size)
                {
                    if (GameFieldLogick.Instance.TypeGameFieldMass[i + 1] == (int)TypeCellEnum.Sand)
                    {
                        GameFieldLogick.Instance.TypeGameFieldMass[i + 1] = (int)TypeCellEnum.Grass;
                        GameFieldLogick.Instance.SetGrassCell(new Vector2(i / 100, i % 100));
                        SpawnCellRecursion(new Vector2((i + 1) / _size, (i + 1) % _size), TypeCellEnum.Grass, 1);
                    }
                }

                if (i - 1 > 0)
                {
                    if (GameFieldLogick.Instance.TypeGameFieldMass[i - 1] == (int)TypeCellEnum.Sand)
                    {
                        GameFieldLogick.Instance.TypeGameFieldMass[i - 1] = (int)TypeCellEnum.Grass;
                        GameFieldLogick.Instance.SetGrassCell(new Vector2(i / 100, i % 100));
                        SpawnCellRecursion(new Vector2((i - _size) / _size, (i - _size) % _size), TypeCellEnum.Grass, 1);
                    }
                }
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
            if (GameFieldLogick.Instance.TypeGameFieldMass[(int)(point.x * _size + point.y)] == (int)TypeCellEnum.Empty)
            {
                GameFieldLogick.Instance.SetSwampCell(point);
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
                if (GameFieldLogick.Instance.TypeGameFieldMass[(int)(point.x * _size + point.y)] == (int)TypeCellEnum.Empty)
                {
                    GameFieldLogick.Instance.TypeGameFieldMass[(int)(point.x * _size + point.y)] = (int)typeCellEnum;

                    if (typeCellEnum == TypeCellEnum.Sand || typeCellEnum == TypeCellEnum.Grass)
                    {
                        GameFieldLogick.Instance.BuildbleGameFieldMass[(int)(point.x * _size + point.y)] = true;
                    }
                    else
                    {
                        GameFieldLogick.Instance.BuildbleGameFieldMass[(int)(point.x * _size + point.y)] = false;
                    }

                    _currentCountSpawnObject++;
                    SpawnCellRecursion(new Vector2(point.x, point.y), typeCellEnum, chanse);
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

