Shader "Pangoo/SurfaceDissolve"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {} //纹理贴图
		_DisolveTex("DisolveTex",2D) = "white"{}//噪声图，影响哪些位置被消融
		_Threshold("Threshold",Range(0,1)) = 0 //控制消融程度
        _EdgeKeepLength("EdgeKeepLength", Range(0,0.1)) = 0.1 //过渡部分多少大
		_EdgeLength("EdgeLength", Range(0,0.4)) = 0.1 //过渡部分多少大
		_BurnTex("BurnTex", 2D) = "white"{} //渐变图，过渡部分用
		_BurnInstensity("BurnInstensity", Range(0,5)) = 1 //过渡部分的强度
        [HDR]_Color("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
 
        CGPROGRAM
 
        #pragma surface surf Standard fullforwardshadows
 
        #pragma target 3.0
 
        sampler2D _MainTex;
		sampler2D _DisolveTex;
		half _Threshold;
		sampler2D _BurnTex;
		half _EdgeLength;
        half _EdgeKeepLength;
		half _BurnInstensity;
        float4 _Color;
 
        struct Input
        {
            float2 uv_MainTex;
			float2 uv_DisolveTex;
        };
 
 
        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)
 
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //读取纹理贴图
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) ;
            o.Albedo = c.rgb;
 
            //消融
			float cutout = tex2D(_DisolveTex, IN.uv_DisolveTex).r;
            //值小于0时舍弃该片元。

            // 这边的0.001用来当做epsilon使用。不知道有什么默认epsilon变量没有
            clip(cutout - (_Threshold + 0.001f));

           
            float remainThreshold =  (_Threshold * (1 + _EdgeLength )) - _EdgeLength;
			
            //temp越接近0的位置越接近消融边界。
			float temp = saturate(saturate(cutout - remainThreshold) / _EdgeLength);
            //用temp当uv坐标读取渐变纹理。temp越接近0读取位置越靠近左下，temp越接近1读取位置越靠近右上。
            fixed4 edgeColor = tex2D(_BurnTex,IN.uv_MainTex)  * _Color;

            // fixed4 edgeColor = tex2D(_BurnTex,IN.uv_MainTex) * _Color;
			// fixed4 edgeColor = temp * _Color;
            //将过渡的燃烧颜色作为自发光。temp=1时该片元完全不显示过渡颜色，当小于1时显示过渡颜色。
			fixed4 finalColor = _BurnInstensity * lerp(edgeColor, fixed4(0,0,0,0), temp);
			o.Emission = finalColor.rgb;
            
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
