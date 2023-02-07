using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    public GameObject Debug_Menu;
    public GameObject Scale_Text;
    public int scale_x;
    //Change scale slider
    public void OnScaleUpdate(float value)
    {
        scale_x = (int)value;
        GlobalSetting.cube_scale = scale_x;
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


    private void Update()
    {
        //print(Scale_Text.GetComponent<Text>());

        //show current scale on UI
        Scale_Text.GetComponent<Text>().text = "Scale: " + scale_x;
        
    }
}
