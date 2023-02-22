using ARLocation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giantbox : MonoBehaviour
{
    private bool init = false;
    public bool EnableMeshPart = true;
    public bool OnChanged = false;
    private GameObject GameManager;

    //private PlaceAtLocation placeAtComponent;
    public GameObject MeshPart;
    public MeshType mesh;//for debug mode current mesh
    private bool useDebugMenu = false;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        //print("Scene init");//after Instantiate. maybe because I'm changing the local pos, so it won't move with the parent's place at locations
    }

    // Update is called once per frame
    void Update()
    {
        if (!init && GlobalSetting.StartGame)
        {
            //placeAtComponent = GetComponent<PlaceAtLocation>();
            DisableObjects();
            //EnableObjects();//for bug that child object was enabled
            init = true;
        }
        //if change position per frame: flick
        if (GlobalSetting.StartGame && useDebugMenu)
        {
            //change pos and scale based on GlobalSetting
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
        }

        //distance
        //var distance = placeAtComponent.SceneDistance;
        //for debug info
        if (GlobalSetting.currentMesh == mesh)
        {
            //print(transform.localPosition);
            //GlobalSetting.debuginfo = "Rot" + MeshPart.transform.localRotation.ToString() + " Pos:" + MeshPart.transform.localPosition.ToString();
            //GlobalSetting.debuginfo = distance.ToString();//show the distance between current mesh and camera
        }

    }

    //first step
    //public void EnableDistanceGuide(bool enable)
    //{
    //    if (enable){
    //        //during tutorials
    //        var compo = gameObject.AddComponent<ARLocation.UI.DebugDistance>();
    //        GlobalSetting.debuginfo = compo.ToString();
    //        //Go to model
    //        GuideUI.SetActive(true);
    //    }
    //    else{
    //        if (gameObject.GetComponent<ARLocation.UI.DebugDistance>())
    //        {
    //            Destroy(gameObject.GetComponent<ARLocation.UI.DebugDistance>());
    //        }
    //        GuideUI.SetActive(false);

    //    }

    //}

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

    //When Enter the spot
    public void EnableObjects()
    {
        MeshPart.gameObject.SetActive(true);
        GlobalSetting.debuginfo += MeshPart.gameObject.ToString();

        if (MeshPart.transform.childCount > 0)
        {
            for(int i = 0; i< MeshPart.transform.childCount; i++)
            {
                //print(MeshPart.transform.GetChild(i).gameObject);
                MeshPart.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        //set local position?
        //MeshPart.transform.localPosition = new Vector3(0, 0, 0);
        //StorePos();//synchronize the position to global settings
        if ( GlobalSetting.currentSpot == Spots.two)
        {
            if (MeshPart.GetComponent<Animation>())
            {
                MeshPart.GetComponent<Animation>().Play("Take001");
                GlobalSetting.debuginfo += MeshPart.gameObject.ToString();

            }
        }


    }
    public void DisableObjects()
    {
        MeshPart.gameObject.SetActive(false);
        if (MeshPart.transform.childCount > 0)
        {
            for (int i = 0; i < MeshPart.transform.childCount; i++)
            {
                //print(MeshPart.transform.GetChild(i).gameObject);
                MeshPart.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    //public void ReceiveMsg(bool state)
    //{
    //    if(state != EnableMeshPart)
    //    {
    //        OnChanged = true;
    //        EnableMeshPart = state;
    //    }
    //}
    //public void IfObjectsActive()
    //{
    //    if (EnableMeshPart)
    //    {
    //        if (MeshPart.transform.childCount > 0)
    //        {
    //            for (int i = 0; i < MeshPart.transform.childCount; i++)
    //            {
    //                //print(MeshPart.transform.GetChild(i).gameObject);
    //                MeshPart.transform.GetChild(i).gameObject.SetActive(true);
    //            }
    //        }
    //        //set local position?
    //        MeshPart.transform.localPosition = new Vector3(0, 0, 0);
    //        StorePos();
    //    }
    //    else
    //    {
    //        if (MeshPart.transform.childCount > 0)
    //        {
    //            for (int i = 0; i < MeshPart.transform.childCount; i++)
    //            {
    //                //print(MeshPart.transform.GetChild(i).gameObject);
    //                MeshPart.transform.GetChild(i).gameObject.SetActive(false);
    //            }
    //        }
    //    }
    //}
}
