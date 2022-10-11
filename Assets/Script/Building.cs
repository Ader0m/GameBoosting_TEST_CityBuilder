using UnityEngine;


internal class Building : MonoBehaviour, IBuilding
{
    private Vector2 _point;
    private SizeBuildEnum _sizeBuild;
    
    #region Get/Set

    public Vector2 GetPoint() {
        return _point; 
    }

    public SizeBuildEnum GetSizeBuild()
    {
        return _sizeBuild;
    }

    public void SetPoint(Vector2 value)
    {
        _point = value;
    }

    public void SetSizeBuild(SizeBuildEnum value)
    {
        _sizeBuild = value;
    }

    public GameObject GameObject()
    {
       return gameObject;
    }

    public BuildingSerial GetSerial()
    {
        return new BuildingSerial(this);
    }

    #endregion
   
}