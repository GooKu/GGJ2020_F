using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreText : MonoBehaviour
{
    public Sprite[] sprites;
    [Header("字串之間的間隔")]
    public float stringOffset = 65.0f;
    public int fontWidth = 50;
    public int fontHeight = 60;
    
    // Start is called before the first frame update
    void Start()
    {
    }
    void cleanChild()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }
    public void setTime(string timeStr)
    {
        //print("setTime"+ timeStr);
        this.cleanChild();
        string centerIndex = Mathf.Round(timeStr.Length / 2).ToString("0.0");
        //Debug.Log(centerIndex);
        float ci = float.Parse(centerIndex);
        float x = 0;
        for (int i = 0; i < timeStr.Length; i++)
        {
            if (timeStr.Substring(i, 1) == ".")
            {
                //print("find" + timeStr.Substring(i, 1));
                GameObject obj = new GameObject();
                Image image = obj.AddComponent<Image>();
                image.sprite = sprites[12];
                x = timeStr.Length - (ci - i) * stringOffset;
                obj.GetComponent<RectTransform>().SetParent(this.GetComponent<RectTransform>());
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 0);
                obj.GetComponent<RectTransform>().sizeDelta = new Vector2(fontWidth, fontHeight);
            }
            else
            {
                GameObject obj = new GameObject();
                Image image = obj.AddComponent<Image>();
                int index = int.Parse(timeStr.Substring(i, 1).ToString());
                //print(index);
                image.sprite = sprites[index];
                x = timeStr.Length - (ci - i) * stringOffset;
                obj.GetComponent<RectTransform>().SetParent(this.GetComponent<RectTransform>());
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 0);
                obj.GetComponent<RectTransform>().sizeDelta = new Vector2(fontWidth, fontHeight);
            }
        }
    }
    public void setScore(int score)
    {
        this.cleanChild();
        string strScore = score.ToString();
        string centerIndex = Mathf.Round(strScore.Length / 2).ToString("0.0");
        Debug.Log(centerIndex);
        float ci = float.Parse(centerIndex);
        float x = 0;
        //int len = sprites.Length;
        for (int i = 0; i < strScore.Length; i++)
        {
            GameObject obj = new GameObject();
            Image image = obj.AddComponent<Image>();
            int index = int.Parse(strScore.Substring(i, 1).ToString());
            image.sprite = sprites[index];
            x = strScore.Length - (ci - i) * stringOffset;
            obj.GetComponent<RectTransform>().SetParent(this.GetComponent<RectTransform>());
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 0);
            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(fontWidth, fontHeight);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
