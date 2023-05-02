Shader "Unlit/UVRange"
{
    Properties
    {
        _Offset("Offset", float) = 1
        _Scale("Scale", float) = 1
    }
        SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _Offset;
            float _Scale;

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
                o.uv = (v.uv + _Offset) * _Scale;
                // o.uv = v.uv;
                return o;
            }

            fixed4 frag(Interpolators i) : SV_Target
            {
                float4 col = float4(i.uv.x, i.uv.y, 0, 1);
                return col;
            }
            ENDCG
        }
    }
}
