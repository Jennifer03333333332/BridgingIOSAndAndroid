using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeshType {//unique
    Barge0, RollingMills0, Train1, Barge2, RollingMills2, Furnaces2
}

//3 spots along the path
public enum Spots { 
    one, two, three, length
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

    public static MeshType currentMesh = MeshType.Barge0;//for debug, set pos, scale..

    public static string debuginfo = "";

    //3 spots
    public static Spots currentSpot = Spots.one;

    //public static MeshType[] s1 = { MeshType.Barge0, MeshType.RollingMills0 };
    //public static MeshType[] s2 = { MeshType.Train1 };
    //public static MeshType[] s3 = { MeshType.Barge2, MeshType.RollingMills2, MeshType.Furnaces2 };

    //public static Dictionary<Spots, MeshType[]> spots2mesh = new Dictionary<Spots, MeshType[]> {
    //    { Spots.one, s1 },
    //    { Spots.two, s2},
    //    { Spots.three, s3},
    //};
    public static int[] ModelNumsinSpots = {2,1,3};
    public static Dictionary<MeshType, ModelsSetting> spots_dictionary = new Dictionary<MeshType, ModelsSetting> {
        //MeshType, world position(world_pos), mesh local pos(pos), scale, delta, rotation degree for each

        //world new Vector3(-2f, -3f, 20)
        {MeshType.Barge0, new ModelsSetting(Spots.one, new Vector3(0f, 0f, 10f), new Vector3(0, 0, 0), 5, 1f, Quaternion.AngleAxis(45f, Vector3.forward))},
        //world new Vector3(50f, -0.5f, 5)
        {MeshType.RollingMills0, new ModelsSetting(Spots.one, new Vector3(50f, -0.5f, 50), new Vector3(0, 0, 0), 1f, 1f, Quaternion.AngleAxis(45f, Vector3.up))},
        //world new Vector3(0, -0.5f, 10)
        {MeshType.Train1, new ModelsSetting(Spots.two, new Vector3(0f, 0f, 0f), new Vector3(0, 0, 0), 6, 1f, Quaternion.AngleAxis(45f, Vector3.up))},
        {MeshType.Barge2, new ModelsSetting(Spots.three, new Vector3(0f, -0.5f, 60), new Vector3(0, 0, 0), 6, 1f, Quaternion.AngleAxis(45f, Vector3.up))},
        {MeshType.RollingMills2, new ModelsSetting(Spots.three, new Vector3(30f, -0.5f, 10), new Vector3(0, 0, 0), 1f, 1f, Quaternion.AngleAxis(45f, Vector3.up))},
        {MeshType.Furnaces2, new ModelsSetting(Spots.three, new Vector3(-20f, -0.5f, 10), new Vector3(0, 0, 0), 1f, 1f, Quaternion.AngleAxis(45f, Vector3.up))},
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


