using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    public int scale_x;
    public GameObject Debug_Menu;
    public GameObject Scale_Text;

    
    public void OnPositionUpdate()
    {

    }
    public void OnScaleUpdate(float value)
    {
        scale_x = (int)value;
    }
    public void TrackPosition()
    {
        
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
    }


    private void Update()
    {
        //Cube_t.transform.localScale = new Vector3(scale_x, scale_x, scale_x);
        GlobalSetting.cube_scale = scale_x;
        //print(Scale_Text.GetComponent<Text>());
        Scale_Text.GetComponent<Text>().text = "Scale: " + scale_x;
        
    }
}
