using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeshType {
    Giantbox,Train,Factory
}

public static class GlobalSetting
{
    //public ArrayList cube_offset;// 0,1,2,3: up, down, left, right

    public static float cube_scale;

    public static bool camera_filter_state;

    public static MeshType currentMesh = MeshType.Giantbox;

    public static string debuginfo = "";
}

public static class TrainSetting {
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

