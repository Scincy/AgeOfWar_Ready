using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    public GameObject[] warriorPrefab;
    public WarriorInfo[] warriorinfo;
    public Team spawnDirection;

    protected GameObject warrior;
    public float spawnTick;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(int id)
    {
        spawnTick = warriorinfo[id].spawnTick;
        warrior = Instantiate(warriorPrefab[id], transform.position, UnityEngine.Quaternion.identity);
        SetCharactorInfo(id);
    }

    public abstract void SetCharactorInfo(int id);
}
