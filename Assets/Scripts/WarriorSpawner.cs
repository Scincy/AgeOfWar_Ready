using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WarriorSpawner : MonoBehaviour
{

    
    public GameObject warriorPrefab;
    public Warrior.MoveWay spawnDirection;
    private GameObject warrior;
    //public float spawnTick=0.5f;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        warrior = Instantiate(warriorPrefab, transform.position, Quaternion.identity);
        warrior.GetComponent<Warrior>().moveDirection = spawnDirection;
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    /*
    IEnumerator Spawn()
    {

    }*/
}
