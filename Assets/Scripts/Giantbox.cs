using ARLocation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giantbox : MonoBehaviour
{
    private bool init = false;
    //private PlaceAtLocation placeAtComponent;
    public GameObject MeshPart;
    public MeshType mesh;//for debug mode current mesh
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!init)
        {
            //placeAtComponent = GetComponent<PlaceAtLocation>();
            StorePos();//synchronize the position to global settings
            //EnableObjects();//for bug that child object was enabled
            init = true;
        }
        //if change position per frame: flick
        //for pos and scale
        switch (mesh)
        {
            case MeshType.Giantbox:
                {
                    MeshPart.transform.localScale = new Vector3(GlobalSetting.cube_scale, GlobalSetting.cube_scale, GlobalSetting.cube_scale);
                    break;
                }
            case MeshType.Train:
                {
                    if (TrainSetting.UpdatingPos)
                    {
                        TrainSetting.UpdatingPos = false; MeshPart.transform.localPosition = TrainSetting.pos; TrainSetting.Train_worldpos = transform.position;
                    }
                    if (TrainSetting.UpdatingRot)
                    {
                        TrainSetting.UpdatingRot = false; MeshPart.transform.localRotation *= Quaternion.AngleAxis(45f, Vector3.up);
                    }
                    MeshPart.transform.localScale = new Vector3(TrainSetting.scale, TrainSetting.scale, TrainSetting.scale);
                    break;
                }
            case MeshType.Factory:
                {
                    if (FactorySetting.UpdatingPos)
                    {
                        FactorySetting.UpdatingPos = false; MeshPart.transform.localPosition = FactorySetting.pos;
                    }
                    MeshPart.transform.localScale = new Vector3(FactorySetting.scale, FactorySetting.scale, FactorySetting.scale);
                    break;
                }
            default:
                {

                    break;
                }
        }
        //distance
        //var distance = placeAtComponent.SceneDistance;
        //for debug info
        if (GlobalSetting.currentMesh == mesh)
        {
            //print(transform.localPosition);
            GlobalSetting.debuginfo = "Rot" + MeshPart.transform.localRotation.ToString() + " Pos:" + MeshPart.transform.localPosition.ToString();
            //GlobalSetting.debuginfo = distance.ToString();//show the distance between current mesh and camera
        }
        
    }
    public void EnableDistanceGuide(bool enable)
    {
        if (enable)
        {
            gameObject.AddComponent<ARLocation.UI.DebugDistance>();
        }
        else
        {
            if (gameObject.GetComponent<ARLocation.UI.DebugDistance>())
            {
                Destroy(gameObject.GetComponent<ARLocation.UI.DebugDistance>());
            }
        }
        
    }

    void StorePos()
    {
        //TrainSetting.pos.y += delta;
        if (mesh == MeshType.Giantbox)
        {
            //TrainSetting.pos = transform.localPosition;
        }
        else if (mesh == MeshType.Train)
        {
            TrainSetting.Train_worldpos = transform.position;
            TrainSetting.pos = MeshPart.transform.localPosition;
        }
        else if (mesh == MeshType.Factory)
        {
            FactorySetting.pos = MeshPart.transform.localPosition;
        }
    }
    public void EnableObjects()
    {
        if (transform.GetComponent<MeshRenderer>())
        {
            transform.GetComponent<MeshRenderer>().enabled = true;
        }
        if (transform.childCount > 0)
        {
            for(int i = 0; i< transform.childCount; i++)
            {
                //print(transform.GetChild(i).gameObject);
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
