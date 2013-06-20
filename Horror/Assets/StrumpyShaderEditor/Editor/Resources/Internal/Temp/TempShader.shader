Shader "ShaderEditor/EditorShaderCache"
{
	Properties 
	{
_SimpleSampler("_SimpleSampler", 2D) = "black" {}
_SimpleNormal("_SimpleNormal", 2D) = "black" {}
_SimpleHightlightColor("_SimpleHightlightColor", Color) = (1,1,1,1)
_SimpleGlossPower("_SimpleGlossPower", Range(0,1) ) = 0.012
_EmissionPower("_EmissionPower", Range(0,1) ) = 0
_Emission("_Emission", 2D) = "black" {}
_EmissionColor("_EmissionColor", Color) = (0.2686567,0.2385832,0.2385832,1)
_RimColor("_RimColor", Color) = (1,1,1,1)

	}
	
	SubShader 
	{
		Tags
		{
"Queue"="Transparent"
"IgnoreProjector"="False"
"RenderType"="Transparent"

		}

		
Cull Back
ZWrite On
ZTest LEqual
ColorMask RGBA
Blend SrcAlpha OneMinusSrcAlpha
Fog{
}


		CGPROGRAM
#pragma surface surf BlinnPhongEditor  vertex:vert
#pragma target 2.0


sampler2D _SimpleSampler;
sampler2D _SimpleNormal;
float4 _SimpleHightlightColor;
float _SimpleGlossPower;
float _EmissionPower;
sampler2D _Emission;
float4 _EmissionColor;
float4 _RimColor;

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
				half4 Custom;
			};
			
			inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
			{
half3 spec = light.a * s.Gloss;
half4 c;
c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
c.a = s.Alpha;
return c;

			}

			inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 h = normalize (lightDir + viewDir);
				
				half diff = max (0, dot ( lightDir, s.Normal ));
				
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0);
				
				half4 res;
				res.rgb = _LightColor0.rgb * diff;
				res.w = spec * Luminance (_LightColor0.rgb);
				res *= atten * 2.0;

				return LightingBlinnPhongEditor_PrePass( s, res );
			}
			
			struct Input {
				float2 uv_SimpleSampler;
float2 uv_SimpleNormal;
float2 uv_Emission;
float3 viewDir;

			};

			void vert (inout appdata_full v, out Input o) {
float4 VertexOutputMaster0_0_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_1_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_2_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_3_NoInput = float4(0,0,0,0);


			}
			

			void surf (Input IN, inout EditorSurfaceOutput o) {
				o.Normal = float3(0.0,0.0,1.0);
				o.Alpha = 1.0;
				o.Albedo = 0.0;
				o.Emission = 0.0;
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Custom = 0.0;
				
float4 Tex2D0=tex2D(_SimpleSampler,(IN.uv_SimpleSampler.xyxy).xy);
float4 Tex2DNormal0=float4(UnpackNormal( tex2D(_SimpleNormal,(IN.uv_SimpleNormal.xyxy).xy)).xyz, 1.0 );
float4 Multiply1=_EmissionColor * _EmissionPower.xxxx;
float4 Tex2D1=tex2D(_Emission,(IN.uv_Emission.xyxy).xy);
float4 Multiply0=Multiply1 * Tex2D1.aaaa;
float4 Fresnel0_1_NoInput = float4(0,0,1,1);
float4 Fresnel0=(1.0 - dot( normalize( float4( IN.viewDir.x, IN.viewDir.y,IN.viewDir.z,1.0 ).xyz), normalize( Fresnel0_1_NoInput.xyz ) )).xxxx;
float4 Pow0=pow(Fresnel0,float4( 3,3,3,3 ));
float4 Multiply2=Pow0 * _RimColor;
float4 Add0=Multiply0 + Multiply2;
float4 Master0_5_NoInput = float4(1,1,1,1);
float4 Master0_7_NoInput = float4(0,0,0,0);
float4 Master0_6_NoInput = float4(1,1,1,1);
o.Albedo = Tex2D0;
o.Normal = Tex2DNormal0;
o.Emission = Add0;
o.Specular = _SimpleGlossPower.xxxx;
o.Gloss = _SimpleHightlightColor;

				o.Normal = normalize(o.Normal);
			}
		ENDCG
	}
	Fallback "Diffuse"
}