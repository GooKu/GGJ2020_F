using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    [Header("放入的材料清單")]
    public List<GameObject> maskMaterials;
    
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
        maskMaterials.Add(material);        
    }
}
