using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransImg : MonoBehaviour
{
    public Material ScreenMat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        //ScreenMat.SetColor("_SepiaColor", sepiaColor);
        //ScreenMat.SetFloat("_VignetteAmount", vignetteAmount);
        //ScreenMat.SetFloat("_EffectAmount", OldFilmEffectAmount);

         //ScreenMat.SetTexture("_VignetteTex", vignetteTexture);


        Graphics.Blit(sourceTexture, destTexture, ScreenMat);

  



    }
}
