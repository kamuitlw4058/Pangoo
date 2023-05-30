Shader "Hidden/MaskMap"
{
    Properties
    {
        _MainTex("Texture",2D) = "white"{}
        _R("Texture", 2D) = "black"{}
        _G("Texture", 2D) = "black"{}
        _B("Texture",2D) = "black"{}
        _A("Texture",2D) = "black"{}
        _Gamma("Gamma",float) = 0.45
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" }
        LOD 100
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _R;
            sampler2D _G;
            sampler2D _B;
            sampler2D _A;
            float _Gamma;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col.r = tex2D(_R, i.uv).r;
                col.g = tex2D(_G, i.uv).g;
                col.b = tex2D(_B, i.uv).b;
                col.a = tex2D(_A, i.uv).g;                
                col.a = pow(col.a, _Gamma);      //线性空间会对a通道做gamma校正，要自己校正         
                return col;
            }
            ENDCG
        }
    }
}
