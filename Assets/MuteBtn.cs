using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteBtn : MonoBehaviour
{
    
    [SerializeField]
    private Texture[] m_Tex;//2 tex: not mute 0 mute 1
    private RawImage m_Img;


    private bool IsMute = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Img = this.gameObject.GetComponent<RawImage>();
        print(m_Img);
    }

    
    public void PressMute()
    {
        if (IsMute)//before: mute tex; after: not mute tex
        {
            m_Img.texture = m_Tex[0];
        }
        else
        {
            m_Img.texture = m_Tex[1];
        }
        print(m_Img.texture);
        IsMute = !IsMute;
    }
}
