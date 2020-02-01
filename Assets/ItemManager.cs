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
            if (other.tag == "Player")
            {
                canCatch = false;
                this.transform.SetParent(other.transform);
                this.transform.localPosition = new Vector3(-.02f, 0, 0);
            }
        }
        
    }

}
