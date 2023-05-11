Shader "Unlit/InterpolationMultipleColors"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Offset("Offset", float) = 1
        _Scale("Scale", float) = 1
        _ColorA("Color A", Color) = (1,1,1,1)
        _ColorB("Color B", Color) = (1,1,1,1)
        _ColorC("Color C", Color) = (1,1,1,1)
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                float4 _ColorA;
                float4 _ColorB;
                float4 _ColorC;
                sampler2D _MainTex;
                float4 _MainTex_ST;
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
                    return o;
                }

                fixed4 frag(Interpolators i) : SV_Target
                {
                    float4 outColor = lerp(_ColorA, _ColorB, i.uv.x);
                    return (outColor);
                    return float4(i.uv.xxx, 1);
                    fixed4 col = tex2D(_MainTex, i.uv);
                    return col;
                }
                ENDCG
            }
        }
}
