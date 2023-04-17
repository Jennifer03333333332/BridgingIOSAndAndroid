using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class videoplayer_gallery : MonoBehaviour
{
    public GameObject txt_ui;
    public GameObject next_btn;
    // Start is called before the first frame update
    void Start()
    {
        var vp = this.GetComponent<UnityEngine.Video.VideoPlayer>();
        vp.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        //skip to next
        //txt_ui.GetComponent<Text>().text = "Next";
        txt_ui.SetActive(false);
        //close gallery
        //this.gameObject.SetActive(false);
        next_btn.SetActive(true);
    }
}
