Shader "Custom/ColorSwapShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TargetColor ("Target Color", Color) = (1, 0, 0, 1) // Default to red
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            Fog { Mode Off }
            Offset 0, -1

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _TargetColor;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed gray = dot(texColor.rgb, float3(0.65, 0.65, 0.65));
                fixed4 outputColor = gray * _TargetColor;
                outputColor.a = texColor.a; // Preserve the original alpha
                return outputColor;
            }
            ENDCG
        }
    }
}
