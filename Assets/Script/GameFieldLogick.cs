using System;
using System.Collections.Generic;
using UnityEngine;

internal class GameFieldLogick
{
    #region Singleton

    public static GameFieldLogick Instance => _instance;
    private static GameFieldLogick _instance;

    #endregion

    public int[] TypeGameFieldMass;
    public bool[] BuildbleGameFieldMass;
    public Cell[] CellsMass;
    public List<GameObject> BuildingList;
    public readonly int SizeGameField;


    public GameFieldLogick(int size)
    {
        _instance = this;
        SizeGameField = size;
        TypeGameFieldMass = new int[SizeGameField * SizeGameField];
        BuildbleGameFieldMass = new bool[SizeGameField * SizeGameField];
        CellsMass = new Cell[SizeGameField * SizeGameField];
        BuildingList = new List<GameObject>();


        for (int i = 0; i < SizeGameField * SizeGameField; i++)
        {
            TypeGameFieldMass[i] = 0;

            // Для корректной работы нужно заполнить пустыми клетками
            CellsMass[i] = new Cell();
        }
    }

    /// <summary>
    /// Редирект для блокирования клеток под зданием
    /// </summary>
    /// <param name="point"> Точка применения </param>
    /// <param name="size"> Тип здания </param>
    public void SetTownCell(Vector2 point, SizeBuildEnum size)
    {
        SetCellUnderBuilding(point, size, TypeCellEnum.Town);
    }

    /// <summary>
    /// Редирект для очистки клеток под зданием
    /// </summary>
    /// <param name="point"> Точка применения </param>
    /// <param name="size"> Тип здания </param>
    public void ClearTownCell(Vector2 point, SizeBuildEnum size)
    {
        SetCellUnderBuilding(point, size, TypeCellEnum.Sand);
    }

    /// <summary>
    /// Проверяет можно ли в "эту" точку установить "это" здание
    /// </summary>
    /// <param name="point"> Точка применения </param>
    /// <param name="size"> Тип здания </param>
    /// <returns></returns>
    public bool CheckClearSpaceForBuilding(Vector2 point, SizeBuildEnum size)
    {
        switch (size)
        {
            case SizeBuildEnum.SmallBuilding:
                {
                    if (BuildbleGameFieldMass[(int)(point.x * SizeGameField + point.y)])
                    {
                        return true;
                    }

                    return false;
                }
            case SizeBuildEnum.MediumBuilding:
                {
                    if (point.x + 1 < SizeGameField && point.y + 1 < SizeGameField)
                        if (BuildbleGameFieldMass[(int)(point.x * SizeGameField + point.y)] &&
                            BuildbleGameFieldMass[(int)((point.x + 1) * SizeGameField + point.y)] &&
                            BuildbleGameFieldMass[(int)((point.x + 1) * SizeGameField + (point.y + 1))] &&
                            BuildbleGameFieldMass[(int)(point.x * SizeGameField + (point.y + 1))]
                        )
                        {
                            return true;
                        }

                    return false;
                }
            case SizeBuildEnum.LargeBuilding:
                {
                    if (point.x + 1 < SizeGameField && point.x - 1 > 0 && point.y + 1 < SizeGameField && point.y - 1 > 0)
                        if (BuildbleGameFieldMass[(int)(point.x * SizeGameField + point.y)] &&     //центр     
                            BuildbleGameFieldMass[(int)((point.x + 1) * SizeGameField + point.y)] &&      //север            
                            BuildbleGameFieldMass[(int)((point.x + 1) * SizeGameField + (point.y + 1))] && //северо-восток
                            BuildbleGameFieldMass[(int)(point.x * SizeGameField + (point.y + 1))] && // восток
                            BuildbleGameFieldMass[(int)((point.x - 1) * SizeGameField + (point.y + 1))] && // юго-восток
                            BuildbleGameFieldMass[(int)((point.x - 1) * SizeGameField + point.y)] && // юг
                            BuildbleGameFieldMass[(int)((point.x - 1) * SizeGameField + (point.y - 1))] && // юго-запад
                            BuildbleGameFieldMass[(int)(point.x * SizeGameField + (point.y - 1))] && // запад
                            BuildbleGameFieldMass[(int)((point.x + 1) * SizeGameField + (point.y - 1))] // Северо-запад
                            )
                        {
                            return true;
                        }

                    return false;
                }

        }

        return false;
    }

    /// <summary>
    /// Выполняет все необходимые действия для регистрации в игре блока.
    /// </summary>
    /// <param name="point"> Точка применения </param>
    public void SetCell(Vector2 point, TypeCellEnum typeCell)
    {
        TypeGameFieldMass[(int)(point.x * SizeGameField + point.y)] = (int)typeCell;

        if (typeCell == TypeCellEnum.Grass || typeCell == TypeCellEnum.Sand)
            BuildbleGameFieldMass[(int)(point.x * SizeGameField + point.y)] = true;
        else
        {
            BuildbleGameFieldMass[(int)(point.x * SizeGameField + point.y)] = false;
        }

        CellsMass[(int)(point.x * SizeGameField + point.y)].Refresh();
    }

    /// <summary>
    /// Регистрирует изменения блоков связанные со зданиями
    /// Вызывает функцию для регистрации всех блоков нужного типа. Не создает здание
    /// </summary>
    /// <param name="point"> Точка установки </param>
    /// <param name="size"> Тип здания </param>
    private void SetCellUnderBuilding(Vector2 point, SizeBuildEnum size, TypeCellEnum typeCell)
    {
        switch (size)
        {
            case SizeBuildEnum.SmallBuilding:
                {
                    SetCell(point, typeCell);

                    break;
                }
            case SizeBuildEnum.MediumBuilding:
                {
                    SetCell(point, typeCell);
                    SetCell(new Vector2(point.x + 1, point.y), typeCell);
                    SetCell(new Vector2(point.x + 1, point.y + 1), typeCell);
                    SetCell(new Vector2(point.x, point.y + 1), typeCell);

                    break;
                }
            case SizeBuildEnum.LargeBuilding:
                {
                    SetCell(point, typeCell); //центр 
                    SetCell(new Vector2(point.x + 1, point.y), typeCell); //север 
                    SetCell(new Vector2(point.x + 1, point.y + 1), typeCell); //северо-восток 
                    SetCell(new Vector2(point.x - 1, point.y + 1), typeCell); // восток    
                    SetCell(new Vector2(point.x, point.y + 1), typeCell); // юго-восток 
                    SetCell(new Vector2(point.x - 1, point.y), typeCell); // юг    
                    SetCell(new Vector2(point.x - 1, point.y - 1), typeCell);   // юго-запад
                    SetCell(new Vector2(point.x, point.y - 1), typeCell); // запад
                    SetCell(new Vector2(point.x + 1, point.y - 1), typeCell); // Северо-запад

                    break;
                }
        }
    }

    [Serializable]
    public class GameFieldSerial
    {
        public int[]                GameFieldMassSerial;
        public List<BuildingSerial> BuildingListSerial;
        public readonly int         SizeGameFieldSerial;

        public GameFieldSerial()
        {
            BuildingListSerial = new List<BuildingSerial>();

            GameFieldMassSerial = GameFieldLogick.Instance.TypeGameFieldMass;
            foreach (GameObject building in GameFieldLogick.Instance.BuildingList)
            {
                IBuilding buildingSerial = building.GetComponent<IBuilding>();
                BuildingListSerial.Add(buildingSerial.GetSerial());
            }
            SizeGameFieldSerial = GameFieldLogick.Instance.SizeGameField;
        }
    }
}

