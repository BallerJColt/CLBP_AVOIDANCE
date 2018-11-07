/****************************
* This script is from this URL:
* https://forum.unity.com/threads/is-single-pass-stereo-rendering-supported-for-rendertextures.485294/
****************************/

using UnityEngine;
using UnityEngine.XR;

public class StereoTest : MonoBehaviour
{
    private Camera m_Camera;

    void Awake()
    {
        m_Camera = GetComponent<Camera>();

        RenderTextureDescriptor desc = new RenderTextureDescriptor(
            XRSettings.eyeTextureWidth * 2,
            XRSettings.eyeTextureHeight,
            RenderTextureFormat.Default,
            24);
        desc.vrUsage = VRTextureUsage.TwoEyes;
        RenderTexture texture = new RenderTexture(desc);
        texture.name = "Stereo RenderTexture";
        texture.Create();
        m_Camera.targetTexture = texture;
    }

    void LateUpdate()
    {
        // Tested enabling UNITY_SINGLE_PASS_STEREO because it wasn't being set by the camera.
        // Shader.EnableKeyword("UNITY_SINGLE_PASS_STEREO");
        m_Camera.Render();
        // Shader.DisableKeyword("UNITY_SINGLE_PASS_STEREO");
    }
}
