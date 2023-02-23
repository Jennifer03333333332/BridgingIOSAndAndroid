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
    //public GameObject DebugInfo;
    //public GameObject Offset_Text;

    private float delta = 0.25f;
    private float FacDelta = 1f;
    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }
    //////////////////////Tutorial
    public void OnClickTutorialCloseBtn()
    {
        //GameManager.SendMessage("StartTheGame");
        GlobalSetting.StartGame = true;
        StartUI.SetActive(false);
    }
    //////////////////////Next btn
    public void OnShowNextBtn()
    {
        NextBtnUI.SetActive(true);
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
    public void OnScaleUpdate(float value)
    {
        switch (GlobalSetting.currentMesh) {
            case MeshType.Giantbox:
                {
                    break;
                }
            case MeshType.Train:
                {
                    TrainSetting.scale = value;
                    break;
                }
            case MeshType.Factory:
                {
                    FactorySetting.scale = value;
                    break;
                }
            default:
                {
                    break;
                }
        }  
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
        
        switch (GlobalSetting.currentMesh)
        {
            case MeshType.Giantbox:
                {
                    break;
                }
            case MeshType.Train:
                {
                    TrainSetting.UpdatingPos = true;
                    TrainSetting.pos.y += delta;
                    break;
                }
            case MeshType.Factory:
                {
                    FactorySetting.UpdatingPos = true;
                    //print(GlobalSetting.UpdatingPos);
                    //print(FactorySetting.pos);
                    FactorySetting.pos.y += FacDelta;
                    break;
                }
            default:
                {
                    
                    break;
                }
        }
    }
    public void OnClickDown()
    {
        switch (GlobalSetting.currentMesh)
        {
            case MeshType.Giantbox:
                {
                    break;
                }
            case MeshType.Train:
                {
                    TrainSetting.UpdatingPos = true;
                    TrainSetting.pos.y -= delta;
                    break;
                }
            case MeshType.Factory:
                {
                    FactorySetting.UpdatingPos = true;
                    FactorySetting.pos.y -= FacDelta;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    public void OnClickLeft()
    {
        switch (GlobalSetting.currentMesh)
        {
            case MeshType.Giantbox:
                {
                    break;
                }
            case MeshType.Train:
                {
                    TrainSetting.UpdatingPos = true;
                    TrainSetting.pos.x -= delta;
                    break;
                }
            case MeshType.Factory:
                {
                    FactorySetting.UpdatingPos = true;
                    FactorySetting.pos.x -= FacDelta;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    public void OnClickRight()
    {
        switch (GlobalSetting.currentMesh)
        {
            case MeshType.Giantbox:
                {
                    break;
                }
            case MeshType.Train:
                {
                    TrainSetting.UpdatingPos = true;
                    TrainSetting.pos.x += delta;
                    break;
                }
            case MeshType.Factory:
                {
                    FactorySetting.UpdatingPos = true;
                    FactorySetting.pos.x += FacDelta;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    //change rotation
    public void OnChangeRotation()
    {
        switch (GlobalSetting.currentMesh)
        {
            case MeshType.Giantbox:
                {
                    break;
                }
            case MeshType.Train:
                {
                    TrainSetting.UpdatingRot = true;
                    break;
                }
            case MeshType.Factory:
                {
                    FactorySetting.UpdatingRot = true;
                    break;
                }
            default:
                {

                    break;
                }
        }
    }

    //////////////////////
    private void Update()
    {
        //print(Scale_Text.GetComponent<Text>());

        //show current scale on UI
        //Train_Scale_Text.GetComponent<Text>().text = "Train Scale: " + TrainSetting.scale;
        //Fac_Scale_Text.GetComponent<Text>().text = "Factory Scale: " + FactorySetting.scale;
        //Offset_Text.GetComponent<Text>().text = "Offset: " + scale_x;

        switch (GlobalSetting.currentMesh)
        {
            case MeshType.Giantbox:
                {
                    break;
                }
            case MeshType.Train:
                {
                    Scale_Text.GetComponent<Text>().text = "Train Scale: " + TrainSetting.scale;

                    break;
                }
            case MeshType.Factory:
                {
                    Scale_Text.GetComponent<Text>().text = "Factory Scale: " + FactorySetting.scale;

                    break;
                }
            default:
                {
                    break;
                }
        }

    }
}
