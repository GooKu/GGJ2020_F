using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    public GroupType group;

    [Header("放入的材料清單")]
    public List<GameObject> maskMiddle;
    public List<GameObject> maskSide;

    public int count = 0;
    
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
}
