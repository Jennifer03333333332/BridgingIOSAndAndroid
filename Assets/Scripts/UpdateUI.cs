using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    public GameObject GameManager;
    public GameObject Debug_Menu;
    public GameObject Scale_Text;
    public GameObject PopUI;
    public GameObject MapUI;
    public GameObject StartUI;
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
        }
        else
        {
            Debug_Menu.SetActive(true);
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
