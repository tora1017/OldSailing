/***********************************************************************/
/*! @file   SplashEffectPlayer.cs
*************************************************************************
*   @brief  水しぶきエフェクトを再生する
*************************************************************************
*   @author Tsuyoshi Takaguchi
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashEffectPlayer : BaseObject {

    private ParticleSystem splashParticle; // @brief 生成したいパーティクルを格納する

    /// <summary>
    /// @brief 水しぶきエフェクトの再生
    /// </summary>
    private void PlaySplashEffect()
    {
        if (Singleton<GameInstance>.Instance.IsShipMove && !Singleton<GameInstance>.Instance.IsGoal)
        {
            splashParticle.Play();
        }
    }

    /// <summary>
    /// @brief 水しぶきエフェクトの停止
    /// </summary>
    private void EndSplashEffect()
    {
        if (Singleton<GameInstance>.Instance.IsGoal)
        {
            splashParticle.Stop();
        }
    }

    public void Start()
    {
        //splashParticle = this.GetComponent<ParticleSystem>();
        //splashParticle.Stop();
    }

	public void Update()
    {
        //PlaySplashEffect();
        //EndSplashEffect();
    }
}
