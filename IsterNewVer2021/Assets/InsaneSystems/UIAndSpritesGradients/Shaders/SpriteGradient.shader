// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see UnityShaderLicense.txt)

Shader "InsaneSystems/SpriteGradient"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0

		[Header(Gradient parameters)]
		[KeywordEnum(Normal, Multiply, Add, Overlay)] _BlendMode("Blend Mode", Range(0, 1)) = 0
		_BlendStrength("Blend Strength", Range(0, 1)) = 1
		_GradientTopColor ("Gradient Top Color", Color) = (1, 0.95, 0.9, 1)
		_GradientBotColor ("Gradient Bottom Color", Color) = (1, 0.8, 0.8, 1)
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

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"
            #pragma multi_compile _BLENDMODE_NORMAL _BLENDMODE_MULTIPLY _BLENDMODE_ADD _BLENDMODE_OVERLAY
            
			float _BlendStrength;
			float _Invert;
			float _IsHorizontal;
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
					currentPositionColor = lerp(_GradientBotColor, _GradientTopColor, lerpCoord);
				else
					currentPositionColor = lerp(_GradientTopColor, _GradientBotColor, lerpCoord);

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
                half4 color = SpriteFrag(IN);

                return ApplyEffects(color, IN);
            }
        ENDCG
        }
    }
}
