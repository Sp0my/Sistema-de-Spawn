using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnables : MonoBehaviour
{
    public SpawnArea area;
    
    public virtual void Spawn()
    {

    }

    public virtual void Recycle()
    {
        area.Recycle(this);
    }
}
