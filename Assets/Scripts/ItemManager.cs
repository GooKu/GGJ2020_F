using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    bool canCatch = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
