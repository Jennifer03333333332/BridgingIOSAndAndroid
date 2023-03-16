using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlignExpand : MonoBehaviour
{
    [SerializeField]
    private Texture[] m_Tex;
    
    [SerializeField]
    private GameObject mImgObj;
    private RawImage mImg;
    // Start is called before the first frame update
    void Start()
    {
        mImg = mImgObj.GetComponent<RawImage>();
    }


    public void UpdateAlignRdfTex()
    {
        print("UpdateAlignRdfTex");
        //mImg = TutorialImgObj.GetComponent<RawImage>();
        if(!mImg) mImg = mImgObj.GetComponent<RawImage>(); 
        mImg.texture = m_Tex[(int)GlobalSetting.currentSpot];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
