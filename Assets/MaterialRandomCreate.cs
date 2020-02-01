using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialRandomCreate : MonoBehaviour
{
    bool createMaterials;

    [Header("會產生的口罩材料")]
    public GameObject[] maskMiddleMaterials;
    public GameObject[] maskSideMaterials;

    [Header("隨機生成時間範圍")]
    public float minTime;
    public float maxTime;

    [Header("隨機生成的範圍")]
    [Header("X軸")]
    public float xAxis;
    [Header("Z軸")]
    public float zAxis;

    [Header("生成高度")]
    public float height;

    private float maskMiddleCreateTime;
    private float maskSideCreateTime;

    private float maskMiddleTimer;
    private float maskSideTimer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //如果不生成物件return
        if (!createMaterials) return;

        maskMiddleTimer += Time.deltaTime;
        maskSideTimer += Time.deltaTime;

        //生成中間材料
        if (maskMiddleTimer > maskMiddleCreateTime)
        {
            CreateMaskMiddle();
            SetMaskMiddleCreateTime();
            maskMiddleTimer = 0;
        }

        //生成線材料
        if (maskSideTimer > maskSideCreateTime)
        {
            CreateMaskSide();
            SetMaskSideCreateTime();
            maskSideTimer = 0;
        }
    }

    //隨機生成口罩中間的材料
    public void CreateMaskMiddle()
    {
        if (maskMiddleMaterials.Length < 1) return;
        int num = Random.Range(0, maskMiddleMaterials.Length);
        Instantiate(maskMiddleMaterials[num], new Vector3(Random.Range(-xAxis, xAxis), height, Random.Range(-zAxis, zAxis)), Quaternion.identity);
    }

    //隨機生成口罩線的材料
    public void CreateMaskSide()
    {
        if (maskSideMaterials.Length < 1) return;
        int num = Random.Range(0, maskSideMaterials.Length);
        Instantiate(maskSideMaterials[num], new Vector3(Random.Range(-xAxis, xAxis), height, Random.Range(-zAxis, zAxis)), Quaternion.identity);
    }

    //重置口罩中間材料的生成時間
    public void SetMaskMiddleCreateTime()
    {
        maskMiddleCreateTime = Random.Range(minTime, maxTime);
    }

    //重置口罩線的生成時間
    public void SetMaskSideCreateTime()
    {
        maskSideCreateTime = Random.Range(minTime, maxTime);
    }

    //開始生成
    [ContextMenu("StartCreate")]
    public void StartCreate()
    {
        createMaterials = true;
    }

    //停止生成
    [ContextMenu("StopCreate")]
    public void StopCreate()
    {
        createMaterials = false;
    }
}
