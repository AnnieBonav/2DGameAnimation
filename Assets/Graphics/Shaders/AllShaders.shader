Shader "Unlit/AllShaders"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {} // Show White
        _ColorA("Color A", Color) = (255,1,1,1)
        _ColorB("Color B", Color) = (1,1,1,1)
        _Scale("UV Scale", Float) = 1
        _Offset("UV Offset", Float) = 0
        _ColorStart("Color Start", Range(0,1)) = 1
        _ColorEnd("Color Start", Range(0,1)) = 1

            //_Value("Value", Float) = 1.0;
    }
        SubShader
        {
            // Tags { "RenderType" = "Transparent" }
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                float _Value; // Need to mentione it here

                sampler2D _MainTex;
                float4 _MainTex_ST; // Optional?
                float4 _ColorA;
                float4 _ColorB;
                float _Scale;
                float _Offset;

                struct MeshData // was app data, per-vertex mesh data
                {
                    float4 vertex : POSITION; // vertex position
                    float3 normals: NORMAL;
                    // float3 tangent: TANGENT;
                    // float4 color: COLOR;
                    float2 uv : TEXCOORD0; // uv0 diffuse/normal map textures // was uv before
                    // float2 uv1 : TEXCOORD1; // uv1 coordinates lightmap coordniates
                    float3 worldPos : TEXCOORD1;
                };

                struct Interpolators // was v2f
                {
                    float4 vertex : SV_POSITION; // CLip space position
                    float2 uv : TEXCOORD0;
                    float3 worldPos : TEXCOORD1;
                    float3 normal : TEXTCOORD2;
                };

                Interpolators vert(MeshData v) // Mesh data was appdata
                {
                    Interpolators o;
                    o.worldPos = mul(UNITY_MATRIX_M, v.vertex), float4(v.vertex.xyz, 1);
                    o.vertex = UnityObjectToClipPos(v.vertex); // Local space to clip space
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.uv = v.uv; // (v.uv + _Offset) * _Scale; // Makes it go from weird - half to a cool image
                    o.normal = UnityObjectToWorldNormal(v.normals);
                    // o.uv = v.uv; // shows text

                    // o.uv.x += _Time.y * 0.1; moves

                    // o.vertex = v.vertex; // WOuld stay in the screen
                    // o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                // float 32 bit
                // hald 16 bit
                // fixed lower precision -1 to 1
                // float4 -> half4 -> 
                // float4x4 -> half4x4 (C# Matrix 4x4)

                float4 frag(Interpolators i) : SV_Target
                {
                    // lerp between two colours based on x UV coordinate
                    // return(_ColorA);
                    float4 outColour = lerp(_ColorA, _ColorB, i.uv.x);
                    return outColour;
                    return float4(i.uv.xxx, 1); // BNW
                    return float4(i.uv,0,1);
                    return float4(i.uv, 0, 1);
                    return float4(i.worldPos.xyz, 1);
                    // return float4(255,0,0,1);
                    // return float4(1,0,0,1); // red
                    float4 col = tex2D(_MainTex, i.uv);
                    return col;
                    //if (all(col == float3(0,0,0)) {
                        //discard;
                    //}
                }
                ENDCG
            }
        }
}
