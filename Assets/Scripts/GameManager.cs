using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int money = 2000;
    public int exp = 0;
    public int level = 1;

    //TODO
    public float HPupAmount;
    public float ATKupAmount;
    public List<GameObject> teamLeft;
    public List<GameObject> teamRight;
    
    
    //싱글톤 디자인 패턴 선언
    public static GameManager instance = null;



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

    public void ConsumeMoney(int balance)
    {
        if ((money-balance) >= 0)
        {
            money -= balance;
        }
        else
        {
            //돈 부족 알림 처리
        }
    }
    public void LevelUp()
    {
        int expcost = level * 100;
        if (exp >= expcost)
        {
            ++level;
            exp -= expcost;
        }
        else
        {
            // 경험치 부족 알림 처리
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
