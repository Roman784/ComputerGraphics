Shader "Unlit/MyFirstShader"
{
    Properties 
    {
        _Tint ("Tint", Color) = (1, 1, 1, 1)
        _MainTex ("MainTexture", 2D) = "white" {}
        _SecTex ("SecTexture", 2D) = "white" {}
    }

    SubShader 
    {
        Pass 
        {
            CGPROGRAM
                
            #pragma vertex MyVertexProgram
            #pragma fragment MyFragmentProgram

            #include "UnityCG.cginc"

            float4 _Tint;

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _SecTex;
            float4 _SecTex_ST;

            struct Interpolators 
            {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 uv_sec : TEXCOORD1;
            };

            struct VertexData 
            {
                float4 position : POSITION;
                float2 uv : TEXCOORD0;
                float2 uv_sec : TEXCOORD1;
            };

            Interpolators MyVertexProgram (VertexData v)
            {
                Interpolators i;
                i.position = UnityObjectToClipPos(v.position);
                i.uv = TRANSFORM_TEX(v.uv, _MainTex);
                i.uv_sec = TRANSFORM_TEX(v.uv_sec, _SecTex);
                return i;
            }

            float4 MyFragmentProgram (Interpolators i) : SV_TARGET
            {
                 float4 mainTexColor = tex2D(_MainTex, i.uv);
                 float4 secTexColor = tex2D(_SecTex, i.uv_sec);
                 return (mainTexColor + secTexColor) * _Tint;
            }

            ENDCG
        }
    }
}
