using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gallery : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] ContentsUI;

    [SerializeField]
    private GameObject ScrollView_;
    // Start is called before the first frame update
    void Start()
    {
        //print("start");
    }

    // cur 0: start. cur 1 : before
    public void RefreshContent(int cur)
    {
        print((int)GlobalSetting.currentSpot);
        if(cur == 0)
        {
            ContentsUI[0].gameObject.SetActive(true);
            ContentsUI[1].gameObject.SetActive(false);
            ScrollView_.GetComponent<ScrollRect>().content = ContentsUI[0];
        }
        else
        {
            ContentsUI[1].gameObject.SetActive(true);
            ContentsUI[0].gameObject.SetActive(false);
            ScrollView_.GetComponent<ScrollRect>().content = ContentsUI[1];
        }
        

    }
}
