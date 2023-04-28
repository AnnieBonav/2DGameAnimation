// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/StandardSurfaceShader1"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB))", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.0
        _Metallic("Metallic", Range(0,1)) = 0.0
        _Offset("Vertex Offset", float) = 0.0
        _Skew("Skew", Vector) = (0,0,0,0)
        _Rotation("Rotation", Vector) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "DisableBatching" = "true"}
        LOD 200

        CGPROGRAM

        #pragma surface surf BlinnPhong // vertex:vert addShadow fullforwardshadows nolightmap
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Offset;
        float4 _Skew;
        float4 _Rotation;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void vert(inout appdata_full v)
        {
            float modifier = v.vertex.y - _Offset;
            float3 modification = float3(0, 0, 0);
            modification += _Skew.xyx * modifier;

            float3x3 test;
            test[0][0];
            v.vertex.xyz += modification;
        }

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }

        ENDCG
    }
}
