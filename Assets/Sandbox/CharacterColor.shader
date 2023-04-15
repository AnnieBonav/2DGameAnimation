Shader "Unlit/CharacterColor"
{
    Properties // input data
    {
        _Color("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            float4 _Color;

            struct MeshData {
                float4 vertex : POSITION; // per-vertex position
                float3 normals : NORMAL;
                //float4 color: COLOR;
                float4 uv0 : TEXCOORD0; // uv0 diffuse/normal map textures
                //float4 uv1 : TEXTCOORD1; //uv1 coordinates lightmap coordinates
            };

            struct Interpolators
            {
                float4 vertex : SV_Position;
                float3 normal : TEXCOORD0; //These TEXT do not matter
                float2 uv : TEXCOORD1; //These TEXT do not matter
            };

            Interpolators vert(MeshData v) {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex); // local space to clip space
                //o.normal = v.normals;
                o.normal = UnityObjectToWorldNormal( v.normals );
                o.uv = v.uv0;
                return o;

            }

            // Fragment shader
            fixed4 frag(Interpolators i) : SV_Target
            {
                //return lerp()
                //return float4(i.uv.xxx, 1);
                return float4(i.uv.x, i.uv.y, 0, 1);
                //return float4(i.uv, 0, 1); //Mango
                return float4(1,1,1,1); //Red
                return _Color;
            }
            ENDCG
        }
    }
}
