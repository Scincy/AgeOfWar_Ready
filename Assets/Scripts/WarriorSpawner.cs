using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WarriorSpawner : MonoBehaviour
{
    public Slider spawnProgressBar;
    public GameObject warriorPrefab;
    public Warrior.MoveWay spawnDirection;
    public bool isAI = false;
    public float spawnTick;

    private GameObject warrior;
    private Warrior warriorInfo;
    //public float spawnTick=0.5f;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        warrior = Instantiate(warriorPrefab, transform.position, Quaternion.identity);
        warrior.GetComponent<Warrior>().moveDirection = spawnDirection;

        if (!isAI)
        {
            StartCoroutine("Spawn");
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnTick);
        warrior = Instantiate(warriorPrefab, transform.position, Quaternion.identity);
        warriorInfo = warrior.GetComponent<Warrior>();
        warriorInfo.moveDirection = spawnDirection;
    }

    void SetSlider(float maxTick)
    {
        spawnProgressBar.maxValue = maxTick;
        spawnProgressBar.value = 0;
    }
    /*
    IEnumerator SliderUpdate()
    {
        while (true)
        {
            spawnProgressBar.value += Mathf.Clamp(Time.deltaTime, 0f, spawnProgressBar.maxValue);
            if (spawnProgressBar.value == spawnProgressBar.maxValue)
            {
                break;
            }
        }
        spawnProgressBar.value = 0;
    }*/
    /*
    IEnumerator AISpawn()
    {

    }
    */
}
