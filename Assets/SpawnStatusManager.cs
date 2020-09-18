using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnStatusManager : MonoBehaviour
{
    public List<WarriorInfo> warriorInfos;
    public SpawnQueueItem[] queueStateUI = new SpawnQueueItem[5];
    public WarriorSpawner playerSpawner;

    public int spawnID=0; //spawn ID는 외부에서 누른 버튼에 의해 정해지도록 구현
    private Queue<bool> spawnQueue = new Queue<bool>();


    //싱글톤 디자인 패턴 선언
    private static SpawnStatusManager instance = null;



    private void Awake()
    {
        // 싱글톤
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this) //앞에서 이미 생성한 상태에서 새롭게 자리를 잡으려 하면?
        {
            Destroy(this); //죽인다.
        }
        // 싱글톤 끝
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            spawnQueue.Enqueue(true);
        }
        Toggle();
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

        //TODO 스폰 프로그래스 바 실행
        yield return new WaitForSeconds(warriorInfos[spawnID].spawnTick);
        spawnQueue.Dequeue();
        UnToggle();
        Toggle();
        playerSpawner.Spawn(spawnID);//Warrior Spawner에게 생성 명령 보내기
        
    }
}
