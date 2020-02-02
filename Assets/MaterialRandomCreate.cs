using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialRandomCreate : MonoBehaviour
{
    public bool createMaterials;

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

    [Header("生成數量上限")]
    public int maxQuantity;

    
    public List<GameObject> item;

    private float maskMiddleCreateTime;
    private float maskSideCreateTime;

    private float maskMiddleTimer;
    private float maskSideTimer;


    // Start is called before the first frame update
    void Start()
    {
        maskMiddleTimer = maxTime;
        maskSideTimer = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (item.Count > maxQuantity)
        {
            return;
        }

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

        //隨機生成1-3個
        int createNum = Random.Range(1, 3);

        for (int i = 0; i < createNum; i++)
        {
            int num = Random.Range(0, maskMiddleMaterials.Length);
            GameObject go = Instantiate(maskMiddleMaterials[num], new Vector3(Random.Range(-xAxis, xAxis), height, Random.Range(-zAxis, zAxis)), Quaternion.identity);
            item.Add(go);
        }
    }

    //隨機生成口罩線的材料
    public void CreateMaskSide()
    {
        if (maskSideMaterials.Length < 1) return;

        //隨機生成1-3個
        int createNum = Random.Range(2, 4);

        for (int i = 0; i < createNum; i++)
        {
            int num = Random.Range(0, maskSideMaterials.Length);
            GameObject go = Instantiate(maskSideMaterials[num], new Vector3(Random.Range(-xAxis, xAxis), height, Random.Range(-zAxis, zAxis)), Quaternion.identity);
            item.Add(go);
        }
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

    //刪除所有物件
    [ContextMenu("DestroyAllItem")]
    public void DestroyAllItem()
    {
        for(int i = 0; i < item.Count; i++)
        {
            Destroy(item[i]);
        }

        item.Clear();
    }
}
