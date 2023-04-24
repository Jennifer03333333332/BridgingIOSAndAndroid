using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameManagerWithoutGPS : MonoBehaviour
{
    [SerializeField]
    private GameObject UIManager;
    [SerializeField]
    private GameObject MapUIManager;
    [SerializeField]
    private GameObject ScenesParent;
    [SerializeField]
    private GameObject[] SpotsModels;//5 models for 3 viewpoints
    [SerializeField]
    private GameObject[] GiftBoxPrefabs;//index be meshtype

    //private Dictionary<Spots, GameObject[]> ModelsInSpots = new Dictionary<Spots, GameObject[]> { } ;
    [SerializeField]
    public Dictionary<Spots, Dictionary<MeshType, GameObject>> ModelsInSpots = new Dictionary<Spots, Dictionary<MeshType, GameObject>> { };

    public bool use_mouse = false;
    /// <summary>
    /// for touch
    /// </summary>

    private Vector2 touchPosition;


    //state
    private bool EnteredCurSpot = false;
    private bool ControllingModels = false;
    //
    private GameObject cur_Mesh;



    //////////////////
    private void Start()
    {
        touchPosition = Vector2.zero;

        Dictionary<MeshType, GameObject> tmp = new Dictionary<MeshType, GameObject>();
        tmp.Add(MeshType.Barge0, SpotsModels[0]);
        
        //tmp.Add(MeshType.RollingMills0, SpotsModels[1]);
        
        ModelsInSpots.Add(Spots.one, tmp);

        Dictionary<MeshType, GameObject> tmp1 = new Dictionary<MeshType, GameObject>();
        tmp1.Add(MeshType.Train1, SpotsModels[2]);
        ModelsInSpots.Add(Spots.two, tmp1);

        Dictionary<MeshType, GameObject> tmp2 = new Dictionary<MeshType, GameObject>();

        tmp2.Add(MeshType.Barge2, SpotsModels[3]);
        //tmp2.Add(MeshType.RollingMills2, SpotsModels[4]);
        //tmp2.Add(MeshType.Furnaces2, SpotsModels[5]);
        ModelsInSpots.Add(Spots.three, tmp2);

        

    }
    private void Update()
    {
        //ControllingChest();
        //ControlTheModels();
    }
    //Clean everything
    public void Clean()
    {
        //GlobalSetting.camera_filter_state = false;

        EnteredCurSpot = false;
        ControllingModels = false;
    }


    //Press "start"
    public void StartTheGame()
    {
        //Audio
        //gameObject.SendMessage("PlayingSound", "Intro");




    }

    //place gifts
    public void PressFoundDirBtn()
    {

        //Fade old image
        //UIManager.SendMessage("OpenOldImage");

        //var arSessionOrigin = FindObjectOfType<XROrigin>();

        
        //Vector3 FaceDir = screenCenter_WorldPos - arCamera.transform.position;
        //GlobalSetting.debuginfo += "screenCenter_WorldPos+" + screenCenter_WorldPos.ToString();
        // GlobalSetting.debuginfo += "cameraPos+" + arCamera.transform.position.ToString();

        //Vector3 screenCenter_WorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 3, 0f));

        //show return
        UIManager.SendMessage("ReturnBtnControl", true);

        //show one video
        //entry.Value is Mesh gameobject, the parent of giftbox and renderer
        foreach (KeyValuePair<MeshType, GameObject> entry in ModelsInSpots[GlobalSetting.currentSpot])
        {
            //print(entry);
            //Screen.orientation = ScreenOrientation.LandscapeLeft;

            //Debug.Log(GlobalSetting.spots_dictionary[entry.Key].world_pos); 
            float z = GlobalSetting.spots_dictionary[entry.Key].world_pos.z;
            Vector3 screenCenterFar_WorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 3, z));
            entry.Value.transform.position = screenCenterFar_WorldPos;// + GlobalSetting.spots_dictionary[entry.Key].world_pos;  //arCamera.transform.position + GlobalSetting.spots_dictionary[entry.Key].world_pos;//
            GlobalSetting.debuginfo += "Pos+" + entry.Value.transform.position.ToString();
            entry.Value.SendMessage("EnableObjects");
            entry.Value.SendMessage("MeshPartFaceToCamera");
            entry.Value.SendMessage("StartVideo");
            
            //entry.Value.transform.LookAt(arCamera.transform.position, Vector3.up);

        }

        //place objects in front of camera
        //SpotsModels[(int)GlobalSetting.currentSpot].transform.position = arCamera.transform.position + GlobalSetting.spots_dictionary[GlobalSetting.currentMesh].pos;
        ////Show gift, disable mesh
        //GiftBoxPrefabs[(int)GlobalSetting.currentSpot].SetActive(true);
        //SpotsModels[(int)GlobalSetting.currentSpot].SendMessage("DisableObjects");
        //hide 
        EnteredCurSpot = true;
    }

    public void ReturnToFindingDir()
    {

        //GlobalSetting.camera_filter_state = false;

        UIManager.SendMessage("ReturnBtnControl", false);
        GlobalSetting.debuginfo += GlobalSetting.currentSpot;
        //show all chests. Key: Meshtype, value: spot models
        foreach (KeyValuePair<MeshType, GameObject> entry in ModelsInSpots[GlobalSetting.currentSpot])
        {
            entry.Value.SendMessage("DisableObjects");
        }
        //hide 
        EnteredCurSpot = false;
    }


    //On click next
    public void ChangeToNextSpot()
    {
        GlobalSetting.debuginfo += "ClickNext ";
        int curSpotNum = (int)GlobalSetting.currentSpot;
        int nextSpotNum = curSpotNum + 1;
        print(nextSpotNum);
        print((int)Spots.length);
        
        

        

        //model and guide
        foreach (KeyValuePair<MeshType, GameObject> entry in ModelsInSpots[GlobalSetting.currentSpot])
        {
            if(entry.Value) Destroy(entry.Value);
        }

        if (nextSpotNum >= (int)Spots.length)
        {
            Ending();
            return;
        }



        //if (SpotsModels[curSpotNum]) Destroy(SpotsModels[curSpotNum]);
        //GiftBoxPrefabs[nextSpotNum].SetActive(false);
        //Audio
        gameObject.SendMessage("PlayingSound", nextSpotNum);

        GlobalSetting.currentSpot += 1;
        Clean();
        //Show map
        MapUIManager.SetActive(true);
        MapUIManager.SendMessage("RefreshCurMap");
    }


    public void Ending()
    {
        UIManager.SendMessage("EndingUI");
        //gameObject.SendMessage("PlayingSound", (int)GlobalSetting.currentSpot + 1);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GlobalSetting.currentSpot = Spots.one;
        GlobalSetting.StartGame = false;
    }

    public void OpenWebsite()
    {
        print("click");
        Application.OpenURL("https://projects.etc.cmu.edu/bridging-time/");
    }
}
