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
    public int typeOneScore;
    public int typeTwoScore;
    public int typeThreeScore;

    [Header("UI控制器")]
    public GameUIManager gameUIManager;

    // Start is called before the first frame update
    void Start()
    {
        
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
        }

        if (material.tag == TagManager.MaskMiddle)
        {
            count++;
            maskMiddle.Add(material);
        }

        if(count == 3)
        {
            CheckMaterials();
        }
    }

    //放入三個材料時判斷是否正確
    public void CheckMaterials()
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
    public void ProduceMask()
    {
        print("Succeed");
        CheckScore();

        ResetMaterialList();
    }

    //錯誤的三個材料
    public void WrongMaterials()
    {
        print("WrongMaterials");
        ResetMaterialList();
    }

    //正確或錯誤組合後重置
    public void ResetMaterialList()
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
    public void CheckScore()
    {
        if (maskMiddle[0].GetComponent<ItemManager>().maskType == MaskType.One)
        {
            if (group == GroupType.Blue)
            {
                gameUIManager.GetComponent<GameUIManager>().setTeamAScore(typeOneScore);
            }
            else
            {
                gameUIManager.GetComponent<GameUIManager>().setTeamBScore(typeOneScore);
            }
        }
        else if (maskMiddle[0].GetComponent<ItemManager>().maskType == MaskType.Two)
        {
            if (group == GroupType.Blue)
            {
                gameUIManager.GetComponent<GameUIManager>().setTeamAScore(typeTwoScore);
            }
            else
            {
                gameUIManager.GetComponent<GameUIManager>().setTeamBScore(typeTwoScore);
            }
        }
        else
        {
            if (group == GroupType.Blue)
            {
                gameUIManager.GetComponent<GameUIManager>().setTeamAScore(typeThreeScore);
            }
            else
            {
                gameUIManager.GetComponent<GameUIManager>().setTeamBScore(typeThreeScore);
            }
        }
    }
}
