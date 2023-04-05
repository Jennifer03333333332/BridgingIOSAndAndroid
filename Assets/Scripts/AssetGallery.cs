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
}
