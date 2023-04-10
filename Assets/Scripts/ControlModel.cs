using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlModel : MonoBehaviour
{
    private float rotSpeed = 0.1f;
    public GameObject[] rot_objs;
    
    private bool ControllingModels = true;
    private bool use_mouse = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControlTheModels();
    }

    public void ControlTheModels()
    {
        //if (!ControllingModels) return;
        if (Input.touchCount > 0 && !use_mouse)
        {
            Input.simulateMouseWithTouches = true;
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
               
                Quaternion rotY = Quaternion.Euler(0f, -touch.deltaPosition.x * rotSpeed, 0f);
                rot_objs[GlobalSetting.Cur_RotModel_ID_inAssetGallery].transform.rotation = rotY * rot_objs[GlobalSetting.Cur_RotModel_ID_inAssetGallery].transform.rotation;

                //hitObject.transform.position = Vector3.Lerp(hitObject.transform.position, touchedPos, Time.deltaTime);

                GlobalSetting.debuginfo += (-touch.deltaPosition.x * rotSpeed).ToString();
                GlobalSetting.debuginfo += "+";
                //GlobalSetting.debuginfo += rotY.ToString();




            }
        }
        if (Input.GetMouseButtonDown(0) && use_mouse)
        {
           

           Quaternion rotY = Quaternion.Euler(0f, -1 * rotSpeed, 0f);
           rot_objs[GlobalSetting.Cur_RotModel_ID_inAssetGallery].transform.rotation = rotY * rot_objs[GlobalSetting.Cur_RotModel_ID_inAssetGallery].transform.rotation;

           



            
        }


    }


    public void ShowCurModel()
    {
        //foreach(var obj in rot_objs)
        for(int i = 0; i < rot_objs.Length; i++)
        {
            bool IsCur = (i == GlobalSetting.Cur_RotModel_ID_inAssetGallery);

            rot_objs[i].SetActive(IsCur);
        }
        //Play video

    }
}
