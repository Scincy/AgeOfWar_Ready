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
    public Image progressBar;
    
    
    private Queue<int> spawnQue = new Queue<int>();
    

    //싱글톤 디자인 패턴 선언
    private static SpawnStatusManager instance = null;



    private void Awake()
    {
        /////////////////////////////
        /////////// 싱글톤 ///////////
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this) //앞에서 이미 생성한 상태에서 새롭게 자리를 잡으려 하면?
        {
            Destroy(this); //죽인다.
        }
        ////////// 싱글톤 끝 /////////
        /////////////////////////////
    }



    void Start()
    {
        StartCoroutine(Deque());
    }
    public void Enqueue(int id)
    {
        if (spawnQue.Count < 5)
        {
            this.spawnID = id;
            spawnQue.Enqueue(this.spawnID);
        }
    }





    ////////////////
    ///  NEW  //////
    ////////////////
    public void EnQue(int id)
    {
        if (spawnQue.Count < 5)
        {
            spawnQue.Enqueue(id);
            UpdateQueToggleUI();
            Debug.Log(spawnQue.Count);
        }
        else
        {
            //에러 처리
        }
    }

    private void UpdateQueToggleUI()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i <= spawnQue.Count-1)
            {
                queueStateUI[i].On();
            }
            else { queueStateUI[i].Off(); }
        }
    }

    IEnumerator Deque()
    {
        int spawnPointer;
        while (true)
        {
            if (spawnQue.Count > 0)
            {
                spawnPointer = spawnQue.Peek(); //peek는 처음 부분을 제거하지 않고 반환함
                StartCoroutine("UpdateScrollUI");
                yield return new WaitForSeconds(warriorInfos[spawnPointer].spawnTick);

                Debug.Log(spawnQue.Count+"!");
                playerSpawner.Spawn(spawnQue.Dequeue());
                Debug.Log(spawnQue.Count+"!!");
                UpdateQueToggleUI();
            }
            else yield return null;
        }
    }

    IEnumerator UpdateScrollUI()
    {
        float rate = 1 / warriorInfos[spawnID].spawnTick;
        while (progressBar.fillAmount > 0)
        {
            progressBar.fillAmount = Mathf.Clamp(progressBar.fillAmount - (rate * Time.deltaTime),0,1);
            yield return null;
        }
        progressBar.fillAmount = 1;
    }
}
