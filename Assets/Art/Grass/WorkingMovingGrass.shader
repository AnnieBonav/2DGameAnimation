Shader "Unlit/WorkingMovingGrass"
{
    Properties
    {
        _MainTex("Base (RGB), Alpha (A)", 2D) = "white" {} // Show White
        _Offset("Vertex Offset", float) = 0.0
        _Skew("Skew", Vector) = (0,0,0,0)
        _TimeChange("Time", float) = 1
    }
        SubShader
        {
            Tags { "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Opaque"}

            Pass
            {
                Blend SrcAlpha One

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                #define TAU 6.28318530718 

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
                };

                Interpolators vert(MeshData v)
                {
                    Interpolators o;
                    o.uv = v.uv;
                    o.vertex = UnityObjectToClipPos(v.vertex);

                    float modifier = v.vertex.y - _Offset;
                    float3 modification = float3(0, 0, 0);
                    _Skew.x += cos(_Time.z) * 0.1;
                    modification += _Skew.xyx * modifier;

                    v.vertex.xyz += modification;

                    o.vertex = UnityObjectToClipPos(v.vertex);
                    return o;
                }

                fixed4 frag(Interpolators i) : SV_Target
                {
                    float xOffset = cos(i.uv.y * TAU * 8) * 0.01;
                    float4 tex = tex2D(_MainTex, i.uv);
                    float clamped = clamp(i.uv, 0.5, 0.9);
                    float4 colorWithAlpha = float4(tex.x, tex.y, tex.z, 0.5);
                    return float4(1 - tex);
                    // return float4(tex.xyz, (1-tex.a)); // Werid
                    return float4(i.uv.xy,1, 0.1);
                    return colorWithAlpha;
                    return clamped;
                    //return(col);
                    // float t = cos( (i.uv.x + xOffset) * TAU * 5) * 0.5 + 0.5; // CoolMove
                    // return(t); // Coool move
                }
            ENDCG
        }
        }
}