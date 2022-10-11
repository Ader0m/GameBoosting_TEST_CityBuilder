using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class GameFieldModel
{
    #region Singleton

    public static GameFieldModel Instance => _instance;
    private static GameFieldModel _instance;

    #endregion

    public int[] TypeGameFieldMass;
    public bool[] BuildbleGameFieldMass;
    public Cell[] CellsMass;
    public List<GameObject> BuildingList;
    public readonly int SizeGameField;


    public GameFieldModel(int size)
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

    public void SetTownCell(Vector2 point, SizeBuildEnum size)
    {
        switch (size)
        {
            case SizeBuildEnum.SmallBuilding:
                {
                    TypeGameFieldMass[(int)(point.x * SizeGameField + point.y)] = (int)TypeCellEnum.Town;

                    BuildbleGameFieldMass[(int)(point.x * SizeGameField + point.y)] = false;

                    CellsMass[(int)(point.x * SizeGameField + point.y)].Refresh();
                    break;
                }
            case SizeBuildEnum.MediumBuilding:
                {
                    TypeGameFieldMass[(int)(point.x * SizeGameField + point.y)]             = (int)TypeCellEnum.Town;                    
                    TypeGameFieldMass[(int)((point.x + 1) * SizeGameField + point.y)]       = (int)TypeCellEnum.Town;                    
                    TypeGameFieldMass[(int)((point.x + 1) * SizeGameField + (point.y + 1))] = (int)TypeCellEnum.Town;                    
                    TypeGameFieldMass[(int)(point.x * SizeGameField + (point.y + 1))]       = (int)TypeCellEnum.Town;

                    BuildbleGameFieldMass[(int)(point.x * SizeGameField + point.y)]             = false;
                    BuildbleGameFieldMass[(int)((point.x + 1) * SizeGameField + point.y)]       = false;
                    BuildbleGameFieldMass[(int)((point.x + 1) * SizeGameField + (point.y + 1))] = false;
                    BuildbleGameFieldMass[(int)(point.x * SizeGameField + (point.y + 1))]       = false;

                    CellsMass[(int)(point.x * SizeGameField + point.y)].Refresh();
                    CellsMass[(int)((point.x + 1) * SizeGameField + point.y)].Refresh();      
                    CellsMass[(int)((point.x + 1) * SizeGameField + (point.y + 1))].Refresh();
                    CellsMass[(int)(point.x * SizeGameField + (point.y + 1))].Refresh();     
                    break;
                }
            case SizeBuildEnum.LargeBuilding:
                {
                    TypeGameFieldMass[(int)(point.x * SizeGameField + point.y)]             = (int)TypeCellEnum.Town; //центр                   
                    TypeGameFieldMass[(int)((point.x + 1) * SizeGameField + point.y)]       = (int)TypeCellEnum.Town; //север                    
                    TypeGameFieldMass[(int)((point.x + 1) * SizeGameField + (point.y + 1))] = (int)TypeCellEnum.Town; //северо-восток                    
                    TypeGameFieldMass[(int)(point.x * SizeGameField + (point.y + 1))]       = (int)TypeCellEnum.Town; // восток                    
                    TypeGameFieldMass[(int)((point.x - 1) * SizeGameField + (point.y + 1))] = (int)TypeCellEnum.Town; // юго-восток                    
                    TypeGameFieldMass[(int)((point.x - 1) * SizeGameField + point.y)]       = (int)TypeCellEnum.Town; // юг
                    TypeGameFieldMass[(int)((point.x - 1) * SizeGameField + (point.y - 1))] = (int)TypeCellEnum.Town; // юго-запад
                    TypeGameFieldMass[(int)(point.x * SizeGameField + (point.y - 1))]       = (int)TypeCellEnum.Town; // запад
                    TypeGameFieldMass[(int)((point.x + 1) * SizeGameField + (point.y - 1))] = (int)TypeCellEnum.Town; // Северо-запад

                    BuildbleGameFieldMass[(int)(point.x * SizeGameField + point.y)] = false;
                    BuildbleGameFieldMass[(int)((point.x + 1) * SizeGameField + point.y)]       = false;
                    BuildbleGameFieldMass[(int)((point.x + 1) * SizeGameField + (point.y + 1))] = false;
                    BuildbleGameFieldMass[(int)(point.x * SizeGameField + (point.y + 1))]       = false;
                    BuildbleGameFieldMass[(int)((point.x - 1) * SizeGameField + (point.y + 1))] = false;
                    BuildbleGameFieldMass[(int)((point.x - 1) * SizeGameField + point.y)]       = false;
                    BuildbleGameFieldMass[(int)((point.x - 1) * SizeGameField + (point.y - 1))] = false;
                    BuildbleGameFieldMass[(int)(point.x * SizeGameField + (point.y - 1))]       = false;
                    BuildbleGameFieldMass[(int)((point.x + 1) * SizeGameField + (point.y - 1))] = false;

                    CellsMass[(int)(point.x * SizeGameField + point.y)]            .Refresh();
                    CellsMass[(int)((point.x + 1) * SizeGameField + point.y)]      .Refresh();
                    CellsMass[(int)((point.x + 1) * SizeGameField + (point.y + 1))].Refresh();
                    CellsMass[(int)(point.x * SizeGameField + (point.y + 1))]      .Refresh();
                    CellsMass[(int)((point.x - 1) * SizeGameField + (point.y + 1))].Refresh();
                    CellsMass[(int)((point.x - 1) * SizeGameField + point.y)]      .Refresh();
                    CellsMass[(int)((point.x - 1) * SizeGameField + (point.y - 1))].Refresh();
                    CellsMass[(int)(point.x * SizeGameField + (point.y - 1))]      .Refresh();
                    CellsMass[(int)((point.x + 1) * SizeGameField + (point.y - 1))].Refresh();

                    break;
                }
        }
    }

    public void ClearTownCell(Vector2 point, SizeBuildEnum size)
    {
        switch (size)
        {
            case SizeBuildEnum.SmallBuilding:
                {
                    TypeGameFieldMass[(int)(point.x * SizeGameField + point.y)] = (int)TypeCellEnum.Sand;
                    
                    BuildbleGameFieldMass[(int)(point.x * SizeGameField + point.y)] = true;

                    CellsMass[(int)(point.x * SizeGameField + point.y)].Refresh();
                    break;
                }
            case SizeBuildEnum.MediumBuilding:
                {
                    TypeGameFieldMass[(int)(point.x * SizeGameField + point.y)] = (int)TypeCellEnum.Sand;
                    TypeGameFieldMass[(int)((point.x + 1) * SizeGameField + point.y)] = (int)TypeCellEnum.Sand;
                    TypeGameFieldMass[(int)((point.x + 1) * SizeGameField + (point.y + 1))] = (int)TypeCellEnum.Sand;
                    TypeGameFieldMass[(int)(point.x * SizeGameField + (point.y + 1))] = (int)TypeCellEnum.Sand;

                    BuildbleGameFieldMass[(int)(point.x * SizeGameField + point.y)] = true;
                    BuildbleGameFieldMass[(int)((point.x + 1) * SizeGameField + point.y)] = true;
                    BuildbleGameFieldMass[(int)((point.x + 1) * SizeGameField + (point.y + 1))] = true;
                    BuildbleGameFieldMass[(int)(point.x * SizeGameField + (point.y + 1))] = true;

                    CellsMass[(int)(point.x * SizeGameField + point.y)].Refresh();
                    CellsMass[(int)((point.x + 1) * SizeGameField + point.y)].Refresh();
                    CellsMass[(int)((point.x + 1) * SizeGameField + (point.y + 1))].Refresh();
                    CellsMass[(int)(point.x * SizeGameField + (point.y + 1))].Refresh();
                    break;
                }
            case SizeBuildEnum.LargeBuilding:
                {
                    TypeGameFieldMass[(int)(point.x * SizeGameField + point.y)] = (int)TypeCellEnum.Sand;     //центр     
                    TypeGameFieldMass[(int)((point.x + 1) * SizeGameField + point.y)] = (int)TypeCellEnum.Sand;      //север            
                    TypeGameFieldMass[(int)((point.x + 1) * SizeGameField + (point.y + 1))] = (int)TypeCellEnum.Sand; //северо-восток
                    TypeGameFieldMass[(int)(point.x * SizeGameField + (point.y + 1))] = (int)TypeCellEnum.Sand; // восток
                    TypeGameFieldMass[(int)((point.x - 1) * SizeGameField + (point.y + 1))] = (int)TypeCellEnum.Sand; // юго-восток
                    TypeGameFieldMass[(int)((point.x - 1) * SizeGameField + point.y)] = (int)TypeCellEnum.Sand; // юг
                    TypeGameFieldMass[(int)((point.x - 1) * SizeGameField + (point.y - 1))] = (int)TypeCellEnum.Sand; // юго-запад
                    TypeGameFieldMass[(int)(point.x * SizeGameField + (point.y - 1))] = (int)TypeCellEnum.Sand; // запад
                    TypeGameFieldMass[(int)((point.x + 1) * SizeGameField + (point.y - 1))] = (int)TypeCellEnum.Sand; // Северо-запад

                    BuildbleGameFieldMass[(int)(point.x * SizeGameField + point.y)] = true;
                    BuildbleGameFieldMass[(int)((point.x + 1) * SizeGameField + point.y)] = true;
                    BuildbleGameFieldMass[(int)((point.x + 1) * SizeGameField + (point.y + 1))] = true;
                    BuildbleGameFieldMass[(int)(point.x * SizeGameField + (point.y + 1))] = true;
                    BuildbleGameFieldMass[(int)((point.x - 1) * SizeGameField + (point.y + 1))] = true;
                    BuildbleGameFieldMass[(int)((point.x - 1) * SizeGameField + point.y)] = true;
                    BuildbleGameFieldMass[(int)((point.x - 1) * SizeGameField + (point.y - 1))] = true;
                    BuildbleGameFieldMass[(int)(point.x * SizeGameField + (point.y - 1))] = true;
                    BuildbleGameFieldMass[(int)((point.x + 1) * SizeGameField + (point.y - 1))] = true;

                    CellsMass[(int)(point.x * SizeGameField + point.y)].Refresh();
                    CellsMass[(int)((point.x + 1) * SizeGameField + point.y)].Refresh();
                    CellsMass[(int)((point.x + 1) * SizeGameField + (point.y + 1))].Refresh();
                    CellsMass[(int)(point.x * SizeGameField + (point.y + 1))].Refresh();
                    CellsMass[(int)((point.x - 1) * SizeGameField + (point.y + 1))].Refresh();
                    CellsMass[(int)((point.x - 1) * SizeGameField + point.y)].Refresh();
                    CellsMass[(int)((point.x - 1) * SizeGameField + (point.y - 1))].Refresh();
                    CellsMass[(int)(point.x * SizeGameField + (point.y - 1))].Refresh();
                    CellsMass[(int)((point.x + 1) * SizeGameField + (point.y - 1))].Refresh();
                    break;
                }
        }
    }

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
                    else
                    {
                        return false;
                    }
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
                            BuildbleGameFieldMass[(int)((point.x - 1) * SizeGameField + (point.y + 1))]  && // юго-восток
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

    public void SetGrassCell(Vector2 point)
    {
        TypeGameFieldMass[(int)(point.x * SizeGameField + point.y)] = (int)TypeCellEnum.Grass;

        BuildbleGameFieldMass[(int)(point.x * SizeGameField + point.y)] = true;

        CellsMass[(int)(point.x * SizeGameField + point.y)].Refresh();
    }

    public void SetSandCell(Vector2 point)
    {
        TypeGameFieldMass[(int)(point.x * SizeGameField + point.y)] = (int)TypeCellEnum.Sand;

        BuildbleGameFieldMass[(int)(point.x * SizeGameField + point.y)] = true;

        CellsMass[(int)(point.x * SizeGameField + point.y)].Refresh();
    }

    public void SetSwampCell(Vector2 point)
    {
        TypeGameFieldMass[(int)(point.x * SizeGameField + point.y)] = (int)TypeCellEnum.Swamp;

        BuildbleGameFieldMass[(int)(point.x * SizeGameField + point.y)] = false;

        CellsMass[(int)(point.x * SizeGameField + point.y)].Refresh();
    }

    public void SetWaterCell(Vector2 point)
    {
        TypeGameFieldMass[(int)(point.x * SizeGameField + point.y)] = (int)TypeCellEnum.Water;

        BuildbleGameFieldMass[(int)(point.x * SizeGameField + point.y)] = false;

        CellsMass[(int)(point.x * SizeGameField + point.y)].Refresh();
    }

    [Serializable]
    public class Serial
    {
        public int[] GameFieldMassSerial;
        public List<BuildingSerial> BuildingListSerial;
        public readonly int SizeGameFieldSerial;

        public Serial()
        {
            BuildingListSerial = new List<BuildingSerial>();

            GameFieldMassSerial = GameFieldModel.Instance.TypeGameFieldMass;
            foreach (GameObject building in GameFieldModel.Instance.BuildingList)
            {
                IBuilding buildingSerial = building.GetComponent<IBuilding>();
                BuildingListSerial.Add(buildingSerial.GetSerial());
            }
            SizeGameFieldSerial = GameFieldModel.Instance.SizeGameField;
        }
    }



}

