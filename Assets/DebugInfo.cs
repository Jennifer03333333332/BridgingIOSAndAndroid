using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugInfo : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Debug: " + GlobalSetting.debuginfo;
    }
}
