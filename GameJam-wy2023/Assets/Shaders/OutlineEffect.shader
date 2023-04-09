Shader "Dye/OutlineEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EdgesOnly ("EdgesOnly", float) = 0.0
        _EdgeColor("EdgeColor", Color) = (0, 0, 0, 1)
        _BackgroundColor("BackgroundColor", Color) = (1, 1, 1, 1)
        _SampleDistance("SampleDistance", float) = 1.0
        _Sensitivity("Sensitivity", Vector) = (1, 1, 0, 0)
        //_DepthTexture("DepthTexture", 2D) = "white"{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        CGINCLUDE
        #include "UnityCG.cginc"

        sampler2D _MainTex;
        half4 _MainTex_TexelSize;
        float _EdgesOnly;
        float _SampleDistance;
        fixed4 _EdgeColor;
        fixed4 _BackgroundColor;
        half4 _Sensitivity;
        sampler2D _CameraDepthNormalsTexture;
        //sampler2D _DepthTexture;

        struct v2f{
            float4 pos : SV_POSITION;
            half2 uv[5] : TEXCOORD0;
        };

        v2f vert(appdata_img v){
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            half2 uv = v.texcoord;
            o.uv[0] = uv;//主纹理的uv坐标

            #if UNITY_UV_STARTS_AT_TOP
            if(_MainTex_TexelSize.y < 0){
                uv = 1 - uv;
            }
            #endif

            //深度+法线纹理的uv
            //记录四个角的uv坐标，使用Robert算子计算梯度
            o.uv[1] = uv + _MainTex_TexelSize.xy * _SampleDistance * half2(1, 1);
            o.uv[2] = uv + _MainTex_TexelSize.xy * _SampleDistance * half2(-1, -1);
            o.uv[3] = uv + _MainTex_TexelSize.xy * _SampleDistance * half2(-1, 1);
            o.uv[4] = uv + _MainTex_TexelSize.xy * _SampleDistance * half2(1, -1);

            return o;
        }
        half CheckSame(half4 sampleA, half4 sampleB){
            half2 noramlA = sampleA.xy;
            float depthA = DecodeFloatRG(sampleA.zw);
            half2 normalB = sampleB.xy;
            float depthB = DecodeFloatRG(sampleB.zw);

            half2 diffNormal = abs(noramlA - normalB) * _Sensitivity.x;
            int isSameNormal = (diffNormal.x + diffNormal.y) < 0.1;
            float diffDepth = abs(depthA - depthB) * _Sensitivity.y;
            int isSameDepth = diffDepth < 0.1 * depthA;

            //都接近才返回1
            return isSameNormal * isSameDepth ? 1.0 : 0;
        }
        fixed4 frag(v2f i) : SV_TARGET{
            half4 sample1 = tex2D(_CameraDepthNormalsTexture, i.uv[1]);
            half4 sample2 = tex2D(_CameraDepthNormalsTexture, i.uv[2]);
            half4 sample3 = tex2D(_CameraDepthNormalsTexture, i.uv[3]);
            half4 sample4 = tex2D(_CameraDepthNormalsTexture, i.uv[4]);

            half same = 1.0;
            same *= CheckSame(sample1, sample2);
            same *= CheckSame(sample3 ,sample4);
            if(0.5f - same < 0){
                return tex2D(_MainTex, i.uv[0]);
            }
            fixed4 col = lerp(tex2D(_MainTex, i.uv[0]), _BackgroundColor, _EdgesOnly);
            //float d = tex2D(_DepthTexture, i.uv[0]).x;
            col = lerp(_EdgeColor, col, same);
            //return tex2D(_DepthTexture, i.uv[0]);
            // if(d < i.pos.z / i.pos.w){
            //     return fixed4(col.rgb, 0.5);
            // }
            return col;
        }
        ENDCG
        Pass
        {
            //outline
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
}
