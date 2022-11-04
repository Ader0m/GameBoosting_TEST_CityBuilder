using UnityEngine;

public class DesertBuilder : BasicBuilder, IBuilder
{
    public DesertBuilder() : base()
    {
        
    }

    public DesertBuilder(int townProcent = 10, int waterProcent = 5, int swampProcent = 5) : base(townProcent, waterProcent, swampProcent)
    {

    }

    /// <summary>
    /// Заполняет все пустые клетки песком
    /// </summary>
    public override void BuildSendCell()
    {
        _targetCountSpawnObject = _size * _size;
        _currentCountSpawnObject = 0;


        for (int i = 0; i < _size * _size; i++)
        {
            if (GameFieldLogick.Instance.TypeGameFieldMass[i] == (int)TypeCellEnum.Empty)
            {
                GameFieldLogick.Instance.SetCell(new Vector2(i / _size, i % _size), TypeCellEnum.Sand); 
            }
        }

    }
    /// <summary>
    /// Заполняет клетки вокруг озер травой. Не поулчится использовать SpawnCell так как там проверка на пустую клетку, а здесь везде клетка пустыни
    /// </summary>
    public override void BuildGrassCell()
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
                        SpawnGrasssNearbyWater(new Vector2((i + _size) / _size, (i + _size) % _size), TypeCellEnum.Grass, 1);               
                }

                if (i - _size > 0)
                {
                    if (GameFieldLogick.Instance.TypeGameFieldMass[i - _size] == (int)TypeCellEnum.Sand)
                        SpawnGrasssNearbyWater(new Vector2((i - _size) / _size, (i - _size) % _size), TypeCellEnum.Grass, 1);             
                }

                if (i + 1 < _size * _size)
                {
                    if (GameFieldLogick.Instance.TypeGameFieldMass[i + 1] == (int)TypeCellEnum.Sand)
                        SpawnGrasssNearbyWater(new Vector2((i + 1) / _size, (i + 1) % _size), TypeCellEnum.Grass, 1);                 
                }

                if (i - 1 > 0)
                {
                    if (GameFieldLogick.Instance.TypeGameFieldMass[i - 1] == (int)TypeCellEnum.Sand)
                        SpawnGrasssNearbyWater(new Vector2((i - 1) / _size, (i - 1) % _size), TypeCellEnum.Grass, 1);             
                }
            }
        }

    }

    private void SpawnGrasssNearbyWater(Vector2 point, TypeCellEnum typeCell, int chanse)
    {
        GameFieldLogick.Instance.SetCell(point, typeCell);
        SpawnCellRecursion(point, typeCell, chanse);
    }
}

