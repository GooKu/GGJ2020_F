using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelCtrl : MonoBehaviour
{
    public Image TeamImage;
    public Image Item1;
    public Image Item2;
    public Text Item1Text;
    public Text Item2Text;
    int item1Cnt = 0;
    int item2Cnt = 0;
    public GameObject Player;
    public Camera cam;
    [Header("物件中心點誤差位移")]
    public int fixedY = 56;
    public int fixedX = 0;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        setTeamB();
        setItem1Label(12);
        Init();
    }

    public void setTeamA()
	{
        TeamImage.color = new Color32(74, 77, 192, 255);
	}

    public void setTeamB()
    {
        TeamImage.color = new Color32(193, 74, 74, 255);
    }

    public void setItem1Label(int value)
	{
        Item1Text.text = value.ToString();
        this.item1Cnt = value;
    }
    public void setItem2Label(int value)
    {
        Item2Text.text = value.ToString();
        this.item2Cnt = value;
    }

    public int getItem1Cnt()
	{
        return this.item1Cnt;
	}

    public int getItem2Cnt()
    {
        return this.item2Cnt;
    }

    public void setPlayer(GameObject player)
	{
        this.Player = player;

    }
    public void setPositionWithObject(Vector3 objectWorldPos)
	{
        Vector3 screenPos = cam.WorldToScreenPoint(objectWorldPos);
        
        this.GetComponent<RectTransform>().position = screenPos + new Vector3(fixedX, fixedY, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (this.Player != null)
        {
           
            Vector3 playerPos = new Vector3();
            playerPos = this.Player.GetComponent<Transform>().transform.position;
            setPositionWithObject(playerPos);
            print("update player pos");
            print(playerPos);
        }
    }

    public void Init()
    {
        item1Cnt = 0;
        item2Cnt = 0;
        setItem1Label(item1Cnt);
        setItem2Label(item2Cnt);
    }
}
