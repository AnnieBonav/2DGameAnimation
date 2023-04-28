Shader "Unlit/MoveShader"
{
    Properties
    {
        _MainTex("Base (RGB), Alpha (A)", 2D) = "white" {} // Show White
        _Color("Color", Color) = (1,1,1,1)
        _Offset("Vertex Offset", float) = 0.0
        _Skew("Skew", Vector) = (0,0,0,0)
        _TimeChange("Time", float) = 1
    }
        SubShader
        {
            Tags { "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"}

            Pass
            {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define TAU 6.28318530718 

            float4 _Color;
            float _Offset;
            float4 _Skew;
            float _TimeChange;
            sampler2D _MainTex;
            float4 _MainTex_ST; // Optional?

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;

            };

            Interpolators vert(MeshData v)
            {
                Interpolators o;
                o.uv = v.uv;

                float modifier = v.vertex.y - _Offset;
                float3 modification = float3(0, 0, 0);
                _Skew.x += cos(_Time.z) * 0.1;
                modification += _Skew.xyx * modifier;

                float3x3 test;
                test[0][0];
                v.vertex.xyz += modification;

                o.color = _Color;

                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(Interpolators i) : SV_Target
            {
                float xOffset = cos(i.uv.y * TAU * 8) * 0.01;
                //float t = cos(i.uv.y * TAU * 5) * 0.5 + 0.5;
                //float t = cos((i.uv.x + xOffset + _Time.y) * TAU * 5) * 0.5 + 0.5;
                //float4 col = tex2D(_MainTex, i.uv.xy);// text
                //float other = float(_MainTex.a);
                //col.a = i.color.a;

                if ((i.uv.x <= 0) || (i.uv.y <= 0))
                {
                    half4 colorTransparent = half4(255, 0, 0, 0);
                    return colorTransparent;
                }
                else
                {
                    half4 tex = tex2D(_MainTex, i.uv);
                    tex.a = i.color.a;
                    return tex;
                }
            
                //return(col);
                // float t = cos( (i.uv.x + xOffset) * TAU * 5) * 0.5 + 0.5; // CoolMove
                // return(t); // Coool move
                return(_Color);
            }
        ENDCG
        }
    }
}
