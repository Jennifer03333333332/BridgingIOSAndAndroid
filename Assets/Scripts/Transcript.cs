using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transcript : MonoBehaviour
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

    // Update is called once per frame
    public void RefreshContent()
    {
        print((int)GlobalSetting.currentSpot);
        for (int i = 0; i < ContentsUI.Length; i++)
        {
            if (i == (int)GlobalSetting.currentSpot) ContentsUI[i].gameObject.SetActive(true);
            else
            {
                ContentsUI[i].gameObject.SetActive(false);
            }
        }

        ScrollView_.GetComponent<ScrollRect>().content = ContentsUI[(int)GlobalSetting.currentSpot];

    }

    public void ChooseContent(int index)
    {
        
        for (int i = 0; i < ContentsUI.Length; i++)
        {
            if (i == index) ContentsUI[i].gameObject.SetActive(true);
            else
            {
                ContentsUI[i].gameObject.SetActive(false);
            }
        }

        ScrollView_.GetComponent<ScrollRect>().content = ContentsUI[index];

    }



}
