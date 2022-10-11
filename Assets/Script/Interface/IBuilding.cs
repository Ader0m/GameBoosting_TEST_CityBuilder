using UnityEngine;


internal interface IBuilding
{
    public Vector2 GetPoint();
    public SizeBuildEnum GetSizeBuild();
    public void SetPoint(Vector2 value);
    public void SetSizeBuild(SizeBuildEnum value);
    public GameObject GameObject();
    public BuildingSerial GetSerial();
}

