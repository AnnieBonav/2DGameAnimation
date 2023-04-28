Shader "Unlit/MoveShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _Offset("Vertex Offset", float) = 0.0
        _Skew("Skew", Vector) = (0,0,0,0)
        _Time("Time", float) = 1
    }
        SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _Color;
            float _Offset;
            float4 _Skew;

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

                float modifier = v.vertex.y - _Offset;
                float3 modification = float3(0, 0, 0);
                _Skew.x += cos(_Time);
                modification += _Skew.xyx * modifier;

                float3x3 test;
                test[0][0];
                v.vertex.xyz += modification;

                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(Interpolators i) : SV_Target
            {
                return(_Color);
            }
            ENDCG
        }
    }
}
