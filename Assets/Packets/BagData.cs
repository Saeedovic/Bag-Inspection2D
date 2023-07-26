using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagData
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float speed;

    public BagData(Vector3 startPos, Vector3 endPos, float speed)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.speed = speed;
    }
}
