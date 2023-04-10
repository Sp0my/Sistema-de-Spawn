using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
public class Range
{
    public float min;
    public float max;
  
    public Range() {min = 0; max = 0; }
    public Range(float a, float b) {min = a; max = b; }
    public float GetValueInRange()
    {
        return Random.Range(min, max);
    }
}



public class SpawnArea : MonoBehaviour
{
    public Spawnables _prefab;
    public Collider area;
    public Collider ground;
    public int initialCount = 5;
    public int maxCount = 10;
    public Range spawnTime = new Range(10, 60);

    private List<Spawnables> _unUsed = new List<Spawnables>();
    private List<Spawnables> _used = new List<Spawnables>();
    private float timeUntilNextSpawn;

    void Start()
    {
        for (int i = 0; i < maxCount; i++)
        {
            Spawnables spawnables = Instantiate(_prefab);
            spawnables.area = this;
            spawnables.transform.parent = this.transform;
            spawnables.gameObject.SetActive(false);
            _unUsed.Add(spawnables);
        }

        for (int i = 0; i < initialCount; i++)
        {
            SpawnMob();
        }

        timeUntilNextSpawn = spawnTime.GetValueInRange();
    }

    void Update()
    {
        timeUntilNextSpawn -= Time.deltaTime;
        if (timeUntilNextSpawn <= 0)
        {
            SpawnMob();
            timeUntilNextSpawn = spawnTime.GetValueInRange();
        }

        Tooltipper t = GetComponent<Tooltipper>();
        if (t)
        {
            string message = $"Prefab used: {_prefab} \n\n Initial Spawns: {initialCount} Max Spawns: {maxCount} \n\n Time for next Spawn: {timeUntilNextSpawn}";
            t.SetMessage(message);
        }
    }


    public void SpawnMob()
    {
        if (_unUsed.Count > 0)
        {
            Spawnables spawnables = _unUsed[0];
            _unUsed.RemoveAt(0);
            _unUsed.Add(spawnables);

            GameObject obj = spawnables.gameObject;
            obj.SetActive(true);

            Vector3 pos = MathUtil.PointInsideArea(area);
            pos = MathUtil.SnapToGround(pos, ground);
            obj.transform.position = pos;

            spawnables.Spawn();
        }
    }
    public void Recycle(Spawnables spawnables)
    {
        spawnables.gameObject.SetActive(false);
        _used.Remove(spawnables);
        _unUsed.Add(spawnables);
    }
}
