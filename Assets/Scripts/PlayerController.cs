using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移動速度")]
    public float moveSpeed;

    [Header("玩家模型Rotation校正值")]
    public Vector3 axisFix;

    [Header("玩家是否可以拿東西")]
    public bool canTake = true;

    //控制方向開關
    bool goUp;
    bool goDown;
    bool goRight;
    bool goLeft;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(axisFix.x, 0 + axisFix.y, axisFix.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (goUp)
        {
            GoUp();
        }
        if (goDown)
        {
            GoDown();
        }
        if (goRight)
        {
            GoRight();
        }
        if (goLeft)
        {
            GoLeft();
        }
    }

    //往上走功能
    private void GoUp()
    {
        transform.rotation = Quaternion.Euler(axisFix.x, 0 + axisFix.y, axisFix.z);
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
    }
    //往上走開關
    public void StartGoUp()
    {
        goUp = true;
    }

    public void StopGoUp()
    {
        goUp = false;
    }
    
    //往下走功能
    private void GoDown()
    {
        transform.rotation = Quaternion.Euler(axisFix.x, 180 + axisFix.y, axisFix.z);
        transform.position += Vector3.back * Time.deltaTime * moveSpeed;
    }
    //往下走開關
    public void StartGoDown()
    {
        goDown = true;
    }

    public void StopGoDown()
    {
        goDown = false;
    }

    //往右走功能
    private void GoRight()
    {
        transform.rotation = Quaternion.Euler(axisFix.x, 90 + axisFix.y, axisFix.z);
        transform.position += Vector3.right * Time.deltaTime * moveSpeed;
    }
    //往右走開關
    public void StartGoRight()
    {
        goRight = true;
    }

    public void StopGoRight()
    {
        goRight = false;
    }

    //往左走功能
    private void GoLeft()
    {
        transform.rotation = Quaternion.Euler(axisFix.x, 270 + axisFix.y, axisFix.z);
        transform.position += Vector3.left * Time.deltaTime * moveSpeed;
    }
    //往左走開關
    public void StartGoLeft()
    {
        goLeft = true;
    }
    public void StopGoLeft()
    {
        goLeft = false;
    }
    
}
