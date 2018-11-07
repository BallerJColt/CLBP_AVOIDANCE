using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]

public class tintImageEffectScript : MonoBehaviour
{

    public Material material;
    public StereoTest stereoTexture;

    void Start()
    {

        Camera copyCamera = stereoTexture.GetComponent<Camera>();
        material.SetTexture("_Override", copyCamera.targetTexture);
        if (!SystemInfo.supportsImageEffects || null == material ||
           null == material.shader || !material.shader.isSupported)
        {
            enabled = false;
            return;
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}