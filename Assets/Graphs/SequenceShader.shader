// Upgrade NOTE: upgraded instancing buffer 'SequenceShader' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SequenceShader"
{
	Properties
	{
		_Scanline1Amount("Scanline 1 Amount", Float) = 1.56
		_Scanline1Speed("Scanline 1 Speed", Float) = 1
		[HDR]_PulseColor("Pulse Color", Color) = (2.680933,2.79544,0,1)
		_Color("Color", Color) = (0.6179246,1,1,1)
		_PulseDirection("Pulse Direction", Float) = 1
		_Tilling("Tilling", Float) = 10
		_EmissiveValue("EmissiveValue", Float) = 1
		_OpacityExp("OpacityExp", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Unlit alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _Tilling;
		uniform float _EmissiveValue;
		uniform float _OpacityExp;

		UNITY_INSTANCING_BUFFER_START(SequenceShader)
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color)
#define _Color_arr SequenceShader
			UNITY_DEFINE_INSTANCED_PROP(float4, _PulseColor)
#define _PulseColor_arr SequenceShader
			UNITY_DEFINE_INSTANCED_PROP(float, _PulseDirection)
#define _PulseDirection_arr SequenceShader
			UNITY_DEFINE_INSTANCED_PROP(float, _Scanline1Amount)
#define _Scanline1Amount_arr SequenceShader
			UNITY_DEFINE_INSTANCED_PROP(float, _Scanline1Speed)
#define _Scanline1Speed_arr SequenceShader
		UNITY_INSTANCING_BUFFER_END(SequenceShader)

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 _Color_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color_arr, _Color);
			float _PulseDirection_Instance = UNITY_ACCESS_INSTANCED_PROP(_PulseDirection_arr, _PulseDirection);
			float4 appendResult328 = (float4(_Tilling , _Tilling , 0.0 , 0.0));
			float2 uv_TexCoord319 = i.uv_texcoord * appendResult328.xy;
			float _Scanline1Amount_Instance = UNITY_ACCESS_INSTANCED_PROP(_Scanline1Amount_arr, _Scanline1Amount);
			float temp_output_40_0 = ( uv_TexCoord319.x * _Scanline1Amount_Instance );
			float _Scanline1Speed_Instance = UNITY_ACCESS_INSTANCED_PROP(_Scanline1Speed_arr, _Scanline1Speed);
			float mulTime43 = _Time.y * _Scanline1Speed_Instance;
			float smoothstepResult339 = smoothstep( 0.1 , 1.0 , (( _PulseDirection_Instance > 0.0 ) ? frac( ( ( 1.0 - temp_output_40_0 ) - mulTime43 ) ) :  frac( ( temp_output_40_0 + mulTime43 ) ) ));
			float temp_output_341_0 = ( smoothstepResult339 + -0.2 );
			float temp_output_342_0 = saturate( ( temp_output_341_0 * step( 0.5 , temp_output_341_0 ) ) );
			float4 _PulseColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_PulseColor_arr, _PulseColor);
			o.Emission = ( ( ( _Color_Instance * temp_output_342_0 ) + _PulseColor_Instance ) * _EmissiveValue ).rgb;
			o.Alpha = ( ( 0.0 + temp_output_342_0 ) * _OpacityExp );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17000
-1351;119.4;1342;689;447.0206;-857.5739;1.452924;True;False
Node;AmplifyShaderEditor.RangedFloatNode;325;-2896.411,1123.51;Float;False;Property;_Tilling;Tilling;5;0;Create;True;0;0;False;0;10;2.31;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;328;-2662.669,1120.589;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;319;-2447.372,1133.22;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RelayNode;306;-2024.389,1218.93;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-2008.117,899.631;Float;False;InstancedProperty;_Scanline1Amount;Scanline 1 Amount;0;0;Create;True;0;0;False;0;1.56;1.52;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-1629.765,804.4552;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-1750.007,1201.432;Float;False;InstancedProperty;_Scanline1Speed;Scanline 1 Speed;1;0;Create;True;0;0;False;0;1;-0.98;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;285;-1410.049,504.0767;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;43;-1525.411,1175.639;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;289;-1233.771,1142.011;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;257;-1835.097,1416.817;Float;False;InstancedProperty;_PulseDirection;Pulse Direction;4;0;Create;True;0;0;True;0;1;-1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;42;-1203.351,880.6347;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RelayNode;284;-870.5854,1095.685;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;290;-1001.007,1222.535;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;45;-976.3276,884.6759;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareGreater;286;-692.6973,1118.395;Float;True;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;339;-379.1748,1111.906;Float;True;3;0;FLOAT;0;False;1;FLOAT;0.1;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;341;-135.6784,1152.322;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;-0.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;345;55.69116,1398.061;Float;True;2;0;FLOAT;0.5;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;347;199.5305,1168.5;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;59;-459.8393,732.1082;Float;False;InstancedProperty;_Color;Color;3;0;Create;True;0;0;False;0;0.6179246,1,1,1;1,1,1,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;342;427.8447,1189.083;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;206;-338.0384,1384.917;Float;False;InstancedProperty;_PulseColor;Pulse Color;2;1;[HDR];Create;True;0;0;False;0;2.680933,2.79544,0,1;83.32378,88.59545,134.8446,0.7490196;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;166.0509,888.7458;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;331;638.2662,1239.958;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;330;864.1119,1006.294;Float;False;Property;_EmissiveValue;EmissiveValue;6;0;Create;True;0;0;False;0;1;0.02;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;334;672.5659,1656.546;Float;False;Property;_OpacityExp;OpacityExp;7;0;Create;True;0;0;False;0;1;6.91;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;209;504.6184,816.1826;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;329;1255.506,844.3374;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;343;913.3564,1469.271;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1985.271,977.3984;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;SequenceShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;2;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;328;0;325;0
WireConnection;328;1;325;0
WireConnection;319;0;328;0
WireConnection;306;0;319;1
WireConnection;40;0;306;0
WireConnection;40;1;41;0
WireConnection;285;0;40;0
WireConnection;43;0;44;0
WireConnection;289;0;285;0
WireConnection;289;1;43;0
WireConnection;42;0;40;0
WireConnection;42;1;43;0
WireConnection;284;0;257;0
WireConnection;290;0;289;0
WireConnection;45;0;42;0
WireConnection;286;0;284;0
WireConnection;286;2;290;0
WireConnection;286;3;45;0
WireConnection;339;0;286;0
WireConnection;341;0;339;0
WireConnection;345;1;341;0
WireConnection;347;0;341;0
WireConnection;347;1;345;0
WireConnection;342;0;347;0
WireConnection;58;0;59;0
WireConnection;58;1;342;0
WireConnection;331;1;342;0
WireConnection;209;0;58;0
WireConnection;209;1;206;0
WireConnection;329;0;209;0
WireConnection;329;1;330;0
WireConnection;343;0;331;0
WireConnection;343;1;334;0
WireConnection;0;2;329;0
WireConnection;0;9;343;0
ASEEND*/
//CHKSM=3521525917248317D1AC61C8E1788EC3529625E2