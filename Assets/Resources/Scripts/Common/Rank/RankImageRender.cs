using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankImageRender : BaseObject {

    
	private Image image;            // @brief SpriteRendererを格納する変数
    private int prevRank;           // @brief Rankを記憶しておく変数
    private int animationCount;     // @brief 再生時間をカウントする変数
    private Animator rankAnimator;  // @brief Animatorを格納する変数

    /* 使用するスプライトの変数 */
	[SerializeField] private Sprite sourceRankFirst;
	[SerializeField] private Sprite sourceRankSecond;
	[SerializeField] private Sprite sourceRankThird;
	[SerializeField] private Sprite sourceRankFource;



    /// <summary>
    /// @brief BaseObjectの実装
    /// </summary>
	protected override void OnAwake()
	{
		base.OnAwake();
		image = GetComponent<Image>();
        prevRank = 4;
        rankAnimator = GetComponent<Animator>();
        rankAnimator.SetBool("animationFlag", false);
        animationCount = 0;
    }
 
    /// <summary>
	/// @brief ランク計算に基づいてCanvas上のスプライトを変更する
    /// </summary>
    /// <param name="rank"> プレイヤーのランク </param>
	public void ChangeRankSprite(int rank)
    {
        switch (rank)
        {
            case 1:
				image.sprite = sourceRankFirst;

                break;

            case 2:
				image.sprite = sourceRankSecond;
                break;

            case 3:
				image.sprite = sourceRankThird;
                break;

            case 4:
				image.sprite = sourceRankFource;
                break;  
        }
        if (!Singleton<GameInstance>.Instance.IsGoal)
        {
            RankAnimation(rank);
        }
    }
    
    /// <summary>
    /// @brief 順位変動時の回転アニメーションを再生する
    /// </summary>
    /// <param name="rank"> プレイヤーのランク </param>
    private void RankAnimation(int rank)
    {
        if(prevRank != rank && 
           !rankAnimator.GetBool("animationFlag"))
        {
            rankAnimator.SetBool("animationFlag", true);
            prevRank = rank;
        }
        
        if (rankAnimator.GetBool("animationFlag"))
        {
            animationCount++;
            // 30はアニメーション再生時間
            if(animationCount >= 15)
            {
                rankAnimator.SetBool("animationFlag", false);
                animationCount = 0;
            }
        }
    }
}
