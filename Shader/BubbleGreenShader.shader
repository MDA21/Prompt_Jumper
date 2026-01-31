Shader "Custom/CartoonBubbleShader"
{
    Properties
    {
        [MainTexture] _MainTex ("Sprite Texture", 2D) = "white" {}
        _BaseColor ("基础绿色", Color) = (0.7, 0.9, 0.6, 0.9)
        _StrokeColor ("描边颜色", Color) = (0.2, 0.5, 0.3, 1.0)
        _StrokeWidth ("描边宽度", Range(0.001, 0.01)) = 0.003
        _HighlightColor ("高光颜色", Color) = (1, 1, 1, 0.6)
        _HighlightOffset ("高光位置偏移", Vector) = (0.1, 0.1, 0, 0)
        _HighlightWidth ("高光宽度", Range(0.1, 0.5)) = 0.3
        _BreathSpeed ("呼吸速度", Range(0.1, 2.0)) = 0.4
        _BreathRange ("呼吸幅度", Range(0.02, 0.1)) = 0.05
        _AlphaCutoff ("透明度裁剪", Range(0, 1)) = 0.1
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "Sprite"="True"
            "PreviewType"="Plane"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            float4 _BaseColor;
            float4 _StrokeColor;
            float _StrokeWidth;
            float4 _HighlightColor;
            float2 _HighlightOffset;
            float _HighlightWidth;
            float _BreathSpeed;
            float _BreathRange;
            float _AlphaCutoff;

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
                float4 color        : COLOR;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float2 uv           : TEXCOORD0;
                float4 color        : COLOR;
                float breathValue   : TEXCOORD2;
            };

            Varyings vert(Attributes input)
            {
                Varyings output;
                
                float breath = sin(_Time.y * _BreathSpeed) * 0.5 + 0.5;
                output.breathValue = breath;
                

                output.positionHCS = UnityObjectToClipPos(input.positionOS);
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                output.color = input.color;
                
                return output;
            }
            
            float GetEdge(sampler2D tex, float2 uv, float width)
            {

                float2 texelSize = float2(1.0 / _MainTex_TexelSize.x, 1.0 / _MainTex_TexelSize.y);
                float2 offset = texelSize * width * 100; // 放大系数适配宽度
                

                float a1 = tex2D(tex, uv + offset).a;
                float a2 = tex2D(tex, uv - offset).a;
                float a3 = tex2D(tex, uv + float2(offset.x, -offset.y)).a;
                float a4 = tex2D(tex, uv + float2(-offset.x, offset.y)).a;
                
                float avgAlpha = (a1 + a2 + a3 + a4) / 4;
                float currentAlpha = tex2D(tex, uv).a;
                return step(0.1, abs(avgAlpha - currentAlpha)); // 硬边判断，贴合卡通风格
            }

            fixed4 frag(Varyings input) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, input.uv) * input.color * _BaseColor;
                float originalAlpha = col.a;
                

                float edge = GetEdge(_MainTex, input.uv, _StrokeWidth);
                col.rgb = lerp(col.rgb, _StrokeColor.rgb, edge * _StrokeColor.a);
                

                float2 highlightUV = input.uv - _HighlightOffset;
                float highlight = smoothstep(_HighlightWidth, 0, length(highlightUV)) * smoothstep(0, _HighlightWidth, highlightUV.y);
                col.rgb += highlight * _HighlightColor.rgb * _HighlightColor.a;
                

                float2 centerUV = input.uv - 0.5;
                float distanceFromCenter = length(centerUV);
                col.rgb *= 1 - smoothstep(0.3, 0.8, distanceFromCenter) * 0.1;
                

                col.a = originalAlpha * ((1 - _BreathRange) + input.breathValue * _BreathRange);
                
                clip(col.a - _AlphaCutoff);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Sprites/Default"
}