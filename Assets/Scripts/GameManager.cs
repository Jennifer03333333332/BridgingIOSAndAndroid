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
    public GameObject SceneOnePrefab;
    public GameObject UIManager;

    //public GameObject currentScene;
    public GameObject[] Scenes;
    public GameObject[] Scenes_viewpoints; //means viewpoints
    public float nearDistance = 3.0f;

    private bool InitGame = false;
    private bool EnteredCurSpot = false;
    // Start is called before the first frame update
    void Start()
    {
        //for test
        InitSpots(Spots.one);
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalSetting.StartGame)//after the tutorial UI
        {
            if (!InitGame)
            {
                InitGame = true;
                if (Scenes_viewpoints[(int)GlobalSetting.currentSpot])
                {
                    Scenes_viewpoints[(int)GlobalSetting.currentSpot].SendMessage("EnableObjects");
                }

                //currentScene.SendMessage("EnableDistanceGuide", true);
            }
            if (Scenes_viewpoints[(int)GlobalSetting.currentSpot])
            {
                var startPos = MathUtils.SetY(ARLocationManager.Instance.MainCamera.transform.position, 1);
                var endPos = MathUtils.SetY(Scenes_viewpoints[(int)GlobalSetting.currentSpot].transform.position, 1);
                GlobalSetting.debuginfo = "end:" + endPos.ToString() + " start:" + startPos.ToString() + EnteredCurSpot.ToString();
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
                    //show the scene
                    Scenes[(int)GlobalSetting.currentSpot].SendMessage("EnableObjects");
                    //remove distance guide
                    //currentScene.SendMessage("EnableDistanceGuide", false);
                }
            }
        }

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
