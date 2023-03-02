using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUIManager : MonoBehaviour
{
    //public GameObject MapUI;
    public GameObject[] ReferPanels;
    public GameObject[] ReferImgs;
    public GameObject[] ReferBtns;

    public void ChangeReferImgState(int index)
    {
        ReferImgs[index].SetActive(!ReferImgs[index].activeInHierarchy);
    }

    public void RefreshCurMap()
    {
        print("RefreshCurMap");
        switch (GlobalSetting.currentSpot)
        {
            case Spots.one:
                {
                    ReferPanels[0].SetActive(true);
                    ReferPanels[1].SetActive(false);
                    ReferPanels[2].SetActive(false);
                    break;
                }
            case Spots.two:
                {
                    ReferPanels[0].SetActive(false);
                    ReferPanels[1].SetActive(true);
                    ReferPanels[2].SetActive(false);
                    break;
                }
            case Spots.three:
                {
                    ReferPanels[0].SetActive(false);
                    ReferPanels[1].SetActive(false);
                    ReferPanels[2].SetActive(true);
                    break;
                }
            default:
                {
                    break;
                }
        }

    }
}
