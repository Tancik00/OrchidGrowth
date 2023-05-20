using UnityEngine;
using UnityEngine.UI;
using System;

namespace NiobiumStudios
{
    public class DailyRewardUI : MonoBehaviour
    {
        public bool showRewardName;

        [Header("UI Elements")]
        public Text textDay;                
        public Text textReward;             
        public Image imageRewardBackground; 
        public Image imageReward;           
        public Color colorClaim;            
        public Color colorUnclaimed;
        public Text adsCountText;

        [Header("Internal")]
        public int day;

        [HideInInspector]
        public Reward reward;

        public DailyRewardState state;
        public CanvasGroup _canvasGroup;

        public enum DailyRewardState
        {
            UNCLAIMED_AVAILABLE,
            UNCLAIMED_UNAVAILABLE,
            CLAIMED
        }
        
        public void Initialize()
        {
            textDay.text = string.Format("Day {0}", day.ToString());
            if (reward.reward > 0)
            {
                if (showRewardName)
                {
                    textReward.text = reward.unit;
                }
                else
                {
                    textReward.text = reward.reward.ToString();
                }
            }
            else
            {
                textReward.text = reward.unit.ToString();
            }
            imageReward.sprite = reward.sprite;
        }
        
        public void Refresh()
        {
            switch (state)
            {
                case DailyRewardState.UNCLAIMED_AVAILABLE:
                    imageRewardBackground.color = colorClaim;
                    _canvasGroup.alpha = 1f;
                    break;
                case DailyRewardState.UNCLAIMED_UNAVAILABLE:
                    imageRewardBackground.color = colorUnclaimed;
                    _canvasGroup.alpha = 0.5f;
                    break;
                case DailyRewardState.CLAIMED:
                    imageRewardBackground.color = colorClaim;
                    _canvasGroup.alpha = 1f;
                    break;
            }
        }
    }
}