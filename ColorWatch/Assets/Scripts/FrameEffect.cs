using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FrameEffect : MonoBehaviour
{
    [SerializeField] VideoPlayer frameEffect;

    private int effectCount = 0;

    public void PlayEffect()
    {
        frameEffect.Play(); //エフェクトを再生
        effectCount++; //追いかけられている数だけ増やす
    }

    public void StopEffect()
    {
        if (effectCount > 0)
            effectCount--; //追いかけるのをやめた分だけ減らす

        if (effectCount == 0) //誰にも追いかけられていない時
            frameEffect.Stop(); //エフェクトを止める
    }
}
