using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class videoPlayer : MonoBehaviour
{
    private UnityEngine.Video.VideoPlayer vp;
    // Start is called before the first frame update
    void Start()
    {
        //print("start");
        //var videoPlayer = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        vp = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        vp.Prepare();
        vp.Play();

        //StartCoroutine("PlayVideo");
    }

    // Update is called once per frame
    IEnumerator PlayVideo()
    {
        
        yield return new WaitForSeconds(1f);
        vp.Play();
    }
}
