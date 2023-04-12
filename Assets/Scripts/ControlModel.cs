using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlModel : MonoBehaviour
{
    private float rotSpeed = 0.1f;
    public GameObject[] rot_objs;
    
    private bool ControllingModels = true;
    private bool use_mouse = false;


    private float deltaRotation = 0.0f;
    private float deltaScale = 0.0f;
    private float initialDistance;
    private Vector3 initialScale;
    private Vector3 lastScale;
    //[SerializeField]
    private Vector3 minScale = new Vector3(0.2f, 0.2f, 0.2f);
    private Vector3 maxScale = new Vector3(10.0f, 10.0f, 10.0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControlTheModels();
        FingerPinch();
    }

    //For totation
    public void ControlTheModels()
    {
        //if (!ControllingModels) return;
        if (Input.touchCount > 0 && !use_mouse)
        {
            Input.simulateMouseWithTouches = true;
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {

                //Quaternion rotY = Quaternion.Euler(0f, -touch.deltaPosition.x * rotSpeed, 0f);
                //rot_objs[GlobalSetting.Cur_RotModel_ID_inAssetGallery].transform.rotation = rotY * rot_objs[GlobalSetting.Cur_RotModel_ID_inAssetGallery].transform.rotation;


                rot_objs[GlobalSetting.Cur_RotModel_ID_inAssetGallery].transform.Rotate(0f, -touch.deltaPosition.x * rotSpeed, 0f);

                deltaRotation -= touch.deltaPosition.x;



                //hitObject.transform.position = Vector3.Lerp(hitObject.transform.position, touchedPos, Time.deltaTime);

                //GlobalSetting.debuginfo += (-touch.deltaPosition.x * rotSpeed).ToString();
                //GlobalSetting.debuginfo += "+";
                //GlobalSetting.debuginfo += rotY.ToString();




            }
        }
        if (Input.GetMouseButtonDown(0) && use_mouse)
        {
           

           //Quaternion rotY = Quaternion.Euler(0f, -1 * rotSpeed, 0f);
           //rot_objs[GlobalSetting.Cur_RotModel_ID_inAssetGallery].transform.rotation = rotY * rot_objs[GlobalSetting.Cur_RotModel_ID_inAssetGallery].transform.rotation;


            rot_objs[GlobalSetting.Cur_RotModel_ID_inAssetGallery].transform.Rotate(0f, -1 * rotSpeed, 0f);

            deltaRotation -= -1;



        }


    }

    //scale
    private void FingerPinch()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // If any one of touches is cancelled or ended then do nothing
            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
            {
                // Have some notable movement of finger
                /*if (deltaScale > 1.0f || deltaScale < 0.5f)
                {
                    if (GameManager.instance.instructionPhase == InstructionPhase.Scale)
                    {
                        GameManager.instance.instructionPhase = InstructionPhase.Done;
                    }
                }*/

                deltaScale = 0.0f;

                return;
            }

            // Calcuate the initial distance
            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = rot_objs[GlobalSetting.Cur_RotModel_ID_inAssetGallery].transform.localScale;
                lastScale = initialScale;
            }
            else
            {
                // If touch moved
                var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                // If accidentally touched or pinch movement is really small
                if (Mathf.Approximately(initialDistance, 0))
                {
                    return;
                }

                // If Use finger touch is moving both vertically
                // Then it will change the transform position of y-axis
                // Otherwise it will just change the scale of the model
                if (Mathf.Abs(touchZero.deltaPosition.y) > 10.0f && Mathf.Abs(touchOne.deltaPosition.y) > 10.0f)
                {
                    var direction = 1.0f;
                    if (touchZero.deltaPosition.y < 0)
                    {
                        direction *= -1.0f;
                    }
                    const float deltaMoveStep = 0.2f;
                    Vector3 pos_tmp = rot_objs[GlobalSetting.Cur_RotModel_ID_inAssetGallery].transform.position;
                    rot_objs[GlobalSetting.Cur_RotModel_ID_inAssetGallery].transform.position = new Vector3(pos_tmp.x, pos_tmp.y + deltaMoveStep * direction, pos_tmp.z);
                }
                else
                {
                    // Calculate scale factor and apply
                    var factor = currentDistance / initialDistance;
                    deltaScale = factor;
                    rot_objs[GlobalSetting.Cur_RotModel_ID_inAssetGallery].transform.localScale = scaleCompare(initialScale * factor) ? initialScale * factor : lastScale;
                    lastScale = rot_objs[GlobalSetting.Cur_RotModel_ID_inAssetGallery].transform.localScale;
                }
            }
        }
    }


    public void ShowCurModel()
    {
        //foreach(var obj in rot_objs)
        for(int i = 0; i < rot_objs.Length; i++)
        {
            bool IsCur = (i == GlobalSetting.Cur_RotModel_ID_inAssetGallery);

            rot_objs[i].SetActive(IsCur);
        }
        //Play video

    }

    /// <summary>
    /// Truncated the scale within the bounds
    /// </summary>
    /// <param name="newScale"> new scale </param>
    /// <returns> true if newScale is winth the bound </returns>
    private bool scaleCompare(Vector3 newScale)
    {
        if (newScale.x > maxScale.x || newScale.y > maxScale.y || newScale.z > maxScale.z)
        {
            return false;
        }

        if (newScale.x < minScale.x || newScale.y < minScale.y || newScale.z < minScale.z)
        {
            return false;
        }

        return true;
    }
}
