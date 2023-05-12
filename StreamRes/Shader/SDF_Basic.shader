// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SDF/SDF_Basic"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,0)
		[KeywordEnum(Sphere,RoundedBox,Capsule)] _SDFType("SDF Type", Float) = 0
		_AlphaClip("Alpha Clip", Range( 0 , 1)) = 0.5
		[HDR]_EdgeGlowColor("Edge Glow Color", Color) = (0.8490566,0.3277805,0.1321645,0)
		_NoiseScale("Noise Scale", Float) = 1
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma shader_feature_local _SDFTYPE_SPHERE _SDFTYPE_ROUNDEDBOX _SDFTYPE_CAPSULE
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float3 worldPos;
		};

		uniform float4x4 _SDF_Sphere;
		uniform float4 _Color;
		uniform float4 _EdgeGlowColor;
		uniform float4x4 _SDF_RoundedBox;
		uniform float4x4 _SDF_Capsule;
		uniform float _NoiseScale;
		uniform float _AlphaClip;
		uniform float _Cutoff = 0.5;


		float RoundBoxexact15_g45( float3 p, float3 b, float r )
		{
			  float3 q = abs(p) - b;
			  return length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0) - r;
		}


		float CapsuleLineexact22_g47( float3 p, float3 a, float3 b, float r )
		{
			float3 pa = p - a, ba = b - a;
			  float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );
			  return length( pa - ba*h ) - r;
		}


		float3 mod3D289( float3 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 mod3D289( float4 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 permute( float4 x ) { return mod3D289( ( x * 34.0 + 1.0 ) * x ); }

		float4 taylorInvSqrt( float4 r ) { return 1.79284291400159 - r * 0.85373472095314; }

		float snoise( float3 v )
		{
			const float2 C = float2( 1.0 / 6.0, 1.0 / 3.0 );
			float3 i = floor( v + dot( v, C.yyy ) );
			float3 x0 = v - i + dot( i, C.xxx );
			float3 g = step( x0.yzx, x0.xyz );
			float3 l = 1.0 - g;
			float3 i1 = min( g.xyz, l.zxy );
			float3 i2 = max( g.xyz, l.zxy );
			float3 x1 = x0 - i1 + C.xxx;
			float3 x2 = x0 - i2 + C.yyy;
			float3 x3 = x0 - 0.5;
			i = mod3D289( i);
			float4 p = permute( permute( permute( i.z + float4( 0.0, i1.z, i2.z, 1.0 ) ) + i.y + float4( 0.0, i1.y, i2.y, 1.0 ) ) + i.x + float4( 0.0, i1.x, i2.x, 1.0 ) );
			float4 j = p - 49.0 * floor( p / 49.0 );  // mod(p,7*7)
			float4 x_ = floor( j / 7.0 );
			float4 y_ = floor( j - 7.0 * x_ );  // mod(j,N)
			float4 x = ( x_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 y = ( y_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 h = 1.0 - abs( x ) - abs( y );
			float4 b0 = float4( x.xy, y.xy );
			float4 b1 = float4( x.zw, y.zw );
			float4 s0 = floor( b0 ) * 2.0 + 1.0;
			float4 s1 = floor( b1 ) * 2.0 + 1.0;
			float4 sh = -step( h, 0.0 );
			float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
			float4 a1 = b1.xzyw + s1.xzyw * sh.zzww;
			float3 g0 = float3( a0.xy, h.x );
			float3 g1 = float3( a0.zw, h.y );
			float3 g2 = float3( a1.xy, h.z );
			float3 g3 = float3( a1.zw, h.w );
			float4 norm = taylorInvSqrt( float4( dot( g0, g0 ), dot( g1, g1 ), dot( g2, g2 ), dot( g3, g3 ) ) );
			g0 *= norm.x;
			g1 *= norm.y;
			g2 *= norm.z;
			g3 *= norm.w;
			float4 m = max( 0.6 - float4( dot( x0, x0 ), dot( x1, x1 ), dot( x2, x2 ), dot( x3, x3 ) ), 0.0 );
			m = m* m;
			m = m* m;
			float4 px = float4( dot( x0, g0 ), dot( x1, g1 ), dot( x2, g2 ), dot( x3, g3 ) );
			return 42.0 * dot( m, px);
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = _Color.rgb;
			float4x4 temp_output_26_0_g43 = _SDF_Sphere;
			float3 ase_worldPos = i.worldPos;
			float4 appendResult6_g43 = (float4((ase_worldPos).xyz , 1.0));
			float temp_output_1_0_g44 = 0.0;
			float4x4 temp_output_27_0_g45 = _SDF_RoundedBox;
			float4 appendResult6_g45 = (float4((ase_worldPos).xyz , 1.0));
			float3 p15_g45 = ( mul( temp_output_27_0_g45, appendResult6_g45 ) * 2 ).xyz;
			float temp_output_11_0_g45 = ( 1.0 - temp_output_27_0_g45[3].w );
			float3 appendResult13_g45 = (float3(temp_output_11_0_g45 , temp_output_11_0_g45 , temp_output_11_0_g45));
			float3 b15_g45 = appendResult13_g45;
			float r15_g45 = ( 1.0 - temp_output_11_0_g45 );
			float localRoundBoxexact15_g45 = RoundBoxexact15_g45( p15_g45 , b15_g45 , r15_g45 );
			float temp_output_1_0_g46 = 0.0;
			float4x4 temp_output_26_0_g47 = _SDF_Capsule;
			float4 appendResult6_g47 = (float4((ase_worldPos).xyz , 1.0));
			float3 p22_g47 = ( mul( temp_output_26_0_g47, appendResult6_g47 ) * 2 ).xyz;
			float3 a22_g47 = float3(0,1,0);
			float3 b22_g47 = float3(0,-1,0);
			float r22_g47 = 1.0;
			float localCapsuleLineexact22_g47 = CapsuleLineexact22_g47( p22_g47 , a22_g47 , b22_g47 , r22_g47 );
			float temp_output_1_0_g48 = 0.0;
			#if defined(_SDFTYPE_SPHERE)
				float staticSwitch144 = ( saturate( ( ( -( length( (( mul( temp_output_26_0_g43, appendResult6_g43 ) * 2 )).xyz ) - 1.0 ) - temp_output_1_0_g44 ) / ( max( temp_output_26_0_g43[3].z , 0.001 ) - temp_output_1_0_g44 ) ) ) * temp_output_26_0_g43[3].x );
			#elif defined(_SDFTYPE_ROUNDEDBOX)
				float staticSwitch144 = ( saturate( ( ( -localRoundBoxexact15_g45 - temp_output_1_0_g46 ) / ( max( temp_output_27_0_g45[3].z , 0.001 ) - temp_output_1_0_g46 ) ) ) * temp_output_27_0_g45[3].x );
			#elif defined(_SDFTYPE_CAPSULE)
				float staticSwitch144 = ( saturate( ( ( -localCapsuleLineexact22_g47 - temp_output_1_0_g48 ) / ( max( temp_output_26_0_g47[3].z , 0.001 ) - temp_output_1_0_g48 ) ) ) * temp_output_26_0_g47[3].x );
			#else
				float staticSwitch144 = ( saturate( ( ( -( length( (( mul( temp_output_26_0_g43, appendResult6_g43 ) * 2 )).xyz ) - 1.0 ) - temp_output_1_0_g44 ) / ( max( temp_output_26_0_g43[3].z , 0.001 ) - temp_output_1_0_g44 ) ) ) * temp_output_26_0_g43[3].x );
			#endif
			float simplePerlin3D81 = snoise( ase_worldPos*_NoiseScale );
			simplePerlin3D81 = simplePerlin3D81*0.5 + 0.5;
			o.Emission = ( _EdgeGlowColor * saturate( ( staticSwitch144 + ( staticSwitch144 * simplePerlin3D81 ) ) ) ).rgb;
			o.Alpha = 1;
			clip( step( _AlphaClip , ( 1.0 - saturate( ( staticSwitch144 + ( staticSwitch144 * simplePerlin3D81 ) ) ) ) ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
-1804;456;1638;748;1907.015;459.1587;1.872008;True;False
Node;AmplifyShaderEditor.CommentaryNode;140;-830.6473,-145.0328;Inherit;False;276.0691;372.9621;;3;139;100;108;SDFs - set matrix via script;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;91;-828.9719,367.6467;Inherit;False;1005.545;397.8192;;6;86;85;84;81;83;82;Edge Noise;1,1,1,1;0;0
Node;AmplifyShaderEditor.FunctionNode;139;-770.8628,-83.62029;Inherit;False;SDF_Sphere;8;;43;b94dbd8aed36b324596b4324e98812a3;0;1;26;FLOAT4x4;1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;108;-780.6473,116.9292;Inherit;False;SDF_Capsule;6;;47;a956941de8f21e44198c43fcfcfededb;0;1;26;FLOAT4x4;1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;100;-786.0192,13.78721;Inherit;False;SDF_RoundedBox;10;;45;6c9cb776c8743bd4aa74aed0be909a02;0;1;27;FLOAT4x4;1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;83;-738.1168,638.2661;Inherit;False;Property;_NoiseScale;Noise Scale;4;0;Create;True;0;0;0;False;0;False;1;6.18;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;144;-476.5543,-65.64238;Inherit;False;Property;_SDFType;SDF Type;1;0;Create;True;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;3;Sphere;RoundedBox;Capsule;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;82;-771.5111,452.4644;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.NoiseGeneratorNode;81;-498.2241,530.3557;Inherit;False;Simplex3D;True;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RelayNode;93;-213.9144,-3.471668;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;-275.0227,530.3399;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;85;-120.6226,537.3176;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;86;-1.114128,543.3036;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RelayNode;92;239.7537,548.1229;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;80;137.3531,-19.42898;Inherit;False;Property;_EdgeGlowColor;Edge Glow Color;3;1;[HDR];Create;True;0;0;0;False;0;False;0.8490566,0.3277805,0.1321645,0;21.56009,8.70743,3.356051,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;74;503.3483,574.8575;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;77;452.03,478.3553;Inherit;False;Property;_AlphaClip;Alpha Clip;2;0;Create;True;0;0;0;False;0;False;0.5;0.781;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;76;739.7654,538.6851;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;565.3546,-14.88756;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;71;507.7402,-299.6712;Inherit;False;Property;_Color;Color;0;0;Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;59;992.9329,-69.11857;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;SDF/SDF_Basic;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;Geometry;All;16;all;True;True;True;True;0;False;-1;False;2;False;-1;255;False;-1;255;False;-1;7;False;-1;3;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;5;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;144;1;139;0
WireConnection;144;0;100;0
WireConnection;144;2;108;0
WireConnection;81;0;82;0
WireConnection;81;1;83;0
WireConnection;93;0;144;0
WireConnection;84;0;93;0
WireConnection;84;1;81;0
WireConnection;85;0;93;0
WireConnection;85;1;84;0
WireConnection;86;0;85;0
WireConnection;92;0;86;0
WireConnection;74;0;92;0
WireConnection;76;0;77;0
WireConnection;76;1;74;0
WireConnection;78;0;80;0
WireConnection;78;1;92;0
WireConnection;59;0;71;0
WireConnection;59;2;78;0
WireConnection;59;10;76;0
ASEEND*/
//CHKSM=EBAFB40209529FF70476D1F7E64BDDB0BF2C57DE