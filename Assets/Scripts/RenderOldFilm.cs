using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RenderOldFilm : MonoBehaviour
{

    #region Variables 
    public Shader curShader; // old film shader

    public float OldFilmEffectAmount = 1.0f;

    public Color sepiaColor = Color.white;
    public Texture2D vignetteTexture;
    public float vignetteAmount = 1.0f;

    public Texture2D scratchesTexture;
    public float scratchesYSpeed = 10.0f;
    public float scratchesXSpeed = 10.0f;

    public Texture2D dustTexture;
    public float dustYSpeed = 10.0f;
    public float dustXSpeed = 10.0f;

    public float random_range = 0.7f;//-1f, 1f
    public float render_frame_interval = 10.0f;
    public float current_frame = 0.0f;

    private Material screenMat;
    private float randomValue;
    #endregion

    #region Properties
    Material ScreenMat
    {
        get
        {
            if (screenMat == null)
            {
                screenMat = new Material(curShader);
                screenMat.hideFlags = HideFlags.HideAndDontSave;
            }
            return screenMat;
        }
    }
    #endregion

    void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }

        if (!curShader && !curShader.isSupported)
        {
            enabled = false;
        }
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {

        if (curShader != null && GlobalSetting.camera_filter_state)
        {//slow down the speed
            if (current_frame >= render_frame_interval)
            {
                //print("OnRender");
                ScreenMat.SetColor("_SepiaColor", sepiaColor);
                ScreenMat.SetFloat("_VignetteAmount", vignetteAmount);
                ScreenMat.SetFloat("_EffectAmount", OldFilmEffectAmount);

                if (vignetteTexture)
                {
                    ScreenMat.SetTexture("_VignetteTex", vignetteTexture);
                }

                if (scratchesTexture)
                {
                    ScreenMat.SetTexture("_ScratchesTex", scratchesTexture);
                    ScreenMat.SetFloat("_ScratchesYSpeed", scratchesYSpeed);
                    ScreenMat.SetFloat("_ScratchesXSpeed", scratchesXSpeed);
                }

                if (dustTexture)
                {
                    ScreenMat.SetTexture("_DustTex", dustTexture);
                    ScreenMat.SetFloat("_dustYSpeed", dustYSpeed);
                    ScreenMat.SetFloat("_dustXSpeed", dustXSpeed);
                    ScreenMat.SetFloat("_RandomValue", randomValue);
                }

                Graphics.Blit(sourceTexture, destTexture, ScreenMat);
                current_frame = 0.0f;
            }
            else
            {
                current_frame++;
                //Graphics.Blit(sourceTexture, destTexture);
            }

        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }
    }

    // Update is called once per frame
    void Update()
    {
        vignetteAmount = Mathf.Clamp01(vignetteAmount);
        OldFilmEffectAmount = Mathf.Clamp(OldFilmEffectAmount, 0f, 1.0f);//OldFilmEffectAmount, 0f, 1.5f
        randomValue = Random.Range(0f - random_range, random_range);
        //Open/close filter
        //if (GlobalSetting.camera_filter_state != enabled)
        //{
        //    enabled = GlobalSetting.camera_filter_state;
        //}

    }

    void OnDisable()
    {
        if (screenMat)
        {
            DestroyImmediate(screenMat);
        }
    }
}
