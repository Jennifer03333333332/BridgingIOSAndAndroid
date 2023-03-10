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

    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
        m_ViewImg = ViewportUI.GetComponent<RawImage>();
    }
    //////////////////////Tutorial
    public void OnClickTutorialCloseBtn()
    {
        //GameManager.SendMessage("StartTheGame");
        GlobalSetting.StartGame = true;
        StartUI.SetActive(false);
        GameManager.SendMessage("StartTheGame");
        MapUI.SetActive(true);
        MapUI.SendMessage("RefreshCurMap");
    }
    //////////////////////Walking to the viewpoint
    public void WalkingUIControl(bool state)
    {
        WalkingUI.SetActive(state);
        //if (state)
        //{
        //    MapUI.SendMessage("RefreshCurMap");
        //}
    }
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
    }
    //showing roadmap
    public void ReturnToStageOne()
    {
        MapUI.SetActive(true);
        WalkingUI.SetActive(false);
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
