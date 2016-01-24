// Upgrade NOTE: replaced 'glstate.matrix.mvp' with 'UNITY_MATRIX_MVP'

Shader "Custom/CharacterShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_LinesTex("Glow Lines", 2D) = "white" {}
		_Shine("Shine", Range(1,50)) = 1.0
	}
	SubShader{
		Pass {
			Tags{ "RenderType" = "Transparent" }
			LOD 200
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			// Use shader model 3.0 target, to get nicer looking lighting
			//	#pragma target 3.0


			// vertex input: position, uv1, uv2
			struct appdata {
				float4 vertex : POSITION;
				float4 texcoord1 : TEXCOORD1;
			};

			struct v2f {
				float4 pos : POSITION;
				float4 texcoord1 : TEXCOORD1;
			};

			v2f vert(appdata v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord1 = v.texcoord1;
				return o;
			}

			float4 _Color;
			sampler2D _LinesTex;
			float _Shine;

			float4 frag(v2f i) : COLOR
			{
				fixed4 c = tex2D(_LinesTex, i.texcoord1);
				float4 outColor = lerp( float4( 0.0,0.0,0.0,0.0 ), _Color, c.a ) * _Shine;
				return outColor;
			}
				ENDCG
		}
	}
	FallBack "Diffuse"
}
