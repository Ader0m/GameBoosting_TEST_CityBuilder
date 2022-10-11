using System;
using UnityEngine;

/// <summary>
/// Принимает разные виды строений по интерфейсу IBuilding. Извлечение типа строения происходит по SizeBuildEnum
/// </summary>
[Serializable]
internal class BuildingSerial : IBuilding
{
    [Serializable]
    public struct Vector2Serial
    {
        public float x;
        public float y;

        public Vector2Serial(Vector2 vector2)
        {
            x = vector2.x;
            y = vector2.y;
        }
    }

    public Vector2Serial Point;
    public int SizeBuild;


    public BuildingSerial(IBuilding building)
    {
        Point = new Vector2Serial(building.GetPoint());
        SizeBuild = (int) building.GetSizeBuild();
    }

    public GameObject GameObject()
    {
        throw new NotImplementedException();
    }

    public Vector2 GetPoint()
    {
        return new Vector2(Point.x, Point.y);
    }

    public IBuilding GetSerial()
    {
        throw new NotImplementedException();
    }

    public SizeBuildEnum GetSizeBuild()
    {
        return (SizeBuildEnum) SizeBuild;
    }

    public void SetPoint(Vector2 value)
    {
        throw new NotImplementedException();
    }

    public void SetSizeBuild(SizeBuildEnum value)
    {
        throw new NotImplementedException();
    }

    BuildingSerial IBuilding.GetSerial()
    {
        throw new NotImplementedException();
    }
}

