using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerraInterface : MonoBehaviour
{
    private TerraLogick _terraLogick;

    void Start()
    {
        _terraLogick = new TerraLogick(this);
    }

    void Update()
    {
        _terraLogick.TerraLogickFunc();
    }
}
