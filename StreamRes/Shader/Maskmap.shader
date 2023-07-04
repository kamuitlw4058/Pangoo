Shader "Hidden/MaskMap"
{
    Properties
    {
        _MainTex("Texture",2D) = "white"{}
        _R("R", 2D) = "black"{}
        _G("G", 2D) = "black"{}
        _B("B",2D) = "black"{}
        _A("A",2D) = "black"{}
        _ToSmoothness("ToSmoothness",float) = 1
        _Gamma("Gamma",float) = 0.45
        _TextureType("TextureType",float) = 0
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
            float _ToSmoothness;
            float _Gamma;
            float _TextureType;
 
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
                if(_TextureType == 0){
                    col.r = tex2D(_R, i.uv).r;
                    col.g = tex2D(_G, i.uv).g;
                    col.b = tex2D(_B, i.uv).b;
                    if(_ToSmoothness == 1){
                        col.a = 1- tex2D(_A, i.uv).g;      
                    }else{
                        col.a =  tex2D(_A, i.uv).g;      
                    }
                    
                }else if(_TextureType == 1){
                    col.r = tex2D(_R, i.uv).r;
                    col.g = tex2D(_G, i.uv).r;
                    col.b = tex2D(_B, i.uv).r;
                    col.a = 1;
                }else if(_TextureType == 2){
                    col.r = tex2D(_R, i.uv).g;
                    col.g = tex2D(_G, i.uv).g;
                    col.b = tex2D(_B, i.uv).g;
                    col.a = 1;
                }else if(_TextureType == 3){
                    col.r = tex2D(_R, i.uv).b;
                    col.g = tex2D(_G, i.uv).b;
                    col.b = tex2D(_B, i.uv).b;
                    col.a = 1;
                }else if(_TextureType == 4){
                    col.r = tex2D(_R, i.uv).a;
                    col.g = tex2D(_G, i.uv).a;
                    col.b = tex2D(_B, i.uv).a;
                    col.a = 1;
                }
          
                col.a = pow(col.a, _Gamma);      //线性空间会对a通道做gamma校正，要自己校正         
                return col;
            }
            ENDCG
        }
    }
}
