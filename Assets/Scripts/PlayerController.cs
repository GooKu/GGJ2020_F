using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移動速度")]
    public float moveSpeed;

    [Header("玩家模型Rotation校正值")]
    public Vector3 axisFix;

    [Header("玩家固定高度")]
    public float yAxisFix;

    [Header("玩家是否可以拿東西")]
    public bool canTake = true;

    public Vector3 bornPosition = Vector3.zero;

    private Rigidbody rig;
    private float direction;

    private AudioSource putAds;

    //控制方向開關
    bool goUp;
    bool goDown;
    bool goRight;
    bool goLeft;
    

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(axisFix.x, direction + axisFix.y, axisFix.z);
        rig = GetComponent<Rigidbody>();
        putAds = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(axisFix.x, direction + axisFix.y, axisFix.z);

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
        direction = 0;
        transform.rotation = Quaternion.Euler(axisFix.x, direction + axisFix.y, axisFix.z);
        //transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
        rig.velocity = Vector3.forward * moveSpeed;
    }
    //往上走開關
    public void StartGoUp()
    {
        goUp = true;
        rig.velocity = Vector3.zero;
    }

    public void StopGoUp()
    {
        goUp = false;
        rig.velocity = Vector3.zero;

    }

    //往下走功能
    private void GoDown()
    {
        direction = 180;
        transform.rotation = Quaternion.Euler(axisFix.x, direction + axisFix.y, axisFix.z);
        //transform.position += Vector3.back * Time.deltaTime * moveSpeed;
        rig.velocity = Vector3.back * moveSpeed;
    }
    //往下走開關
    public void StartGoDown()
    {
        goDown = true;
        rig.velocity = Vector3.zero;
    }

    public void StopGoDown()
    {
        goDown = false;
        rig.velocity = Vector3.zero;

    }

    //往右走功能
    private void GoRight()
    {
        direction = 90;
        transform.rotation = Quaternion.Euler(axisFix.x, direction + axisFix.y, axisFix.z);
        //transform.position += Vector3.right * Time.deltaTime * moveSpeed;
        rig.velocity = Vector3.right * moveSpeed;
    }
    //往右走開關
    public void StartGoRight()
    {
        goRight = true;
        rig.velocity = Vector3.zero;
    }

    public void StopGoRight()
    {
        goRight = false;
        rig.velocity = Vector3.zero;
    }

    //往左走功能
    private void GoLeft()
    {
        direction = 270;
        transform.rotation = Quaternion.Euler(axisFix.x, direction + axisFix.y, axisFix.z);
        //transform.position += Vector3.left * Time.deltaTime * moveSpeed;
        rig.velocity = Vector3.left * moveSpeed;
    }
    //往左走開關
    public void StartGoLeft()
    {
        goLeft = true;
        rig.velocity = Vector3.zero;
    }
    public void StopGoLeft()
    {
        goLeft = false;
        rig.velocity = Vector3.zero;
    }

    public void PutSound()
    {
        putAds.Play();
    }
    
    public void SetBornTransForm(Vector3 pos){
        this.bornPosition = pos;
    }

    public void ResetPlayer(){
        this.transform.position = this.bornPosition;
    }

}
