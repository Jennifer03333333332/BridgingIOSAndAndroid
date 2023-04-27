using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteBtn : MonoBehaviour
{
    
    [SerializeField]
    private Texture[] m_Tex;//2 tex: not mute 0 mute 1
    private RawImage m_Img;

    [SerializeField]
    private GameObject[] twin_MuteBtns;//4 options
    public GameObject transcript_MuteBtn;//4 options
    
    public GameObject slider_obj;//4 options
    private Slider m_slider;

    //private GameObject twin_MuteBtn;

    private bool IsMute = false;
    private float vol = 0.8f;
    // Start is called before the first frame update
    void Awake()
    {
        m_Img = this.gameObject.GetComponent<RawImage>();
        //print(m_Img);
        m_slider = slider_obj.GetComponent<Slider>();
    }

    public void ReturnToInitial()
    {
        if (this.gameObject.activeInHierarchy == true)
        {
            m_Img.texture = m_Tex[0];
            //IsMute = false;
        }
    }
    
    public void PressMute()
    {
        //IsMute = ;
        if (GlobalSetting.IsMutes[(int)GlobalSetting.currentSpot])//before: mute tex; after: not mute tex
        {
            m_Img.texture = m_Tex[0];
        }
        else
        {
            m_Img.texture = m_Tex[1];
        }
        print(m_Img.texture);
        GlobalSetting.IsMutes[(int)GlobalSetting.currentSpot] = !GlobalSetting.IsMutes[(int)GlobalSetting.currentSpot];
    }

    public void SetButtonState(float v)
    {
        //button tex
        print(v);
        print(GlobalSetting.IsMutes[(int)GlobalSetting.currentSpot]);
        if (!GlobalSetting.IsMutes[(int)GlobalSetting.currentSpot])//before: mute tex; after: not mute tex
        {
            m_Img.texture = m_Tex[0];
        }
        else
        {
            m_Img.texture = m_Tex[1];
        }
        //slider length: how to change?
        m_slider.value = v;
        //print(m_slider);c

    }
    //when close transcript, call transcript's mute btn to send msgs to main page
    public void TranstoMapBtnState()//in main map
    {
        print((int)GlobalSetting.currentSpot);
        vol = m_slider.value;
        twin_MuteBtns[(int)GlobalSetting.currentSpot].SendMessage("SetButtonState", vol);
    }
    //when click unfold, call main map's mute btn to send msgs to unique transcript's btn
    public void MaptoTransBtnState()//in main map
    {
        print("unfolds");
        vol = m_slider.value;
        //AudioScyncArgs args = new AudioScyncArgs(GlobalSetting.IsMutes[(int)GlobalSetting.currentSpot], vol);
        transcript_MuteBtn.SendMessage("SetButtonState", vol);
    }
}
