using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeshType {//unique
    Barge, Train, Factory
}

//3 spots along the path
public enum Spots { 
    one, two, three
}


public class ModelsSetting
{
    public Spots spot_;
    public Vector3 world_pos;
    public Vector3 pos;//MeshPart.transform.localPosition
    public float scale;
    public float delta;
    public bool UpdatingPos = false;
    public bool UpdatingRot = false;
    public Quaternion rot = Quaternion.AngleAxis(45f, Vector3.up);
    public ModelsSetting(Spots sp, Vector3 wp, Vector3 p, float s, float d, Quaternion rot_)
    {
        spot_ = sp;
        world_pos = wp;
        pos = p;
        scale = s;
        delta = d;
        rot = rot_;
    }
}

public static class GlobalSetting
{
    public static bool StartGame = false;
    public static bool useDebugMenu = false;
    public static float cube_scale;

    public static bool camera_filter_state = false;

    public static MeshType currentMesh = MeshType.Barge;//for debug, set pos, scale..

    public static string debuginfo = "";

    //3 spots
    public static Spots currentSpot = Spots.one;

    public static Dictionary<Spots, Vector3> WorldPos = new Dictionary<Spots, Vector3> {
        {Spots.one, new Vector3(0, -0.5f, 100)},
    };
    public static Dictionary<MeshType, ModelsSetting> spots_dictionary = new Dictionary<MeshType, ModelsSetting> {
        //MeshType, world position, mesh local pos(for debug), scale, delta, rotation degree for each
        {MeshType.Barge, new ModelsSetting(Spots.one, new Vector3(0, -0.5f, 100), new Vector3(0, 0, 0), 2, 1f, Quaternion.AngleAxis(45f, Vector3.forward))},
        {MeshType.Train, new ModelsSetting(Spots.two, new Vector3(0, -0.5f, 100), new Vector3(0, 0, 0), 6, 1f, Quaternion.AngleAxis(45f, Vector3.up))},
    };
}


//public static class Model1Setting
//{
//    public static Vector3 world_pos = new Vector3(0, -0.5f, 100);
//    public static Vector3 pos;//MeshPart.transform.localPosition
//    public static float scale = 2;
//    public static bool UpdatingPos = false;
//    public static bool UpdatingRot = false;
//}

//public static class TrainSetting {
//    //public static Vector3 Train_worldpos;
//    public static Vector3 pos;
//    public static float scale = 6;
//    public static bool UpdatingPos = false;
//    public static bool UpdatingRot = false;
//}

//public static class FactorySetting
//{
//    public static Vector3 pos;
//    public static float scale = 1;
//    public static bool UpdatingPos = false;
//    public static bool UpdatingRot = false;
//}


