using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    
    public float maxMX;
    public float maxPX;
    [Space]
    public float moveSpeed = 10;

    Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int moveDirection = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = 1;
        }
        else moveDirection = 0;
        


        move = new Vector3(moveSpeed * moveDirection * Time.deltaTime, 0, 0);

        transform.position += move;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,maxMX,maxPX), transform.position.y,transform.position.z);

    }
}
