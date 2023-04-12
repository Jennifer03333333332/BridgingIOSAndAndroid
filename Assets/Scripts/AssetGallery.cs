using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetGallery : MonoBehaviour
{
    [SerializeField]
    private GameObject assetUI;
    [SerializeField]
    private GameObject eachAssetUI;
    [SerializeField]
    private GameObject StartUI;
    [SerializeField]
    private GameObject EndUI;
    //For model
    [SerializeField]
    private GameObject Model_WorldUI;
    [SerializeField]
    private GameObject Model_ScreenUI;
    [SerializeField]
    private GameObject model_txtUI;
    [SerializeField]
    public string[] model_assetTxt;
    //end

    //For video
    [SerializeField]
    private GameObject video_UI;
    [SerializeField]
    private GameObject[] video_assets;
    [SerializeField]
    private GameObject[] vp_assets;
    [SerializeField]
    private GameObject video_txtUI;
    [SerializeField]
    public string[] video_assetTxt;


    //end
    private RawImage m_assetImg;
    [SerializeField]
    public Texture[] m_assetTex;

    [SerializeField]
    private GameObject txtUI;
    [SerializeField]
    public string[] m_assetTxt;

    // Start is called before the first frame update
    void Start()
    {
        m_assetImg = assetUI.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenAsset(int id)
    {
        eachAssetUI.SetActive(true);
        //texture
        m_assetImg.texture = m_assetTex[id];
        m_assetImg.SetNativeSize();
        //txt
        txtUI.GetComponent<Text>().text = m_assetTxt[id];
    }


    public void OpenModel(int id)//0 1 2
    {
        //
        if (!GlobalSetting.StartGame)
        {
            StartUI.SetActive(false);
        }
        else
        {
            EndUI.SetActive(false);
        }

        Model_WorldUI.SetActive(true);
        Model_ScreenUI.SetActive(true);
        //model
        GlobalSetting.Cur_RotModel_ID_inAssetGallery = id;
        //set each model
        Model_WorldUI.SendMessage("ShowCurModel");
        //txt
        model_txtUI.GetComponent<Text>().text = model_assetTxt[id];
        //close 
        gameObject.SetActive(false);
    }

    public void OpenVideo(int id)
    {
        video_UI.SetActive(true);
        //each video
        for (int i = 0; i < video_assets.Length; i++)
        {
            bool isCur = (id == i);
            video_assets[i].SetActive(isCur);

        }
        var vp = vp_assets[id].GetComponent<UnityEngine.Video.VideoPlayer>();
        //var vp = video_assets[id].GetComponent<UnityEngine.Video.VideoPlayer>();
        //vp.Prepare();
        vp.Play();
        //txt
        video_txtUI.GetComponent<Text>().text = video_assetTxt[id];

    }

    public void CloseGallery()
    {
        if (!GlobalSetting.StartGame)
        {
            StartUI.SetActive(true);
        }
        else
        {
            EndUI.SetActive(true);
        }
    }
}
