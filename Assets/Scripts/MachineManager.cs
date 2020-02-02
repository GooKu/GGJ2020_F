using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineManager : MonoBehaviour
{
    public GroupType group;

    [Header("放入的材料清單")]
    public List<GameObject> maskMiddle;
    public List<GameObject> maskSide;

    public int count = 0;

    [Header("各項分數")]
    public int[] maskScore;

    private int totalScore;

    [Header("UI控制器")]
    public GameUIManager gameUIManager;

    [Header("材料數量UI")]
    public ItemPanelCtrl itemPanelCtrl;

    [Header("音效")]
    public AudioClip succeed;
    public AudioClip failure;

    private AudioSource machineAds;

    //[Header("材料位置")]
    private Vector3[] materialMiddlePos = { new Vector3(0f, .5f, 0), new Vector3(-.2f, .5f, 0), new Vector3(.2f, 0, 0) };
    private Vector3[] materialSidePos = { new Vector3(-.5f, .7f, 0), new Vector3(-.5f, .7f, 0), new Vector3(0, 0, 0) };

    //[Header("材料Rotation")]
    private Vector3[] materialSideRot = { new Vector3(0, 0, 0), new Vector3(0, 180, 0), new Vector3(0, 0, 0) };

    // Start is called before the first frame update
    void Start()
    {
        CheckScore();
        machineAds = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMaterial(GameObject material)
    {
        if(material.tag == TagManager.MaskSide)
        {
            count++;
            maskSide.Add(material);
            itemPanelCtrl.setItem1Label(maskSide.Count);
        }

        if (material.tag == TagManager.MaskMiddle)
        {
            count++;
            maskMiddle.Add(material);
            itemPanelCtrl.setItem2Label(maskMiddle.Count);
        }

        if (count > 1)
        {
            SetMaterialsPosition();
        }

        if (count == 3)
        {
            CheckMaterials();
            ResetMaterialsQuantity();
        }
    }

    //放入三個材料時判斷是否正確
    private void CheckMaterials()
    {
        if(maskSide.Count == 2 && maskMiddle.Count == 1)
        {
            ProduceMask();
        }
        else
        {
            WrongMaterials();
        }
    }

    //製作口罩
    private void ProduceMask()
    {
        print("Succeed");

        machineAds.clip = succeed;
        machineAds.Play();

        //抓取全隊分數
        if (group == GroupType.Blue)
        {
            totalScore = gameUIManager.GetComponent<GameUIManager>().getTeamAScore();
        }
        else
        {
            totalScore = gameUIManager.GetComponent<GameUIManager>().getTeamBScore();
        }

        totalScore += maskScore[(maskMiddle[0].GetComponent<ItemManager>().maskTypeId)];

        CheckScore();

        ResetMaterialList();
    }

    //錯誤的三個材料
    private void WrongMaterials()
    {
        print("WrongMaterials");

        machineAds.clip = failure;
        machineAds.Play();

        ResetMaterialList();
    }

    //正確或錯誤組合後重置
    private void ResetMaterialList()
    {
        for(int i = 0; i < maskMiddle.Count; i++)
        {
            Destroy(maskMiddle[i].gameObject);
        }

        for (int i = 0; i < maskSide.Count; i++)
        {
            Destroy(maskSide[i].gameObject);
        }

        maskMiddle.Clear();
        maskSide.Clear();
        count = 0;
    }

    //判斷分數
    private void CheckScore()
    {
        if(group == GroupType.Blue)
        {
            gameUIManager.GetComponent<GameUIManager>().setTeamAScore(totalScore);
        }
        else
        {
            gameUIManager.GetComponent<GameUIManager>().setTeamBScore(totalScore);
        }
    }
    

    [ContextMenu("GetScore")]
    //獲得現有分數
    public int GetScore()
    {
        return totalScore;
    }

    [ContextMenu("ResetScore")]
    //分數重置
    public int ResetScore()
    {
        totalScore = 0;
        CheckScore();
        ResetMaterialsQuantity();
        return totalScore;
    }

    [ContextMenu("ResetMaterialsQuantity")]
    //重置材料數量
    public void ResetMaterialsQuantity()
    {
        itemPanelCtrl.setItem1Label(0);
        itemPanelCtrl.setItem2Label(0);
    }

    private void SetMaterialsPosition()
    {
        for(int i = 0; i< maskSide.Count; i++)
        {
            maskSide[i].transform.localPosition = materialSidePos[i];
            maskSide[i].transform.localRotation = Quaternion.Euler(materialSideRot[i]);
        }

        for (int i = 0; i < maskMiddle.Count; i++)
        {
            maskMiddle[i].transform.localPosition = materialMiddlePos[i];
            maskMiddle[i].transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
