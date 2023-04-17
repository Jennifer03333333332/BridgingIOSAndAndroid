using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    public GameObject GameManager;
    [SerializeField]
    private GameObject Debug_Menu;
    [SerializeField]
    private GameObject Scale_Text;
    [SerializeField]
    private GameObject PopUI;
    [SerializeField]
    private GameObject[] PopUIImg;
    [SerializeField]
    private GameObject MapUI;
    [SerializeField]
    private GameObject StartUI;
    [SerializeField]
    private GameObject NextBtnUI;
    [SerializeField]
    private GameObject ReturnBtn;
    [SerializeField]
    private GameObject WalkingUI;
    [SerializeField]
    private GameObject FindingModelsUI;
    [SerializeField]
    private GameObject GalleryUI;
    [SerializeField]
    private GameObject[] GalleryPages;
    //public GameObject DebugInfo;
    //public GameObject Offset_Text;
    [SerializeField]
    private GameObject ViewportUI;
    [SerializeField]
    private GameObject[] OutLineUIs;
    private RawImage m_ViewImg;
    private RawImage m_OutlineImg;
    [SerializeField]
    public Texture[] m_ViewTex;
    [SerializeField]
    private GameObject m_OldUI;
    private RawImage m_OldImg;
    [SerializeField]
    public Texture[] m_OldTex;
    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
        m_ViewImg = ViewportUI.GetComponent<RawImage>();
        m_OldImg = m_OldUI.GetComponent<RawImage>();
    }
    //////////////////////Tutorial
    public void OnClickTutorialCloseBtn()
    {
        
        GlobalSetting.StartGame = true;
        StartUI.SetActive(false);
        GameManager.SendMessage("StartTheGame");

        //OpenMap();
    }

    public void OpenMap()
    {
        MapUI.SetActive(true);
        MapUI.SendMessage("RefreshCurMap");
    }
    //////////////////////Walking to the viewpoint

    //change walking UI img
    public void ChangeWalkingUIImg()
    {
        m_ViewImg.texture = m_ViewTex[(int)GlobalSetting.currentSpot];
        foreach (var obj in OutLineUIs)
        {
            obj.SetActive(false);
        }
        OutLineUIs[(int)GlobalSetting.currentSpot].SetActive(true);
    }
    //////////////////////Next btn
    public void OnClickEnterSpotBtn()
    {
        MapUI.SetActive(false);
        WalkingUI.SetActive(true);
        ChangeWalkingUIImg();
        GameManager.SendMessage("StopAllPlayingSound");
    }
    /// <summary>
    /// For old ref img fading
    /// </summary>
    public void OpenOldImage()
    {
        m_OldUI.SetActive(true);
        m_OldImg.texture = m_OldTex[(int)GlobalSetting.currentSpot];
        StartCoroutine(DisableOldImage());
    }
    IEnumerator DisableOldImage()
    {
        // loop over 1 second backwards
        for (float i = 2; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            m_OldImg.color = new Color(1, 1, 1, i/2.0f);
            yield return null;
            if (i < 0.4f)
            {
                m_OldUI.SetActive(false);
                //GlobalSetting.camera_filter_state = true;
            }
        }
        //yield return new WaitForSeconds(2f);
        //m_OldUI.SetActive(false);

    }
    //showing roadmap
    public void ReturnToStageOne()
    {
        MapUI.SetActive(true);
        WalkingUI.SetActive(false);
        GameManager.SendMessage("PlayingSound", (int)GlobalSetting.currentSpot);
    }

    //On every chests opened
    public void OnAllChestsOpened()
    {
        OnShowNextBtn();
        ReturnBtnControl(false);
        FindingModelsUI.SetActive(false);
    }

    public void OnShowNextBtn()
    {
        
        NextBtnUI.SetActive(true);
    }
    public void ReturnBtnControl(bool state)
    {
        ReturnBtn.SetActive(state);//close return btn
    }
    public void OnClickNextBtn()
    {
        NextBtnUI.SetActive(false);
        GameManager.SendMessage("ChangeToNextSpot");

    }
    //////////////////////Map
    public void OnChangeMap()
    {
        MapUI.SetActive(!MapUI.activeInHierarchy);
    }
    public void OnCloseMap()
    {
        MapUI.SetActive(false);
    }
    //////////////////////Pop up info
    //Enter Spot
    public void OnEnterSpot()
    {
        //pop up one ui
        PopUI.SetActive(true);
        //show cur spot img
        switch (GlobalSetting.currentSpot)
        {
            case Spots.one:
                {
                    PopUIImg[0].SetActive(true);
                    PopUIImg[1].SetActive(false);
                    break;
                }
            case Spots.two:
                {
                    PopUIImg[1].SetActive(false);
                    PopUIImg[0].SetActive(true);
                    break;
                }
            case Spots.three:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    public void OnClosePopUI()
    {
        PopUI.SetActive(false);
    }
    //////////////////////Debug info
    //Change scale slider
    public void OnScaleUpdate(float value) {

        //GlobalSetting.spots_dictionary[GlobalSetting.currentSpot].model

        //if (GlobalSetting.spots_dictionary.ContainsKey(GlobalSetting.currentSpot))
        //{

        //}

        //find current debug mesh
        GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].scale = value;
    }



    //Open/close debug menu
    public void OnClickDebugBtn()
    {
        if (Debug_Menu.activeInHierarchy)
        {
            Debug_Menu.SetActive(false);
            GlobalSetting.useDebugMenu = false;
        }
        else
        {
            Debug_Menu.SetActive(true);
            GlobalSetting.useDebugMenu = true;

        }

    }
    //On/off camera filter
    public void OnClickFilterBtn()
    {
        GlobalSetting.camera_filter_state = !GlobalSetting.camera_filter_state;
        //print(GlobalSetting.camera_filter_state);
    }

    //Change pos
    public void OnClickUp()
    {
        GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].UpdatingPos = true;
        GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].pos.y += GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].delta;
        GlobalSetting.debuginfo += GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].pos.ToString();
    }
    public void OnClickDown()
    {
        GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].UpdatingPos = true;
        GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].pos.y -= GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].delta;
    }
    public void OnClickLeft()
    {
        GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].UpdatingPos = true;
        GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].pos.x -= GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].delta;
    }
    public void OnClickRight()
    {
        GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].UpdatingPos = true;
        GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].pos.x += GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].delta;
    }

    //change rotation
    public void OnChangeRotation()
    {
        GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].UpdatingRot = true;
    }


    public void EndingUI()
    {
        GalleryUI.SetActive(true);
        GalleryPages[0].SetActive(false);
        GalleryPages[1].SetActive(true);
    }
    //////////////////////
    private void Update()
    {
        //print(Scale_Text.GetComponent<Text>());

        //show current scale on UI
        //Train_Scale_Text.GetComponent<Text>().text = "Train Scale: " + TrainSetting.scale;
        //Fac_Scale_Text.GetComponent<Text>().text = "Factory Scale: " + FactorySetting.scale;
        //Offset_Text.GetComponent<Text>().text = "Offset: " + scale_x;

        Scale_Text.GetComponent<Text>().text = "Train Scale: " + GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].scale;


    }
}
