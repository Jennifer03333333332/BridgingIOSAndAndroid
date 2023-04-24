using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using System.IO;
using System.Text;

public class UIManager : MonoBehaviour
{
    //Buttons for models
    [SerializeField]
    private Button[] UI_icons;

    [SerializeField]
    private Sprite[] unselected_imgs;

    [SerializeField]
    private Sprite[] selected_imgs;

    [SerializeField]
    private GameObject[] models;

    //Screenshot
    [SerializeField]
    private Button capture_btn;

    [SerializeField]
    private Text debug_msg;

    [SerializeField]
    private Button fold_btn;

    [SerializeField]
    private Button unfold_btn;

    [SerializeField]
    private GameObject hiddenUI;

    [SerializeField]
    private GameObject unfold_Btn;

    //Back and Tutorial
    [SerializeField]
    private Button back_btn;

    [SerializeField]
    private Button tutorial_btn;

    [SerializeField]
    private Button confirm_btn;

    [SerializeField]
    private GameObject tutorial_page;

    //Manage the picture display
    [SerializeField]
    private GameObject myCanvas;

    public GameObject Logo;

    public GameObject Prompt;


    // Start is called before the first frame update
    void Start()
    {
        tutorial_page.SetActive(true);
        foreach (var Item in models)
        {
            Item.SetActive(false);
        }

        UI_icons[0].onClick.AddListener(delegate { ChangeModel(0); });
        UI_icons[1].onClick.AddListener(delegate { ChangeModel(1); });
        UI_icons[2].onClick.AddListener(delegate { ChangeModel(2); });
        capture_btn.onClick.AddListener(Capture);
        tutorial_btn.onClick.AddListener(ShowTutorial);
        confirm_btn.onClick.AddListener(HideTutorial);
        fold_btn.onClick.AddListener(FoldUI);
        unfold_btn.onClick.AddListener(UnfoldUI);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Capture()
    {
        StartCoroutine(ScreenShot());
    }

    IEnumerator ScreenShot()
    {
        //Full Screen Shot
        string filename = "bridge_time_selfie-" + System.DateTime.Now.ToString("MM-dd-yy_HH-mm-ss") + ".png";
        string relaventDirToDCIM = "../../../../DCIM/Camera/";
        myCanvas.SetActive(false);
        Logo.SetActive(true);
        yield return new WaitForEndOfFrame();

        ScreenCapture.CaptureScreenshot(relaventDirToDCIM + filename);
        yield return new WaitForEndOfFrame();

        myCanvas.SetActive(true);
        Logo.SetActive(false);

        Prompt.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Prompt.SetActive(false);

        
        //Magic not sure if working
        using (AndroidJavaClass jcUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (AndroidJavaObject joActivity = jcUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (AndroidJavaObject joContext = joActivity.Call<AndroidJavaObject>("getApplicationContext"))
        using (AndroidJavaClass jcMediaScannerConnection = new AndroidJavaClass("android.media.MediaScannerConnection"))
        using (AndroidJavaClass jcEnvironment = new AndroidJavaClass("android.os.Environment"))
        using (AndroidJavaObject joExDir = jcEnvironment.CallStatic<AndroidJavaObject>("getExternalStorageDirectory"))
        {
            jcMediaScannerConnection.CallStatic("scanFile", joContext, new string[] { relaventDirToDCIM + filename }, null, null);
        }
        File.Move(relaventDirToDCIM, "../../../../DCIM/");

        if (System.IO.File.Exists(relaventDirToDCIM + filename))
        {
            System.IO.File.Move(relaventDirToDCIM + filename, "../../../../DCIM/");
            debug_msg.text = "saved";
        }
    }

    void FoldUI()
    {
        hiddenUI.SetActive(false);
        unfold_Btn.SetActive(true);
        Vector3 pos = capture_btn.transform.position;
        pos.y -= 500f;
        capture_btn.transform.position = pos;

    }

    void UnfoldUI()
    {
        hiddenUI.SetActive(true);
        unfold_Btn.SetActive(false);
        Vector3 pos = capture_btn.transform.position;
        pos.y += 500f;
        capture_btn.transform.position = pos;
    }

    void ChangeModel(int i)
    {
        //Output this to console when Button1 or Button3 is clicked
        //Debug.Log("You have clicked the button!");
        foreach (var Item in models)
        {
            Item.SetActive(false);
        }
        
        models[i].SetActive(true);
        for (int j = 0; j < 3; j++)
        {
            UI_icons[j].image.sprite = unselected_imgs[j];
        }
        UI_icons[i].image.sprite = selected_imgs[i];
    }

    void ShowTutorial()
    {
        tutorial_page.SetActive(true);
        Debug.Log("show");
    }

    void HideTutorial()
    {
        tutorial_page.SetActive(false);
    }
}
