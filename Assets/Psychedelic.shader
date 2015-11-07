Shader "Hidden/Bloom"
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
			#pragma debug
			
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
				fixed4 col = tex2D(_MainTex, i.uv);
				float t = _Time.x * 20;
				float v = sin(_Time.w);
				half4 m;
				m.r = (1 + sin(v * t + 1 + i.uv.x)) / .6 * col.r;
				m.g = (1 - cos(t * 2 + 2 + i.uv.y)) / v * .8 * col.g; 
				m.b = (1 + sin(t * 3 + 3 + i.uv.x + (v * i.uv.y))) / 2 * col.b;
				m.a = col.a;
				return col / 2.0 + m / 2.0;
			}
			ENDCG
		}
	}
}
