using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameManagerWithoutGPS : MonoBehaviour
{
    [SerializeField]
    private GameObject UIManager;
    [SerializeField]
    private GameObject MapUIManager;
    [SerializeField]
    private GameObject ScenesParent;
    [SerializeField]
    private GameObject[] SpotsModels;//5 models for 3 viewpoints
    [SerializeField]
    private GameObject[] GiftBoxPrefabs;//index be meshtype

    //private Dictionary<Spots, GameObject[]> ModelsInSpots = new Dictionary<Spots, GameObject[]> { } ;
    [SerializeField]
    public Dictionary<Spots, Dictionary<MeshType, GameObject>> ModelsInSpots = new Dictionary<Spots, Dictionary<MeshType, GameObject>> { } ;

    private bool use_mouse = true;
    /// <summary>
    /// for touch
    /// </summary>
    [SerializeField]
    private Camera arCamera;
    private Vector2 touchPosition;
    private int touchstate = 0;
    private int OpenedChest = 0;

    //state
    private bool EnteredCurSpot = false;
    private bool ControllingModels = false;
    //
    private GameObject cur_Mesh;
    //////////////////
    private void Start()
    {
        touchPosition = Vector2.zero;

        Dictionary<MeshType, GameObject> tmp = new Dictionary<MeshType, GameObject>();
        tmp.Add(MeshType.Barge0, SpotsModels[0]);
        tmp.Add(MeshType.RollingMills0, SpotsModels[1]);
        ModelsInSpots.Add(Spots.one, tmp);

        Dictionary<MeshType, GameObject> tmp1 = new Dictionary<MeshType, GameObject>();
        tmp1.Add(MeshType.Train1, SpotsModels[2]);
        ModelsInSpots.Add(Spots.two, tmp1);

        Dictionary<MeshType, GameObject> tmp2 = new Dictionary<MeshType, GameObject>();

        tmp2.Add(MeshType.Barge2, SpotsModels[3]);
        tmp2.Add(MeshType.RollingMills2, SpotsModels[4]);
        tmp2.Add(MeshType.Furnaces2, SpotsModels[5]);
        ModelsInSpots.Add(Spots.three, tmp2);

        //foreach (KeyValuePair<Spots, Dictionary<MeshType, GameObject>> t in ModelsInSpots)
        //foreach (var t in ModelsInSpots)
        //{
        //    foreach (var entry in t.Value)
        //    {
        //        print(entry.Value);
        //    }
        //}

        //cur_Mesh = ModelsInSpots[GlobalSetting.currentSpot][MeshType.Barge0];

    }
    private void Update()
    {
        ControllingChest();
        //ControlTheModels();
    }
    //Clean everything
    public void Clean()
    {
        GlobalSetting.camera_filter_state = false;
        touchstate = 0;
        EnteredCurSpot = false;
        ControllingModels = false;
    }


    //Press "start"
    public void StartTheGame()
    {
        //Show "I've found the space"
        

        



    }

    //place gifts, 
    public void PressFoundDirBtn()
    {
        //Fade old image
        UIManager.SendMessage("OpenOldImage");



        //var arSessionOrigin = FindObjectOfType<XROrigin>();
        Vector3 screenCenter_WorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight/3, 20f));
        Vector3 FaceDir = screenCenter_WorldPos - arCamera.transform.position;
        //GlobalSetting.debuginfo += "screenCenter_WorldPos+" + screenCenter_WorldPos.ToString();
       // GlobalSetting.debuginfo += "cameraPos+" + arCamera.transform.position.ToString();

        GlobalSetting.camera_filter_state = true;
        UIManager.SendMessage("WalkingUIControl", false);

        //show return
        UIManager.SendMessage("ReturnBtnControl", true);
        print(GlobalSetting.currentSpot);
        print(ModelsInSpots[GlobalSetting.currentSpot]);
        //show all chests
        foreach (KeyValuePair<MeshType, GameObject> entry in ModelsInSpots[GlobalSetting.currentSpot])
        {
            print(entry);
            //Screen.orientation = ScreenOrientation.LandscapeLeft;
            //entry.Value is Mesh gameobject, the parent of giftbox and renderer
            entry.Value.transform.position = screenCenter_WorldPos + GlobalSetting.spots_dictionary[entry.Key].world_pos;  //arCamera.transform.position + GlobalSetting.spots_dictionary[entry.Key].world_pos;//
            entry.Value.transform.LookAt(arCamera.transform.position, Vector3.up);
            
            GiftBoxPrefabs[(int)entry.Key].SetActive(true);
            //reset touch state for giftbox
            for (int i = 0; i < GiftBoxPrefabs[(int)entry.Key].transform.childCount; i++)
            {
                //print(GiftBoxPrefabs[(int)entry.Key].transform.GetChild(i).gameObject);
                if (i == 0) {
                    GiftBoxPrefabs[(int)entry.Key].transform.GetChild(i).gameObject.SetActive(true);
                    continue;
                }
                GiftBoxPrefabs[(int)entry.Key].transform.GetChild(i).gameObject.SetActive(false);
            }
            if (entry.Value) entry.Value.SendMessage("DisableObjects");
        }

        //place objects in front of camera
        //SpotsModels[(int)GlobalSetting.currentSpot].transform.position = arCamera.transform.position + GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].pos;
        ////Show gift, disable mesh
        //GiftBoxPrefabs[(int)GlobalSetting.currentSpot].SetActive(true);
        //SpotsModels[(int)GlobalSetting.currentSpot].SendMessage("DisableObjects");
        
        //hide 
        EnteredCurSpot = true;


    }

    public void ReturnToFindingDir()
    {
        OpenedChest = 0;
        touchstate = 0;
        GlobalSetting.camera_filter_state = false;
        UIManager.SendMessage("WalkingUIControl", true);
        UIManager.SendMessage("ReturnBtnControl", false);
        GlobalSetting.debuginfo += GlobalSetting.currentSpot;
        //show all chests. Key: Meshtype, value: spot models
        foreach (KeyValuePair<MeshType, GameObject> entry in ModelsInSpots[GlobalSetting.currentSpot])
        {
            GiftBoxPrefabs[(int)entry.Key].SetActive(false);

            entry.Value.SendMessage("DisableObjects");
        }
        //hide 
        EnteredCurSpot = false;
    }

    public void ControllingChest()
    {
        if (!EnteredCurSpot) return;
        Input.simulateMouseWithTouches = true;
        //Check touch for chest
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touchPosition);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    //GlobalSetting.debuginfo += "Touch object: " + hitObject.transform.name;
                    //var Item = GiftBoxPrefabs[(int)GlobalSetting.currentSpot];
                    //foreach (var Item in GiftBoxPrefabs){
                    foreach (KeyValuePair<MeshType, GameObject> entry in ModelsInSpots[GlobalSetting.currentSpot]){
                        var Item = GiftBoxPrefabs[(int)entry.Key];
                        if (Item.name == hitObject.transform.name){//if chest is current spot's chest
                        GlobalSetting.debuginfo += "Touch state: " + touchstate.ToString();
                        //show box on each state
                        if (Item.transform.childCount > 0)
                        {
                            if (touchstate >= Item.transform.childCount)
                            {
                                OnOpenChest(entry.Key);
                            }
                            else
                            {
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
                    }

                }
            }
        }
        //test with click
        if (Input.GetMouseButtonDown(0) && use_mouse)
        {
            //Touch touch = Input.GetTouch(0);
            //touchPosition = touch.position;
            Vector2 mousePos = Input.mousePosition;
            Ray ray = arCamera.ScreenPointToRay(mousePos);
            RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    foreach (KeyValuePair<MeshType, GameObject> entry in ModelsInSpots[GlobalSetting.currentSpot])
                    {
                        var Item = GiftBoxPrefabs[(int)entry.Key];
                        if (Item.name == hitObject.transform.name)
                        {//if chest is current spot's chest
                            GlobalSetting.debuginfo += "Touch state: " + touchstate.ToString();
                            //show box on each state
                            if (Item.transform.childCount > 0)
                            {
                                if (touchstate >= Item.transform.childCount)
                                {
                                    OnOpenChest(entry.Key);
                                }
                                else
                                {
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
                    }

                }
            
        }
    }


    public void OnOpenChest(MeshType m)
    {
        touchstate = 0;
        //now it close every giftbox
        foreach (KeyValuePair<MeshType, GameObject> entry in ModelsInSpots[GlobalSetting.currentSpot])
        {
            if(entry.Key == m)
            {
                //GlobalSetting.debuginfo += entry.Key.ToString() + "Mesh";
                GiftBoxPrefabs[(int)entry.Key].SetActive(false);
                entry.Value.SendMessage("EnableObjects");
            }
        }

        ////Destroy gift box
        //GiftBoxPrefabs[(int)GlobalSetting.currentSpot].SetActive(false);
        ////show the scene
        //SpotsModels[(int)GlobalSetting.currentSpot].SendMessage("EnableObjects");

        OpenedChest++;

        //if (OpenedChest >= GlobalSetting.ModelNumsinSpots[(int)GlobalSetting.currentSpot])
        GlobalSetting.debuginfo += ModelsInSpots[GlobalSetting.currentSpot].Count.ToString();
        if (OpenedChest >= ModelsInSpots[GlobalSetting.currentSpot].Count)
        {
            OnEveryChestsOpen();
        }
        ControllingModels = true;
    }

    public void OnEveryChestsOpen()
    {
        //Show next btn
        UIManager.SendMessage("OnAllChestsOpened");
        ControllingModels = false;

        OpenedChest = 0;
    }

    public void ControlTheModels()
    {
        if (!ControllingModels) return;
        if (Input.touchCount > 0)
        {
            Input.simulateMouseWithTouches = true;
            Touch touch = Input.GetTouch(0);
            //GlobalSetting.debuginfo = "touchPosition: " + touch.position.ToString();

            //touchPosition = touch.position;
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                Transform cur_hitobj= ModelsInSpots[GlobalSetting.currentSpot][MeshType.Barge0].transform;
                Ray ray = arCamera.ScreenPointToRay(touchPosition);
                RaycastHit hitObject;
               
                if (Physics.Raycast(ray, out hitObject))
                {
                    cur_hitobj = hitObject.transform;
                    //GlobalSetting.debuginfo += "hitObject: " + hitObject.transform.name;
                    //Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                    ////hitObject.transform.SendMessage("UpdateModel", touchedPos);
                    //touchedPos.z = hitObject.transform.position.z;
                    //hitObject.transform.position = Vector3.Lerp(hitObject.transform.position, touchedPos, Time.deltaTime);
                    ////foreach (KeyValuePair<MeshType, GameObject> entry in ModelsInSpots[GlobalSetting.currentSpot])
                    //{
                    //}


                }
                if (cur_hitobj)
                {
                    Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 20));//Camera.main.pixelHeight-
                    //touchedPos.z = (Camera.main.transform.position - cur_hitobj.transform.position).magnitude;
                    cur_hitobj.transform.position = Vector3.Lerp(cur_hitobj.transform.position, touchedPos, Time.deltaTime);
                    //GlobalSetting.debuginfo += " to : " + touchedPos.ToString();
                    //GlobalSetting.debuginfo += " cur_hitobj: " + cur_hitobj.transform.position.ToString();

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
        foreach (KeyValuePair<MeshType, GameObject> entry in ModelsInSpots[GlobalSetting.currentSpot])
        {
            if(entry.Value) Destroy(entry.Value);
        }
        foreach (KeyValuePair<MeshType, GameObject> entry in ModelsInSpots[(Spots)nextSpotNum])
        {
            GiftBoxPrefabs[(int)entry.Key].SetActive(false);
        }

        //if (SpotsModels[curSpotNum]) Destroy(SpotsModels[curSpotNum]);
        //GiftBoxPrefabs[nextSpotNum].SetActive(false);


        GlobalSetting.currentSpot += 1;
        Clean();
        //Show map
        MapUIManager.SetActive(true);
        MapUIManager.SendMessage("RefreshCurMap");
    }
}
