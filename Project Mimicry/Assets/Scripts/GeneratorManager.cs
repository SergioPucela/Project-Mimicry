using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{

    public List<FanManager> fansToIgnite = new List<FanManager>();

    public LaserCube laserCube;

    public void setFans(bool setBool)
    {
        foreach(FanManager FM in fansToIgnite)
        {
            FM.fanIsON = setBool;
            if (setBool)
            {
                FM.setFanCollider(true);
            }
        }
    }
}
