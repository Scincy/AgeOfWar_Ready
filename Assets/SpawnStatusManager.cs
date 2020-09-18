using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnStatusManager : MonoBehaviour
{
    public List<WarriorInfo> warriorInfos;
    public SpawnQueueItem[] queueStateUI = new SpawnQueueItem[5];
    public WarriorSpawner playerSpawner;

    public int spawnID=0;
    private Queue<bool> spawnQueue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Toggle()
    {
        for (int i = 0; i < spawnQueue.Count; i++)
        {
            queueStateUI[i].On();
        }
    }

    void UnToggle()
    {
        for (int i = 0; i < 5; i++)
        {
            queueStateUI[i].Off();
        }
    }

    IEnumerator EnQueue()
    {
        if (spawnQueue.Count <= 5)
        {
            spawnQueue.Enqueue(true);
            Toggle();
        }
        else StopCoroutine("EnQueue");

        yield return new WaitForSeconds(warriorInfos[spawnID].spawnTick);
        spawnQueue.Dequeue();
        UnToggle();
        Toggle();
        playerSpawner.Spawn(spawnID);
        //Warrior Spawner에게 생성 명령 보내기
    }
}
