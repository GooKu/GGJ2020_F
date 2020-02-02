using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //public MaskType maskType;
    private MaterialRandomCreate materialRandomCreate;
    private List<GameObject> list;

    [Header("口罩ID")]
    public int maskTypeId;

    [Header("初始Rotation")]
    public Vector3 defaultRotation;

    [Header("在地板上的Y值")]
    public float yGroundPos;
    [Header("落下速度")]
    public float fallSpeed;

    bool canCatch = true;
    public bool fall = true;
    
    // Start is called before the first frame update
    void Start()
    {
        materialRandomCreate = GameObject.FindObjectOfType<MaterialRandomCreate>();
        transform.rotation = Quaternion.Euler(defaultRotation.x,Random.Range(0,360),defaultRotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!fall) return;
        if(Vector3.Distance(transform.position, new Vector3(transform.position.x, yGroundPos)) < .1f)
        {
            fall = false;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, yGroundPos, transform.position.z), fallSpeed * Time.deltaTime);
    }
    

    private void OnTriggerEnter(Collider other)
    {      

        if (canCatch)
        {
            if (other.tag == TagManager.Player)
            {
                if (other.gameObject.transform.parent.GetComponent<PlayerController>().canTake)
                {
                    fall = false;
                    canCatch = false;
                    other.gameObject.transform.parent.GetComponent<PlayerController>().canTake = false;
                    other.gameObject.transform.parent.GetComponent<PlayerController>().PutSound();
                    this.transform.SetParent(other.transform.parent);
                    this.transform.localPosition = new Vector3(-.02f, 0, 0);

                    materialRandomCreate.RemoveItem(this.gameObject);
                }
            }
        }
        else
        {
            if (other.tag == TagManager.Machine)
            {
                this.transform.parent.GetComponent<PlayerController>().canTake = true;
                this.transform.parent.GetComponent<PlayerController>().PutSound();

                this.transform.SetParent(other.transform);
                this.transform.localPosition = new Vector3(0, 1f, 0);
                other.gameObject.GetComponent<MachineManager>().AddMaterial(this.gameObject);
            }
        }
        
    }
  

}
