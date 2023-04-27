using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SelfieSwitch : MonoBehaviour
{
    private int startOrEnd = 0;//0 and 1

    public GameObject StartUI;
    public GameObject EndUI;

    public GameObject[] playingmodels;
    [SerializeField]
    ARCameraManager m_CameraManager;
    public void JumpToSelfie(int index)
    {
        startOrEnd = index;

        switchCameraFacingDir();
    }


    public void BacktoMain()
    {
        if (startOrEnd == 0)
        {
            StartUI.SetActive(true);
        }
        else if (startOrEnd == 1)
        {
            EndUI.SetActive(true);

        }
        foreach(var obj in playingmodels)
        {
            obj.SetActive(false);
        }
        switchCameraFacingDir();
    }

    public void switchCameraFacingDir()
    {
        Debug.Assert(m_CameraManager != null, "camera manager cannot be null");
        CameraFacingDirection newFacingDirection;
        switch (m_CameraManager.requestedFacingDirection)
        {
            case CameraFacingDirection.World:
                newFacingDirection = CameraFacingDirection.User;

                break;
            case CameraFacingDirection.User:
            default:
                newFacingDirection = CameraFacingDirection.World;
                break;
        }

        //newFacingDirection = CameraFacingDirection.User;
        GlobalSetting.debuginfo = $"Switching ARCameraManager.requestedFacingDirection from {m_CameraManager.requestedFacingDirection} to {newFacingDirection}";
        //Debug.Log($"Switching ARCameraManager.requestedFacingDirection from {m_CameraManager.requestedFacingDirection} to {newFacingDirection}");
        m_CameraManager.requestedFacingDirection = newFacingDirection;
    }
}
