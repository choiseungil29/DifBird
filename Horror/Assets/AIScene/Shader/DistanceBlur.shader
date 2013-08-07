// DistanceBlurShader for Texture - not very useful.. - Unity version : mgear - http://unitycoder.com/blog
// Original shader: Field shaders by Google
/*
* Copyright 2009, Google Inc. All rights reserved.
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions are met:
*     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
*     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
*     * Neither the name of Google Inc. nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
* THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
* "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
* LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
* A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
* OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
* SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
* LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
* DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
* THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
* OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
Shader "mShaders/DistanceBlur1" {
	Properties {
		blurSizeX("BlurSizeX", Float) = 0
		blurSizeY("BlurSizeY", Float) = 0
		_MainTex ("Texture", 2D) = "white" { }		 
	}
	
	SubShader {
		Pass {
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				float blurSizeX;
				float blurSizeY;
				sampler2D _MainTex;
				struct v2f {
				float4  pos : SV_POSITION;
				float2  uv : TEXCOORD0;
				float2 depth : TEXCOORD1;
			};
			float4 _MainTex_ST;
			v2f vert (appdata_base v)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				UNITY_TRANSFER_DEPTH(o.depth);
				o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
				return o;
			}
			half4 frag (v2f i) : COLOR
			{
				half4 sum = half4(0.0);
				float depth = 1-i.depth.r*0.0005;
				// gather pixel color from neighbours, distance to look for depends on camera distance..
				sum += tex2D(_MainTex, float2(i.uv.x - 5.0 * depth, i.uv.y - 5.0 * depth)) * 0.025;
				sum += tex2D(_MainTex, float2(i.uv.x - 4.0 * depth, i.uv.y - 4.0 * depth)) * 0.05;
				sum += tex2D(_MainTex, float2(i.uv.x - 3.0 * depth, i.uv.y - 3.0 * depth)) * 0.09;
				sum += tex2D(_MainTex, float2(i.uv.x - 2.0 * depth, i.uv.y - 2.0 * depth)) * 0.12;
				sum += tex2D(_MainTex, float2(i.uv.x - 1.0 * depth, i.uv.y - 1.0 * depth)) * 0.15;
				sum += tex2D(_MainTex, float2(i.uv.x, i.uv.y)) * 0.16;
				sum += tex2D(_MainTex, float2(i.uv.x + 1.0 * depth, i.uv.y + 1.0 * depth)) * 0.15;
				sum += tex2D(_MainTex, float2(i.uv.x + 2.0 * depth, i.uv.y + 2.0 * depth)) * 0.12;
				sum += tex2D(_MainTex, float2(i.uv.x + 3.0 * depth, i.uv.y + 3.0 * depth)) * 0.09;
				sum += tex2D(_MainTex, float2(i.uv.x + 4.0 * depth, i.uv.y + 4.0 * depth)) * 0.05;
				sum += tex2D(_MainTex, float2(i.uv.x + 5.0 * depth, i.uv.y + 5.0 * depth)) * 0.025;
				return sum;
			}
			ENDCG
		}
	}
	Fallback "VertexLit"
}