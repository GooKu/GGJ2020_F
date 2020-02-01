using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public MaskType maskType;

    [Header("初始Rotation")]
    public Vector3 defaultRotation;

    [Header("在地板上的Y值")]
    public float yGroundPos;
    [Header("落下速度")]
    public float fallSpeed;

    bool canCatch = true;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(defaultRotation.x,Random.Range(0,360),defaultRotation.z);
    }

    // Update is called once per frame
    void Update()
    {
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
                    canCatch = false;
                    other.gameObject.transform.parent.GetComponent<PlayerController>().canTake = false;
                    this.transform.SetParent(other.transform.parent);
                    this.transform.localPosition = new Vector3(-.02f, 0, 0);
                }
            }
        }
        else
        {
            if (other.tag == TagManager.Machine)
            {
                this.transform.parent.GetComponent<PlayerController>().canTake = true;
                this.transform.SetParent(other.transform);
                this.transform.localPosition = new Vector3(0, 1f, 0);
                other.gameObject.GetComponent<MachineManager>().AddMaterial(this.gameObject);
            }
        }
        
    }

}
