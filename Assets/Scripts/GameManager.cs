using ARLocation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Arrange the process. Show each set of models based on current camera position
/// </summary>
public class GameManager : MonoBehaviour
{
    //public GameObject SceneParent;
    //public GameObject SceneOnePrefab;
    public GameObject UIManager;
    public GameObject GuideUIPrefab;

    //public GameObject currentScene;
    public GameObject[] Scenes;
    public GameObject[] Scenes_viewpoints; //means viewpoints
    public float nearDistance = 3.0f;

    private bool InitEachSpot = false;
    private bool EnteredCurSpot = false;
    //////////////////For touch
    [SerializeField]
    private Camera arCamera;
    private Vector2 touchPosition;
    [SerializeField]
    private GameObject[] GiftBoxPrefabs;
    private int touchstate = 0;
    // Start is called before the first frame update
    void Start()
    {
        //for test
        InitSpots(Spots.one);
        touchPosition = Vector2.zero;

    }
    //
    // Update is called once per frame
    void Update()
    {
        if (GlobalSetting.StartGame)//after the tutorial UI
        {
            if (!InitEachSpot)
            {
                InitEachSpot = true;
                PreSpots();
            }
            DuringWalkingToViewPoint();

            //after Enter the spot
            if (Input.touchCount > 0 && EnteredCurSpot) 
            {
                Touch touch = Input.GetTouch(0);
                touchPosition = touch.position;
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = arCamera.ScreenPointToRay(touchPosition);
                    RaycastHit hitObject;
                    if (Physics.Raycast(ray, out hitObject)){
                        //GlobalSetting.debuginfo += "Touch object: " + hitObject.transform.tag;
                        GlobalSetting.debuginfo += "Touch object: " + hitObject.transform.name;
                        foreach (var Item in GiftBoxPrefabs){
                            if (Item.name == hitObject.transform.name){
                                //show box on each state
                                if (Item.transform.childCount > 0)
                                {
                                    if(touchstate >= Item.transform.childCount)
                                    {
                                        //Destroy gift box
                                        Destroy(GiftBoxPrefabs[(int)GlobalSetting.currentSpot]);
                                        //show the scene
                                        Scenes[(int)GlobalSetting.currentSpot].SendMessage("EnableObjects");
                                        //Show next btn
                                        UIManager.SendMessage("OnShowNextBtn");

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
        }

    }
    //show the guide
    public void PreSpots()
    {
        if (Scenes_viewpoints[(int)GlobalSetting.currentSpot])
        {
            Scenes_viewpoints[(int)GlobalSetting.currentSpot].SendMessage("EnableObjects");
        }
    }

    public void DuringWalkingToViewPoint()
    {
        if (Scenes_viewpoints[(int)GlobalSetting.currentSpot])
        {
            if (GiftBoxPrefabs[(int)GlobalSetting.currentSpot].activeInHierarchy)
            {
                GiftBoxPrefabs[(int)GlobalSetting.currentSpot].SetActive(false);
            }
            Scenes[(int)GlobalSetting.currentSpot].SendMessage("DisableObjects");
            //GiftBoxPrefabs[(int)GlobalSetting.currentSpot + 1]
            var startPos = MathUtils.SetY(ARLocationManager.Instance.MainCamera.transform.position, 1);
            var endPos = MathUtils.SetY(Scenes_viewpoints[(int)GlobalSetting.currentSpot].transform.position, 1);
            //GlobalSetting.debuginfo = "end:" + endPos.ToString() + " start:" + startPos.ToString() + EnteredCurSpot.ToString();

            //Vector3 CameraWorldPos = ARLocationManager.Instance.MainCamera.transform.position;
            //Location location = ARLocationManager.Instance.GetLocationForWorldPosition(CameraWorldPos);
            //Vector3 SceneWorldPos = currentScene.transform.position;
            if (Vector3.Distance(startPos, endPos) < nearDistance && !EnteredCurSpot) //the distance could be 0 if not stable
            {
                EnteredCurSpot = true;
                //Pop up UI
                UIManager.SendMessage("OnEnterSpot");

                //Destroy guide
                Destroy(Scenes_viewpoints[(int)GlobalSetting.currentSpot]);

                //remove distance guide
                //currentScene.SendMessage("EnableDistanceGuide", false);
                //show gift box
                GiftBoxPrefabs[(int)GlobalSetting.currentSpot].SetActive(true);
                GlobalSetting.camera_filter_state = true;
            }
        }
    }
    public void ChangeToNextSpot()
    {
        GlobalSetting.debuginfo += "clicknext ";
        //model and guide
        if (Scenes_viewpoints[(int)GlobalSetting.currentSpot]) Destroy(Scenes_viewpoints[(int)GlobalSetting.currentSpot]);
        if (Scenes[(int)GlobalSetting.currentSpot]) Destroy(Scenes[(int)GlobalSetting.currentSpot]);
        GiftBoxPrefabs[(int)GlobalSetting.currentSpot + 1].SetActive(false);

        //create new distance line
        StartCoroutine(CreateDistanceLine((int)GlobalSetting.currentSpot + 1, 0));
        GlobalSetting.currentSpot++;
        GlobalSetting.debuginfo += GlobalSetting.currentSpot.ToString();
        //Clear everything
        touchstate = 0;
        InitEachSpot = false;
        EnteredCurSpot = false;
        GlobalSetting.camera_filter_state = false;
        

    }

    public IEnumerator CreateDistanceLine(int newSpot, float waitTime)
    {
        var options = new PlaceAtLocation.PlaceAtOptions { };
        var location = new Location()
        {
            Latitude = 40.42755994812879,
            Longitude = -79.96209292157984,
            Altitude = 0,
            AltitudeMode = AltitudeMode.GroundRelative
        };
        Scenes_viewpoints[newSpot] = PlaceAtLocation.CreatePlacedInstance(GuideUIPrefab, location, options, true);
        yield return new WaitForSeconds(waitTime);
    }


    //Helper function
    public static void DestoryChildGameObject(Transform trans_)
    {
        if (trans_ != null && trans_.childCount > 0)
            for (int i = 0; i < trans_.childCount; i++)
                GameObject.Destroy(trans_.GetChild(i).gameObject);
    }
    public void InitSpots(Spots spot)//place, but not show
    {
        switch (GlobalSetting.currentSpot)
        {
            case Spots.one:
                {
                    //print(currentScene);
                    //if (currentScene) Destroy(currentScene);

                    //enable current scene
                    //currentScene = Scenes[0];//Instantiate(SceneOnePrefab);
                    
                    //print(currentScene);
                    break;
                }
            case Spots.two:
                {
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

}
