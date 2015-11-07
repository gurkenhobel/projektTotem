﻿Shader "Hidden/Wobbly"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 v = float2(i.uv.x
					+ sin(_Time.w * 4) / _ScreenParams.x * 10
					- cos(_Time.z * 3) / _ScreenParams.x * 20,
					i.uv.y
					+ cos(_Time.w * 4) / _ScreenParams.y * 10
					- sin(_Time.z * 3) / _ScreenParams.y * 20);
				fixed4 col = tex2D(_MainTex, v);
				return col;
			}
			ENDCG
		}
	}
}