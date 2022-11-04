using UnityEngine;

public abstract class BasicBuilder : IBuilder
{
    protected int _townProcent = 10;
    protected int _waterProcent = 5;
    protected int _swampProcent = 5;
    protected int _size;
    protected int _currentCountSpawnObject;
    protected int _targetCountSpawnObject;

    
    public BasicBuilder()
    {
        _size = GameFieldLogick.Instance.SizeGameField;
    }

    public BasicBuilder(int townProcent = 10, int waterProcent = 5, int swampProcent = 5)
    {
        if (townProcent + waterProcent + swampProcent <= 90)
        {
            _townProcent = townProcent;
            _waterProcent = waterProcent;
            _swampProcent = swampProcent;
        }
        else
        {
            Debug.Log("Bad proporcies. Using standart");
        }
        _size = GameFieldLogick.Instance.SizeGameField;
    }

    public virtual void BuildGrassCell()
    {
        ;
    }

    public virtual void BuildSendCell()
    {
        ;
    }

    public virtual void BuildSwampCell()
    {
        _targetCountSpawnObject = _size * _size * _swampProcent / 100;
        _currentCountSpawnObject = 0;

        while (_currentCountSpawnObject < _targetCountSpawnObject)
        {
            Vector2 point = new Vector2(Random.Range(0, _size), Random.Range(0, _size));

            if (GameFieldLogick.Instance.TypeGameFieldMass[(int)(point.x * _size + point.y)] == (int)TypeCellEnum.Empty)
            {
                SpawnCell(point, TypeCellEnum.Swamp, 3);
            }
        }
    }

    public virtual void BuildTownCell()
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
                        RegisterBuilding(point, SizeBuildEnum.SmallBuilding);
                        break;
                    }
                case 2:
                case 3:
                    {
                        RegisterBuilding(point, SizeBuildEnum.MediumBuilding);
                        break;
                    }
                case 4:
                    {
                        RegisterBuilding(point, SizeBuildEnum.LargeBuilding);
                        break;
                    }
            }
        }
    }

    protected void RegisterBuilding(Vector2 point, SizeBuildEnum buildEnum)
    {
        if (GameFieldLogick.Instance.CheckClearSpaceForBuilding(point, buildEnum))
        {
            GameFieldDraw.Instance.AddBuilding(point, buildEnum);
            _currentCountSpawnObject += (int)buildEnum * (int)buildEnum;
        }
    } 

    public virtual void BuildWaterCell()
    {
        _targetCountSpawnObject = _size * _size * _waterProcent / 100;
        _currentCountSpawnObject = 0;

        while (_currentCountSpawnObject < _targetCountSpawnObject)
        {
            Vector2 point = new Vector2(Random.Range(0, _size), Random.Range(0, _size));

            if (GameFieldLogick.Instance.TypeGameFieldMass[(int)(point.x * _size + point.y)] == (int)TypeCellEnum.Empty)
            {
                SpawnCell(point, TypeCellEnum.Water, 4);
            }
        }
    }

    /// <summary>
    /// Рандомит спавн подобного себе блока вокруг, и запускает в нем новый шаг рекурсии
    /// </summary>
    /// <param name="point"> Координата точки </param> 
    /// <param name="typeCell"> Тип блока </param> 
    /// <param name="chanse"> Шанс в процентах * 10</param> 
    protected void SpawnCellRecursion(Vector2 point, TypeCellEnum typeCell, int chanse)
    {
        if (point.x + 1 < _size)
        {
            point.x += 1;
            TrySpawnCell( point, typeCell, chanse);
            point.x -= 1;
        }

        if (point.x - 1 > 0)
        {
            point.x -= 1;
            TrySpawnCell(point, typeCell,  chanse);
            point.x += 1;
        }

        if (point.y + 1 < _size - 1)
        {
            point.y += 1;
            TrySpawnCell(point, typeCell,  chanse);
            point.y -= 1;
        }

        if (point.y - 1 > 0)
        {
            point.y -= 1;
            TrySpawnCell(point, typeCell, chanse);
            point.y += 1;
        }
    }

    protected virtual void SpawnCell(Vector2 point, TypeCellEnum typeCell, int chanse)
    {
        if (GameFieldLogick.Instance.TypeGameFieldMass[(int)(point.x * _size + point.y)] == (int)TypeCellEnum.Empty)
        {
            GameFieldLogick.Instance.SetCell(point, typeCell);

            if (typeCell == TypeCellEnum.Sand || typeCell == TypeCellEnum.Grass)
            {
                GameFieldLogick.Instance.BuildbleGameFieldMass[(int)(point.x * _size + point.y)] = true;
            }
            else
            {
                GameFieldLogick.Instance.BuildbleGameFieldMass[(int)(point.x * _size + point.y)] = false;
            }

            _currentCountSpawnObject++;
            SpawnCellRecursion(new Vector2(point.x, point.y), typeCell, chanse);
        }      
    }

    protected void TrySpawnCell(Vector2 point, TypeCellEnum typeCell, int chanse)
    {
        if (Random.Range(0, 9) < chanse && _currentCountSpawnObject < _targetCountSpawnObject)
        {
            SpawnCell(point, typeCell, chanse);
        }
    }
}

