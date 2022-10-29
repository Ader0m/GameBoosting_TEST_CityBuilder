using UnityEngine;

public class FlatBuilder : BasicBuilder, IBuilder
{
    public FlatBuilder() : base()
    {
        
    }

    public FlatBuilder(int townProcent = 10, int waterProcent = 5, int swampProcent = 5) : base(townProcent, waterProcent, swampProcent)
    {
        
    }

    /// <summary>
    /// «аполн€ет все пустые клетки вокруг воды песком
    /// </summary>
    public override void BuildSendCell()
    {
        _targetCountSpawnObject = _size * _size;
        _currentCountSpawnObject = 0;


        for (int i = 0; i < _size * _size; i++)
        {
            if (GameFieldLogick.Instance.TypeGameFieldMass[i] == (int)TypeCellEnum.Water)
            {
                if (i + _size < _size * _size)
                {
                    SpawnCell(new Vector2((i + _size) / _size, (i + _size) % _size), TypeCellEnum.Sand, 1);                  
                }

                if (i - _size > 0)
                {
                    SpawnCell(new Vector2((i - _size) / _size, (i - _size) % _size), TypeCellEnum.Sand, 1);                  
                }

                if (i + 1 < _size * _size)
                {
                    SpawnCell(new Vector2((i + 1) / _size, (i + 1) % _size), TypeCellEnum.Sand, 1);                   
                }

                if (i - 1 > 0)
                {
                    SpawnCell(new Vector2((i - 1) / _size, (i - 1) % _size), TypeCellEnum.Sand, 1);                   
                }
            }
        }
    }

    /// <summary>
    /// «аполн€ет все пустые клетки травой
    /// </summary>
    public override void BuildGrassCell()
    {
        _targetCountSpawnObject = _size * _size;
        _currentCountSpawnObject = 0;


        for (int i = 0; i < _size * _size; i++)
        {
            if (GameFieldLogick.Instance.TypeGameFieldMass[i] == (int)TypeCellEnum.Empty)
            {
                GameFieldLogick.Instance.SetCell(new Vector2(i / _size, i % _size), TypeCellEnum.Grass);
            }
        }
    }
}
