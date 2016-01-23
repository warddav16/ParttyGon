// Upgrade NOTE: replaced 'glstate.matrix.mvp' with 'UNITY_MATRIX_MVP'

Shader "Custom/CharacterShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_LinesTex("Glow Lines", 2D) = "white" {}
	}
	SubShader{
		Pass {
			Tags{ "RenderType" = "Transparent" }
			LOD 200
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

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
				float4 color : COLOR;
			};

			struct v2f {
				float4 pos : POSITION;
				float4 texcoord1 : TEXCOORD1;
				float4 color : COLOR;
			};

			v2f vert(appdata v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord1 = v.texcoord1;
				o.color = v.color;
				return o;
			}

			float4 _Color;
			float _LineTex;

			float4 frag(v2f i) : COLOR
			{
				fixed4 c = tex2D(_LinesTex, IN.uv_MainTex) * _Color;
				float4 outColor = float4( 0.0,0.0,0.0,0.0 );
				if (i.texcoord1.x < _LineWidth ||
					i.texcoord1.y < _LineWidth)
				{
					outColor =  _Color;
				}

				else if ((i.texcoord1.x - i.texcoord1.y) < _LineWidth &&
					(i.texcoord1.y - i.texcoord1.x) < _LineWidth)
				{
					outColor = _Color;
				}
				else
				{
					outColor = float4(0.0, 0.0, 0.0, 0.0);
				}

				outColor.a = any(outColor.xyz > float3(0, 0, 0));

				return outColor;
			}
				ENDCG
		}
	}
	FallBack "Diffuse"
}
