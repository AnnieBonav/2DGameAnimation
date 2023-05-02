Shader "Unlit/InverseLerpColors"
{
    Properties
    {
        _ColorA("Color A", Color) = (1,1,1,1)
        _ColorB("Color B", Color) = (1,1,1,1)

        _ColorStart("Color Start", Range(0,1)) = 1
        _ColorEnd("Color End", Range(0,1)) = 0

    }
        SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _ColorA;
            float4 _ColorB;

            float _ColorStart;
            float _ColorEnd;

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            Interpolators vert(MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float InverseLerp(float a, float b, float v) {
                return (v - a) / (b - a);
            }

            fixed4 frag(Interpolators i) : SV_Target
            {
                // float t = saturate( InverseLerp( _ColorStart, _ColorEnd, i.uv.x ) );
                // float t = InverseLerp(_ColorStart, _ColorEnd, i.uv.x);
                // float t = i.uv.x; // Clamped
                float t = frac(i.uv.x * 5);
                // t = frac(t);
                return t;
                float4 outColor = lerp(_ColorA, _ColorB, t);
                return outColor;
            }
            ENDCG
        }
    }
}
