Shader "Unlit/BlackLight"
{
    Properties
    {
        [NoScaleOffset] _MainTex("Texture", 2D) = "red" {}
        _Position("Position",Vector) = (0,0,0) //プレイヤー座標
        _Judge("Judge",Vector) = (0,0,0) //Judgeの座標
        _MaxAngle("MaxAngle",float) = 20 //最大角度
    }
        SubShader
        {
            Tags
            {
                "RenderType" = "Opaque"
            }

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                float _Radius;
                float3 _Judge;
                float3 _Position;
                float _MaxAngle;

                sampler2D _MainTex;

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float3 worldPos : TEXCOORD1;
                    float2 uv : TEXCOORD0;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);

                    float3 lightVector = _Judge - _Position; //プレイヤーからJudgeへのベクトル
                    float3 touchVector = i.worldPos - _Position; //プレイヤーから描画ピクセルへのベクトル

                    //それぞれのベクトルの長さを計算
                    float lengthA = length(lightVector);
                    float lengthB = length(touchVector);

                    float dotProduct = dot(lightVector, touchVector); //ベクトルの内積を計算
                    float rad = acos(dotProduct / (lengthA * lengthB)); //内積からベクトルの間の角度を計算(ラジアン)

                    float angle = degrees(rad); //ラジアン→度に変換

                    float threshold_angle = _MaxAngle - angle; //最大角度から現在の描画ピクセルの角度を引く(最大を超えるとマイナスになる)
                    float threshold_length = lengthA - lengthB; //ジャッジまでのベクトルの長さから描画ピクセルまでのベクトル長さを引く(Judgeの場所を超えるとマイナスになる)

                    //両方のしきい値が0を超えてたら(範囲に収まっていたら)1を返す
                    float v = threshold_angle >= 0 && threshold_length >= 0 ? 1 : -1;

                    //0以上だったらマスクする
                    clip(v);

                    return col;
                }
                ENDCG
            }
        }
}