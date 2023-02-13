using ARLocation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Arrange the process. Show each set of models based on current camera position
/// </summary>
public class GameManager : MonoBehaviour
{
    public GameObject SceneParent;
    public GameObject SceneOnePrefab;

    public GameObject currentScene;

    public float nearDistance = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        //for test
        placeModelsBySpot(Spots.one);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScene)
        {
            var startPos = MathUtils.SetY(ARLocationManager.Instance.MainCamera.transform.position, 1);
            var endPos = MathUtils.SetY(currentScene.transform.position, 1);

            //Vector3 CameraWorldPos = ARLocationManager.Instance.MainCamera.transform.position;
            //Location location = ARLocationManager.Instance.GetLocationForWorldPosition(CameraWorldPos);
            //Vector3 SceneWorldPos = currentScene.transform.position;
            if (Vector3.Distance(startPos, endPos) < nearDistance)
            {
                //show the scene
                currentScene.SendMessage("EnableObjects");
                //remove distance guide
                currentScene.SendMessage("EnableDistanceGuide", false);
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
    public void placeModelsBySpot(Spots spot)//place, but not show
    {
        switch (GlobalSetting.currentSpot)
        {
            case Spots.one:
                {
                    Destroy(currentScene);
                    //enable current scene
                    currentScene = Instantiate(SceneOnePrefab);
                    currentScene.SendMessage("EnableDistanceGuide", true);
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
