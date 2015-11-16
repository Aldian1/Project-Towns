 Shader "RM/RoundLands/Snow" {
    Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert
      struct Input {
          float4 screenPos;
      };
    fixed4 _Color;
      void surf (Input IN, inout SurfaceOutput o) {
        //  o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
          float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
          screenUV *= float2(8,6);
          o.Emission = _Color, screenUV* 2;
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }