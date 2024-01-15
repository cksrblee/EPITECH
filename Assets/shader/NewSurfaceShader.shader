Shader "Custom/ChromaKeyShader" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _KeyColor ("Key Color", Color) = (0,1,0,1) // 녹색을 기본값으로 설정
        _Threshold ("Threshold", Range(0,1)) = 0.1
        _Smoothness ("Smoothness", Range(0,1)) = 0.1
    }
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        AlphaTest Greater 0.1
        Fog { Mode Off }

        CGPROGRAM
        #pragma surface surf Lambert alpha:fade

        sampler2D _MainTex;
        fixed4 _KeyColor;
        float _Threshold;
        float _Smoothness;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            float chromaDist = distance(c.rgb, _KeyColor.rgb);
            o.Alpha = smoothstep(_Threshold - _Smoothness, _Threshold + _Smoothness, chromaDist);
            o.Albedo = c.rgb;
            o.Alpha = 1.0 - o.Alpha;
        }
        ENDCG
    }
    FallBack "Transparent/Diffuse"
}