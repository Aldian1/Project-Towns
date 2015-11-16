Shader "RM/RoundLands" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_Power ("Power", Range(0.0,5)) = 0.5
	 _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
      _RimPower ("Rim Power", Range(0.5,2)) = 3.0
}
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 200
	cull off
CGPROGRAM
#pragma surface surf Lambert

sampler2D _MainTex;
fixed4 _Color;
float _Power;
 float4 _RimColor;
      float _RimPower;

struct Input {
	float2 uv_MainTex;
	float2 uv_Illum;
	  float3 viewDir;
};


void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	fixed4 c = tex * _Color;
	o.Albedo = c.rgb;
	o.Emission = c.rgb*_Power;
	o.Alpha = c.a;
	
	 half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
     o.Emission *= _RimColor.rgb * pow (rim, _RimPower);

}
ENDCG
} 
FallBack "Self-Illumin/VertexLit"
}