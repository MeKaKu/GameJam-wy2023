Shader "Dye/Water"
{
    Properties
    {
        _Color("Color", Color) = (0.5,0.5,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "RenderQueue"="Transparent"}
        Pass
        {
            Cull Off
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            fixed4 _Color;
            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
