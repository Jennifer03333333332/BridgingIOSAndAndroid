using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeshType {
    Giantbox,Train, Factory
}

//3 spots along the path
public enum Spots { 
    one, two, three
}


public static class GlobalSetting
{
    public static float cube_scale;

    public static bool camera_filter_state;

    public static MeshType currentMesh = MeshType.Giantbox;//for debug

    public static string debuginfo = "";

    //3 spots
    public static Spots currentSpot = Spots.one;
}

public static class TrainSetting {
    public static Vector3 Train_worldpos;
    public static Vector3 pos;
    public static float scale = 6;
    public static bool UpdatingPos = false;
    public static bool UpdatingRot = false;
}

public static class FactorySetting
{
    public static Vector3 pos;
    public static float scale = 1;
    public static bool UpdatingPos = false;
    public static bool UpdatingRot = false;
}

