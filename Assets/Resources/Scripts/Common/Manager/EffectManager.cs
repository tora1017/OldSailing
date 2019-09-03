/***********************************************************************/
/*! @file   EffectManager.cs
*************************************************************************
*   @brief  エフェクトを管理するスクリプト
*************************************************************************
*   @author Tsuyoshi Takaguchi
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : BaseObjectSingleton<EffectManager> {

    [SerializeField]
    private GameObject[] effectObject; // @brief 再生をしたいエフェクトを格納する配列

    private GameObject spawnObject; // @brief 生成されたオブジェクトを格納する

    private class EffectInfo
    {

        public string resourceName; // @brief エフェクトの名前
        public GameObject effectObject; // @brief 生成したいエフェクト
        
        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="resourceName"> エフェクトの名前 </param>
        /// <param name="effectObject"> エフェクトのオブジェクト </param>
        public EffectInfo(string resourceName, GameObject effectObject)
        {
            this.resourceName = resourceName;
            this.effectObject = effectObject;
        }
    }

    private Dictionary<string, EffectInfo> effectClips; // @brief エフェクトをまとめるディクショナリ

    /// <summary>
    /// @brief エフェクトの初期化関数
    /// </summary>
    private void EffectInitialize()
    {
        effectClips = new Dictionary<string, EffectInfo>();
   
        effectClips.Add("Tap", new EffectInfo("TapEffect", effectObject[(int)eEffectType.eTap_Effect]));
        effectClips.Add("PassedMarker", new EffectInfo("PassedMarkerEffect", effectObject[(int)eEffectType.ePassedMarker_Effect]));
    }

    /// <summary>
    /// @brief エフェクトの再生関数
    /// </summary>
    /// <param name="effectName"> エフェクトの名前 </param>
    /// <param name="effectPosition"> 生成させる場所 </param>
    /// <param name="effectRotate"> 生成させる場所の角度 </param>
    /// <param name="effectScale"> 生成される大きさ </param>
    public void PlayEffect(string effectName, Vector3 effectPosition, Quaternion effectRotate, Vector3? effectScale = null)
    {
        if(effectClips.ContainsKey(effectName) == false)
        {
            return;
        }
        // エフェクトオブジェクトを生成し一定時間後に削除する
        Destroy(spawnObject = Instantiate(effectClips[effectName].effectObject, effectPosition, effectRotate), 1.2f);
        // 生成されたエフェクトの大きさを変更
        if (effectScale != null)
        {
            spawnObject.transform.localScale = (Vector3)effectScale;
        }
    }

    /// <summary>
    /// @brief 全てのエフェクトの停止関数
    /// </summary>
    public void EndEffectAll()
    {
        foreach (KeyValuePair<string, EffectInfo> pair in effectClips)
        {
            Destroy(pair.Value.effectObject);
        }
    }

    public void Start()
    {
        EffectInitialize();
    }
}
