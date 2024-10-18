// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see UnityShaderLicense.txt)
// Modified by Insane Systems

Shader "InsaneSystems/UI/Gradient"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}

		[Header(Default parameters)]
        _Color ("Tint", Color) = (1,1,1,1)

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0

		[Header(Gradient parameters)]
		[KeywordEnum(Normal, Multiply, Add, Overlay)] _BlendMode("Blend Mode", Range(0, 1)) = 0
		_BlendStrength("Blend Strength", Range(0, 1)) = 1
		_GradientTopColor ("Gradient Top Color", Color) = (1, 0.95,0.9,1)
		_GradientBotColor ("Gradient Bottom Color", Color) = (1,0.8,0.8,1)
		_Distribution ("Distribution", Range(0, 1)) = 0.5
		[Toggle] _Invert ("Invert", Float) = 0
		[Toggle] _IsHorizontal ("Make horizontal", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0 // was 2.0 before

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile __ UNITY_UI_CLIP_RECT
            #pragma multi_compile __ UNITY_UI_ALPHACLIP
            #pragma multi_compile _BLENDMODE_NORMAL _BLENDMODE_MULTIPLY _BLENDMODE_ADD _BLENDMODE_OVERLAY
            
            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                OUT.color = v.color * _Color;
                return OUT;
            }

			float _Invert;
			float _IsHorizontal;
			float _Distribution;
			float _BlendStrength;
			fixed4 _GradientTopColor;
			fixed4 _GradientBotColor;
			
            float Overlay(float base, float top)
            {
                 if (base < 0.5)
                      return 2 * top * base;
                 else 
                      return 1 - 2 * (1 - base) * (1 - top);
            }
            
			fixed4 ApplyEffects(fixed4 inputColor, v2f IN)
			{
				fixed4 currentPositionColor = fixed4(1, 1, 1, 1);
				float lerpCoord = IN.texcoord.y;

				if (_IsHorizontal > 0)
					lerpCoord = IN.texcoord.x;

				if (_Invert <= 0)
					currentPositionColor = lerp(_GradientBotColor, _GradientTopColor, clamp(lerpCoord + (_Distribution - 0.5) * 2, 0, 1));
				else
					currentPositionColor = lerp(_GradientTopColor, _GradientBotColor, clamp(lerpCoord + (_Distribution - 0.5) * 2, 0, 1));
				
				fixed4 resultColor = inputColor;
				
				#if (_BLENDMODE_NORMAL)
				    resultColor = lerp(inputColor, currentPositionColor, currentPositionColor.a * inputColor.a * _BlendStrength);
                #endif
                
                #if (_BLENDMODE_MULTIPLY)
                    resultColor.r = inputColor.r * lerp(1, currentPositionColor.r, _BlendStrength);
                    resultColor.g = inputColor.g * lerp(1, currentPositionColor.g, _BlendStrength);
                    resultColor.b = inputColor.b * lerp(1, currentPositionColor.b, _BlendStrength);
                    resultColor.a = currentPositionColor.a * inputColor.a;
                #endif
                
                #if (_BLENDMODE_ADD)
                    resultColor.r = inputColor.r + lerp(0, currentPositionColor.r, _BlendStrength);
                    resultColor.g = inputColor.g + lerp(0, currentPositionColor.g, _BlendStrength);
                    resultColor.b = inputColor.b + lerp(0, currentPositionColor.b, _BlendStrength);
                    resultColor.a = currentPositionColor.a * inputColor.a;
                #endif
   
                #if (_BLENDMODE_OVERLAY)
                    resultColor.r = lerp(inputColor.r, Overlay(inputColor.r, currentPositionColor.r), _BlendStrength);
                    resultColor.g = lerp(inputColor.g, Overlay(inputColor.g, currentPositionColor.g), _BlendStrength);
                    resultColor.b = lerp(inputColor.b, Overlay(inputColor.b, currentPositionColor.b), _BlendStrength);
                    resultColor.a = currentPositionColor.a * inputColor.a;
                #endif
                
				return resultColor;
			}
		
            fixed4 frag(v2f IN) : SV_Target
            {
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                return ApplyEffects(color, IN);
            }
        ENDCG
        }
    }
}
