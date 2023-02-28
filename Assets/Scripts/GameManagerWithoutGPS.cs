using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerWithoutGPS : MonoBehaviour
{
    [SerializeField]
    private GameObject UIManager;
    [SerializeField]
    private GameObject MapUIManager;
    [SerializeField]
    private GameObject[] SpotsModels;
    [SerializeField]
    private GameObject[] GiftBoxPrefabs;
    /// <summary>
    /// for touch
    /// </summary>
    [SerializeField]
    private Camera arCamera;
    private Vector2 touchPosition;
    private int touchstate = 0;
    //state
    private bool EnteredCurSpot = false;
    //////////////////
    private void Start()
    {
        touchPosition = Vector2.zero;

    }
    private void Update()
    {
        AfterEnterSpots();
    }
    //Clean everything
    public void Clean()
    {
        GlobalSetting.camera_filter_state = false;
        touchstate = 0;
        EnteredCurSpot = false;
    }


    //Press "start"
    public void StartTheGame()
    {
        //Show "I've found the space"
        

        



    }

    public void PressFoundDirBtn()
    {
        UIManager.SendMessage("WalkingUIControl", false);
        //place objects in front of camera
        SpotsModels[(int)GlobalSetting.currentSpot].transform.position = arCamera.transform.position + new Vector3(0,-0.5f,100);
        //Show gift, disable mesh
        GiftBoxPrefabs[(int)GlobalSetting.currentSpot].SetActive(true);
        SpotsModels[(int)GlobalSetting.currentSpot].SendMessage("DisableObjects");
        //hide 
        EnteredCurSpot = true;

        //use debug btn


    }

    public void OnOpenChest()
    {
        //Destroy gift box
        GiftBoxPrefabs[(int)GlobalSetting.currentSpot].SetActive(false);
        //Destroy(GiftBoxPrefabs[(int)GlobalSetting.currentSpot]);
        GlobalSetting.camera_filter_state = true;
        //show the scene
        SpotsModels[(int)GlobalSetting.currentSpot].SendMessage("EnableObjects");
        //Show next btn
        UIManager.SendMessage("OnShowNextBtn");
    }

    public void AfterEnterSpots()
    {
        //Check touch for chest
        if (Input.touchCount > 0 && EnteredCurSpot)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touchPosition);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    //GlobalSetting.debuginfo += "Touch object: " + hitObject.transform.tag;
                    GlobalSetting.debuginfo += "Touch object: " + hitObject.transform.name;
                    var Item = GiftBoxPrefabs[(int)GlobalSetting.currentSpot];
                    //foreach (var Item in GiftBoxPrefabs){
                    if (Item.name == hitObject.transform.name){
                        GlobalSetting.debuginfo += "Touch state: " + touchstate.ToString();
                        //show box on each state
                        if (Item.transform.childCount > 0)
                        {
                            if (touchstate >= Item.transform.childCount)
                            {
                                OnOpenChest();
                            }
                            else
                            {
                                //OnTouchedChest();
                                for (int i = 0; i < Item.transform.childCount; i++)
                                {
                                    //print(Item.transform.GetChild(i).gameObject);
                                    if (i == touchstate)
                                    {
                                        Item.transform.GetChild(i).gameObject.SetActive(true);
                                    }
                                    else
                                    {
                                        Item.transform.GetChild(i).gameObject.SetActive(false);
                                    }
                                }
                                touchstate++;
                                GlobalSetting.debuginfo += "Touch state: " + touchstate.ToString();
                            }

                        }
                    }
                    //}

                }
            }
        }
    }



    //On click next
    public void ChangeToNextSpot()
    {
        GlobalSetting.debuginfo += "ClickNext ";
        int curSpotNum = (int)GlobalSetting.currentSpot;
        int nextSpotNum = curSpotNum + 1;
        //model and guide
        if (SpotsModels[curSpotNum]) Destroy(SpotsModels[curSpotNum]);
        GiftBoxPrefabs[nextSpotNum].SetActive(false);
        GlobalSetting.currentSpot += 1;
        Clean();
        //Show map
        MapUIManager.SetActive(true);
        MapUIManager.SendMessage("RefreshCurMap");
    }
}
