// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/CameraOverride"
{
	Properties
   {
      _MainTex ("Source", 2D) = "white" {}
      _Override ("OverrideSource", 2D) = "white" {}
      _Color ("Tint", Color) = (1,1,1,1)
   }
   SubShader
   {
      Cull Off 
      ZWrite Off 
      ZTest Always

      Pass
      {
         CGPROGRAM
         #pragma vertex vertexShader
         #pragma fragment fragmentShader
			
         #include "UnityCG.cginc"

         struct vertexInput
         {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
         };

         struct vertexOutput
         {
            float2 texcoord : TEXCOORD0;
            float4 position : SV_POSITION;
         };

         vertexOutput vertexShader(vertexInput i)
         {
            vertexOutput o;
            o.position = UnityObjectToClipPos(i.vertex);
            o.texcoord = i.texcoord;
            return o;
         }
			
         sampler2D _MainTex;
         float4 _MainTex_ST;
         sampler2D _Override;
         float4 _Override_ST;
         float4 _Color;

         float4 fragmentShader(vertexOutput i) : COLOR
         {
            float4 color;
            color = tex2D(_Override, 
            UnityStereoScreenSpaceUVAdjust(
            i.texcoord, _Override_ST));		

            return color * _Color;
         }
         ENDCG
      }
   }
   Fallback Off
}
