Shader "Dye/WinkShader"
{
    Properties
    {
        _Color("Color", color) = (0,0,0,1)
        _MainTex ("Texture", 2D) = "white" {}
        _Width ("Width", float) = 0.5
        _Height ("Height", float) = 0.25
        _BlurWidth ("BlurWidth", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
            struct v2f{
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            half2 _MainTex_TexelSize;
            float _Width;
            float _Height;
            float _BlurWidth;
            fixed4 _Color;

            v2f vert(appdata_img v){
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }
            fixed4 frag(v2f i):SV_TARGET{
                float val = ((i.uv.x - 0.5) * (i.uv.x - 0.5)) / (_Width * _Width) + ((i.uv.y -0.5) * (i.uv.y-0.5)) / (_Height * _Height);
                if(val > 1 + _BlurWidth){
                    return _Color;
                } 
                else if(val > 1){
                    float t = saturate( (val - 1.0f) / _BlurWidth );
                    return lerp(tex2D(_MainTex, i.uv), _Color, t);
                }
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
