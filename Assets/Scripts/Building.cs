using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float HP = 1500f;
    public Team buildingTeam;
    // Start is called before the first frame update
    void Start()
    {
        //////////////////////////////////////////////////
        if (buildingTeam == Team.left)
        {
            GameManager.instance.teamLeft.Add(this.gameObject);
        }else GameManager.instance.teamRight.Add(this.gameObject);
        //////////////////////////////////////////////////
        StartCoroutine(DestroyWait());
    }

    

    // Update is called once per frame
    void Update()
    {
    }


    IEnumerator DestroyWait()
    {
        yield return new WaitUntil(() => (HP <= 0));
        
        //BroadcastMessage("BuildingDestroyed");
        GameManager.instance.BuildingDestroyed(buildingTeam);
    }

    void Demaged()
    {
        
    }
    
}
