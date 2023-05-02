Shader "Unlit/Grass"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // Show White
        _TimeChange("Time Change", float) = 0
    }
        SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _Value; // Need to mentione it here

            sampler2D _MainTex;
            float4 _MainTex_ST; // Optional?
            float _TimeChange;

           
            struct MeshData // was app data, per-vertex mesh data
            {
                float4 vertex : POSITION; // vertex position
                float3 normals: NORMAL;
                float2 uv : TEXCOORD0; // uv0 diffuse/normal map textures // was uv before
                float3 worldPos : TEXCOORD1;
            };

            struct Interpolators // was v2f
            {
                float4 vertex : SV_POSITION; // CLip space position
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float3 normal : TEXTCOORD2;
            };

            Interpolators vert ( MeshData v ) // Mesh data was appdata
            {
                Interpolators o;
                o.worldPos = mul(UNITY_MATRIX_M, v.vertex), float4(v.vertex.xyz, 1);
                o.vertex = UnityObjectToClipPos(v.vertex); // Local space to clip space
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal(v.normals);
                o.uv.x += _Time.y * _TimeChange;

                return o;
            }

            float4 frag(Interpolators i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
