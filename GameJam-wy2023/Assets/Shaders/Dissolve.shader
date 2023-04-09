Shader "Dye/Dissolve"
{
    Properties
    {
        [Header(Base)]
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _Specular("Specular", Color) = (1,1,1,1)
        _Gloss("Gloss", Range(8, 256)) = 20

        [Header(Dissolve)]
        _DissolveTexture("Dissolve Texture", 2D) = "white"{}//溶解贴图
        _DissolveBorderColor("Dissolve Border Color", Color) = (1,1,1,1)//溶解边缘颜色
        _DissolveBorderWidth("Dissolve Border Width", float) = 0.05//溶解边缘宽度
        _Percent("Percent", Range(0, 1)) = 0

        [Header(Particle)]
        _Direction("Direction", Vector) = (0, 0, 0, 0)
        _FlowMap("Flow (RG)", 2D) = "balck"{}
        _Expand("Expand", float) = 1
        _ParticleSahpe("Shape", 2D) = "white"{}
        _ParticleRadius("Radius", float) = 1
        [Toggle]_ParticleLit("Use Lit", int) = 1
        [HDR]_ParticleColor("Particle Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Pass
        {
            Tags{"LightMode"="ForwardBase"}
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geo
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed4 _Specular;
            float _Gloss;

            sampler2D _DissolveTexture;
            fixed4 _DissolveBorderColor;
            float _DissolveBorderWidth;
            float _Percent;

            sampler2D _FlowMap;
            float4 _FlowMap_ST;
            float _Expand;
            float4 _Direction;
            sampler2D _ParticleSahpe;
            float _ParticleRadius;
            float4 _ParticleColor;
            int _ParticleLit;

            struct appdata{
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };
            struct v2g{
                float4 pos : SV_POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };
            struct g2f{
                float4 pos : SV_POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float4 worldPos : TEXCOORD1;
            };
            float rand(float2 uv){
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453123);
            }
            float remap(float value, float fromL, float fromR, float toL, float toR){
                return (value - fromL)/(fromR - fromL) * (toR - toL) + toL;
            }
            float4 remapFlowTexture(float4 tex){
                return float4(
                    remap(tex.x, 0, 1, -1, 1),
                    remap(tex.y, 0, 1, -1, 1),
                    0,
                    remap(tex.w, 0, 1, -1, 1)
                );
            }
            //顶点着色器
            v2g vert(appdata v){
                v2g o;
                o.pos = v.vertex;
                o.normal = v.normal;
                o.uv = v.uv;
                return o;
            }
            [maxvertexcount(7)]//三角面 + 粒子的quad
            void geo(triangle v2g IN[3], inout TriangleStream<g2f> triStream){
                float3 avgPos = (IN[0].pos + IN[1].pos + IN[2].pos) / 3;
                float3 avgNormal = (IN[0].pos + IN[1].pos + IN[2].pos) / 3;
                float2 avgUV = (IN[0].uv + IN[1].uv + IN[2].uv) / 3;

                float dissolve_value = tex2Dlod(_DissolveTexture, float4(avgUV,0,0)).r;
                float t = clamp(_Percent * 2 - dissolve_value, 0, 1);

                float2 flowUV = TRANSFORM_TEX(mul(unity_ObjectToWorld, avgPos).xz, _FlowMap);
                float4 flowVector = remapFlowTexture(tex2Dlod(_FlowMap, float4(flowUV,0,0)));
                float3 pseudoRandomPos = (avgPos) + UnityWorldToObjectDir(_Direction);
                pseudoRandomPos += (flowVector.xyz * _Expand);

                float3 p = lerp(avgPos, pseudoRandomPos, t);
                float radius = lerp(_ParticleRadius, 0, t);

                if(t > 0){
                    //创建一个quad

                    float3 right = UNITY_MATRIX_IT_MV[0].xyz;
                    float3 up = UNITY_MATRIX_IT_MV[1].xyz;

                    float4 v[4];
                    v[0] = float4(p + radius * right - radius * up, 1.0f);
                    v[1] = float4(p + radius * right + radius * up, 1.0f);
                    v[2] = float4(p - radius * right - radius * up, 1.0f);
                    v[3] = float4(p - radius * right + radius * up, 1.0f);

                    g2f vert;
                    vert.pos = UnityObjectToClipPos(v[0]);
                    vert.uv = float2(1.0f, 0.0f);
                    vert.normal = UnityObjectToWorldNormal(avgNormal);
                    vert.worldPos = mul(unity_ObjectToWorld, v[0]);
                    triStream.Append(vert);

                    vert.pos = UnityObjectToClipPos(v[1]);
                    vert.uv = float2(1.0f, 1.0f);
                    vert.normal = UnityObjectToWorldNormal(avgNormal);
                    vert.worldPos = mul(unity_ObjectToWorld, v[1]);
                    triStream.Append(vert);

                    vert.pos = UnityObjectToClipPos(v[2]);
                    vert.uv = float2(0.0f, 0.0f);
                    vert.normal = UnityObjectToWorldNormal(avgNormal);
                    vert.worldPos = mul(unity_ObjectToWorld, v[2]);
                    triStream.Append(vert);

                    vert.pos = UnityObjectToClipPos(v[3]);
                    vert.uv = float2(0.0f, 1.0f);
                    vert.normal = UnityObjectToWorldNormal(avgNormal);
                    vert.worldPos = mul(unity_ObjectToWorld, v[3]);
                    triStream.Append(vert);

                    triStream.RestartStrip();
                }

                for(int j=0;j<3;j++){
                    g2f o;
                    o.pos = UnityObjectToClipPos(IN[j].pos);
                    o.uv = TRANSFORM_TEX(IN[j].uv, _MainTex);
                    o.normal = UnityObjectToWorldNormal(IN[j].normal);
                    o.worldPos = float4(mul(unity_ObjectToWorld, IN[j].pos).xyz, -1);
                    triStream.Append(o);
                }

                triStream.RestartStrip();
            }

            fixed4 frag(g2f i) : SV_TARGET{
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb;
                fixed3 lightDir = normalize(UnityWorldSpaceLightDir(i.worldPos.xyz));
                fixed3 diffuse = _LightColor0.rgb * saturate(dot(i.normal, lightDir));
                fixed3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos.xyz));
                fixed3 halfDir = normalize(viewDir + lightDir);
                fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(halfDir, i.normal)), _Gloss);

                fixed4 col = 1;

                if(i.worldPos.w < 0){
                    float dissolve = tex2D(_DissolveTexture, i.uv).r;
                    clip(dissolve - 2 * _Percent);

                    col = tex2D(_MainTex, i.uv) * _Color;
                    col.rgb = ambient * col.rgb + diffuse*col.rgb + specular;
                    if(_Percent >= 0){
                        col += _DissolveBorderColor * step(dissolve - 2 * _Percent, _DissolveBorderWidth);
                    }
                }
                else{
                    col.rgb = lerp(_ParticleColor.rgb, ambient * _ParticleColor.rgb + diffuse * _ParticleColor + specular, _ParticleLit);
                    float s = tex2D(_ParticleSahpe, i.uv).r;
                    clip(s - 0.5);
                }
                return col;
            }
            ENDCG
        }
    }
}
