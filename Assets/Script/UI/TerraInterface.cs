using UnityEngine;

public class TerraInterface : MonoBehaviour
{
    private TerraLogick _terraLogick;


    public void Awake()
    {
        _terraLogick = new TerraLogick(this);
    }

    void Update()
    {
        _terraLogick.TerraLogickFunc();
    }
}
