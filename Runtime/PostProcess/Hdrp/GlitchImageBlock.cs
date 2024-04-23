#if USE_HDRP
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System;

[Serializable, VolumeComponentMenu("Post-processing/Glitch/GlitchImageBlock")]
public sealed class GlitchImageBlock : CustomPostProcessVolumeComponent, IPostProcessComponent
{
    public BoolParameter Enable = new BoolParameter(false);
    public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f);
    public ClampedFloatParameter Fade = new ClampedFloatParameter(1f, 0f, 1f);


    public ClampedFloatParameter Speed = new ClampedFloatParameter(0.5f, 0f, 1f);

    public ClampedFloatParameter Amount = new ClampedFloatParameter(1f, 0f, 20f);


    public ClampedFloatParameter BlockLayer1_U = new ClampedFloatParameter(9f, 0f, 50f);

    public ClampedFloatParameter BlockLayer1_V = new ClampedFloatParameter(9f, 0f, 50f);

    public ClampedFloatParameter BlockLayer2_U = new ClampedFloatParameter(5f, 0f, 50f);

    public ClampedFloatParameter BlockLayer2_V = new ClampedFloatParameter(5f, 0f, 50f);

    public ClampedFloatParameter BlockLayer1_Indensity = new ClampedFloatParameter(8f, 0f, 50f);

    public ClampedFloatParameter BlockLayer2_Indensity = new ClampedFloatParameter(4f, 0f, 50f);

    public ClampedFloatParameter RGBSplitIndensity = new ClampedFloatParameter(0.5f, 0f, 50f);





    Material m_Material;

    private float TimeX = 1.0f;

    public bool IsActive() => m_Material != null && Enable.value;

    // Do not forget to add this post process in the Custom Post Process Orders list (Project Settings > Graphics > HDRP Global Settings).
    public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

    const string kShaderName = "Hidden/Shader/NewPostProcessVolume";

    public override void Setup()
    {
        if (Shader.Find(kShaderName) != null)
            m_Material = new Material(Shader.Find(kShaderName));
        else
        {
            Debug.LogError($"Unable to find shader '{kShaderName}'. Post Process Volume New Post Process Volume is unable to load.");
        }

    }
    static class ShaderIDs
    {
        internal static readonly int Params = Shader.PropertyToID("_Params");
        internal static readonly int Params2 = Shader.PropertyToID("_Params2");
        internal static readonly int Params3 = Shader.PropertyToID("_Params3");
    }

    public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
    {
        if (m_Material == null)
            return;

        if (Enable.value)
        {
            TimeX += Time.deltaTime;
            if (TimeX > 100)
            {
                TimeX = 0;
            }
            m_Material.SetVector(ShaderIDs.Params, new Vector3(TimeX * Speed.value, Amount.value, Fade.value));
            m_Material.SetVector(ShaderIDs.Params2, new Vector4(BlockLayer1_U.value, BlockLayer1_V.value, BlockLayer2_U.value, BlockLayer2_V.value));
            m_Material.SetVector(ShaderIDs.Params3, new Vector3(RGBSplitIndensity.value, BlockLayer1_Indensity.value, BlockLayer2_Indensity.value));

            m_Material.SetFloat("_Intensity", intensity.value);
            cmd.Blit(source, destination, m_Material, 0);
        }



    }

    public override void Cleanup()
    {
        CoreUtils.Destroy(m_Material);
    }
}
#endif