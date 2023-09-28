Shader "Hidden/Shader/NewPostProcessVolume"
{
    Properties
    {
        // This property is necessary to make the CommandBuffer.Blit bind the source texture to _MainTex
        _MainTex("Main Texture", 2DArray) = "grey" {}
    }

    HLSLINCLUDE

    #pragma target 4.5
    #pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch

    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/PostProcessing/Shaders/FXAA.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/PostProcessing/Shaders/RTUpscale.hlsl"

    struct Attributes
    {
        uint vertexID : SV_VertexID;
        UNITY_VERTEX_INPUT_INSTANCE_ID
    };

    struct Varyings
    {
        float4 positionCS : SV_POSITION;
        float2 texcoord   : TEXCOORD0;
        UNITY_VERTEX_OUTPUT_STEREO
    };

    Varyings Vert(Attributes input)
    {
        Varyings output;
        UNITY_SETUP_INSTANCE_ID(input);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
        output.positionCS = GetFullScreenTriangleVertexPosition(input.vertexID);
        output.texcoord = GetFullScreenTriangleTexCoord(input.vertexID);
        return output;
    }

    // List of properties to control your post process effect
    float _Intensity;
	uniform half3 _Params;
	uniform half4 _Params2;
	uniform half3 _Params3;

	#define _TimeX _Params.x
	#define _Offset _Params.y
	#define _Fade _Params.z

	#define _BlockLayer1_U _Params2.w
	#define _BlockLayer1_V _Params2.x
	#define _BlockLayer2_U _Params2.y
	#define _BlockLayer2_V _Params2.z

	#define _RGBSplit_Indensity _Params3.x
	#define _BlockLayer1_Indensity _Params3.y
	#define _BlockLayer2_Indensity _Params3.z


    TEXTURE2D_X(_MainTex);
    SAMPLER(sampler_MainTex);

    float randomNoise(float2 seed)
	{
		return frac(sin(dot(seed * floor(_TimeX * 30.0), float2(127.1, 311.7))) * 43758.5453123);
	}
	
	float randomNoise(float seed)
	{
		return randomNoise(float2(seed, 1.0));
	}


    float4 CustomPostProcess(Varyings input) : SV_Target
    {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

        // Note that if HDUtils.DrawFullScreen is used to render the post process, use ClampAndScaleUVForBilinearPostProcessTexture(input.texcoord.xy) to get the correct UVs

        // float3 sourceColor = SAMPLE_TEXTURE2D_X(_MainTex, s_linear_clamp_sampler, input.texcoord).xyz;

        // //Apply greyscale effect
        // float3 color = lerp(sourceColor, Luminance(sourceColor), _Intensity);

        // // return float4(color, 1);
        // return float4(color, 1);

        float2 uv = input.texcoord.xy;
		
		//求解第一层blockLayer
		float2 blockLayer1 = floor(uv * float2(_BlockLayer1_U, _BlockLayer1_V));
		float2 blockLayer2 = floor(uv * float2(_BlockLayer2_U, _BlockLayer2_V));

		//return float4(blockLayer1, blockLayer2);
		
		float lineNoise1 = pow(randomNoise(blockLayer1), _BlockLayer1_Indensity);
		float lineNoise2 = pow(randomNoise(blockLayer2), _BlockLayer2_Indensity);
		float RGBSplitNoise = pow(randomNoise(5.1379), 7.1) * _RGBSplit_Indensity;
		float lineNoise = lineNoise1 * lineNoise2 * _Offset  - RGBSplitNoise;
		
		float4 colorR = SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, uv);
		float4 colorG = SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, uv + float2(lineNoise * 0.05 * randomNoise(7.0), 0));
		float4 colorB = SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, uv - float2(lineNoise * 0.05 * randomNoise(23.0), 0));
		
		float4 result = float4(float3(colorR.x, colorG.y, colorB.z), colorR.a + colorG.a + colorB.a);
		result = lerp(colorR, result, _Fade);
		
		return result;

    }

    ENDHLSL

    SubShader
    {
        Tags{ "RenderPipeline" = "HDRenderPipeline" }
        Pass
        {
            Name "New Post Process Volume"

            ZWrite Off
            ZTest Always
            Blend Off
            Cull Off

            HLSLPROGRAM
                #pragma fragment CustomPostProcess
                #pragma vertex Vert
            ENDHLSL
        }
    }
    Fallback Off
}
