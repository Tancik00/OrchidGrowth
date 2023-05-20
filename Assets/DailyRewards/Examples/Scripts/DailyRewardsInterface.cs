using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using MergeFactory;
using Mindravel.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace NiobiumStudios
{
    public class DailyRewardsInterface : MonoBehaviour
    {
        public RectTransform dailyBonusPanel;
        public GameObject dailyRewardPrefab;
        public GameObject topListUserPref;
        public Transform topListUsersParent;

        [Header("Panel Reward Message")] public GameObject panelReward;
        public Text textReward;
        public Button buttonCloseReward;
        public Image imageReward;

        [Header("Panel Reward")] public Button buttonClaim;
        public Text textTimeDue;
        public Text topListTimer;
        public Text userIDText;
        public Transform dailyRewardsGroup;
        public ScrollRect scrollRect;
        public Text userGameTimeText;

        public LuckyPrizesManager luckyPrizesManager;
        public string[] topUserIDsList = new string[5];
        public string[] topUserReceivedPointsList = new string[5];

        public int UserId
        {
            get
            {
                return PlayerPrefs.GetInt("UserId");
            }
            set
            {
                PlayerPrefs.SetInt("UserId", value);
            }
        }

        private bool readyToClaim;
        private List<DailyRewardUI> dailyRewardsUI = new List<DailyRewardUI>();

        private DailyRewards dailyRewards;
        private bool AdsEnabled;
        private int adsCount;
        private TimeSpan timeInRepeater = TimeSpan.Zero;
        private TimeSpan timer = new TimeSpan();
        private Vector3 _dailyBonusPanelInitPosition;

        private void Awake()
        {
            dailyRewards = GetComponent<DailyRewards>();

            AdsEnabled = true;

#if UNITY_EDITOR
			AdsEnabled = false;
#endif
        }

        private void Start()
        {
            InitializeDailyRewardsUI();

            UpdateUI();
            
            if (UserId <= 0)
            {
                UserId = Random.Range(100000000, 999999999);
            }

            userIDText.text = "Your ID: " + UserId;

            SetTodayValues();

            SetTopRandomUserValues();

            InvokeRepeating("TimeDescender", 0, 1);
            
            if (PlayerPrefs.GetString("testTimer") != String.Empty)
            {
                timer = TimeSpan.Parse(PlayerPrefs.GetString("testTimer"));
            }

            _dailyBonusPanelInitPosition = dailyBonusPanel.anchoredPosition;
        }

        private void Update()
        {
            dailyRewards.TickTime();
            // Updates the time due
            CheckTimeDifference();

            if (dailyRewards.isResetPrize)
            {
                UpdateUI();
            }

            CountGameTime();
        }
        
        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                if (PlayerPrefs.GetString("testTimer") != String.Empty)
                {
                    timer = TimeSpan.Parse(PlayerPrefs.GetString("testTimer"));
                }
            }
            else
            {
                PlayerPrefs.SetString("testTimer", timer.ToString());
            }
        }

        private void CountGameTime()
        {
            timer = timer.Add(TimeSpan.FromSeconds(Time.deltaTime));
            userGameTimeText.text = string.Format("{0:00}:{1:00}:{2:00}", timer.Hours, timer.Minutes, timer.Seconds);
        }

        private void SetTopRandomUserValues()
        {
            for (int i = 0; i < 5; i++)
            {
                var gameObj = Instantiate(topListUserPref, topListUsersParent);
                gameObj.GetComponentInChildren<Text>().text = "#" + (i + 1) + " User " + topUserIDsList[i ] + ":      " + topUserReceivedPointsList[i];
            }

            var user = Instantiate(topListUserPref, topListUsersParent);
            user.GetComponentInChildren<Text>().text = "User " + UserId + ":            " + PlayerPrefs.GetInt(DateTime.Today.AddDays(-1) + "receivedPoints");
            user.GetComponentInChildren<Outline>().effectColor = new Color32(255, 123, 0, 255);
        }

        private void SetTodayValues()
        {
            if (PlayerPrefs.HasKey(DateTime.Today.ToString() + "topRandomUserReceivedPoints"))
            {
                var value = PlayerPrefs.GetString(DateTime.Today.ToString() + "topRandomUserReceivedPoints", "1");
                topUserReceivedPointsList = value.Split(' ');
            }
            else
            {
                var value = PlayerPrefs.GetString(DateTime.Today.ToString() + "topRandomUserReceivedPoints", "1");
                if (value == "1")
                {
                    for (int i = 0; i < topUserReceivedPointsList.Length; i++)
                    {
                        var randomValue = 0;
                        switch (i)
                        {
                            case 0: randomValue = Random.Range(1440, 1500); break;
                            case 1: randomValue = Random.Range(1300, 1440); break;
                            case 2: randomValue = Random.Range(1100, 1300); break;
                            case 3: randomValue = Random.Range(950, 1100); break;
                            case 4: randomValue = Random.Range(650, 950); break;
                        }
                        topUserReceivedPointsList[i] = randomValue.ToString();
                        var userValue = PlayerPrefs.GetString(DateTime.Today.ToString() + "topRandomUserReceivedPoints");
                        userValue += topUserReceivedPointsList[i] + " ";
                        PlayerPrefs.SetString(DateTime.Today.ToString() + "topRandomUserReceivedPoints", userValue);
                    }
                }
                else
                {
                    topUserReceivedPointsList = value.Split(' ');
                }
            }
            SetTodayTopUserIdValues();
        }

        private void SetTodayTopUserIdValues()
        {
            if (PlayerPrefs.HasKey(DateTime.Today.ToString() + "topRandomUserIds"))
            {
                var ids = PlayerPrefs.GetString(DateTime.Today.ToString() + "topRandomUserIds", "1");
                topUserIDsList = ids.Split(' ');
            }
            else
            {
                var ids = PlayerPrefs.GetString(DateTime.Today.ToString() + "topRandomUserIds", "1");
                if (ids == "1")
                {
                    for (int i = 0; i < topUserIDsList.Length; i++)
                    {
                        topUserIDsList[i] = Random.Range(100000000, 999999999).ToString();
                        var userIds = PlayerPrefs.GetString(DateTime.Today.ToString() + "topRandomUserIds");
                        userIds += topUserIDsList[i] + " ";
                        PlayerPrefs.SetString(DateTime.Today.ToString() + "topRandomUserIds", userIds);
                    }
                }
                else
                {
                    topUserIDsList = ids.Split(' ');
                }
            }
        }

        public void ClaimClick()
        {
            //Claim();
            if (AdsEnabled)
            {
                if (ads.instance.rewardedAvailable)
                {
                    ads.instance.ShowRewardVideo("LuckyPuzzle_REWARD");
                    ads.instance.wasRequested = true;
                    ads.instance.luckyPuzzle = true;
                }
                else
                {
                    GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
                    GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE",
                        "TRY AGAIN LATER !", string.Empty, null, null, false);
                }
            }
            else
            {
                Claim();
            }
        }

        public string[] rewardAddedPoints;
        public string[] addedPrizeIDs;

        public void Claim()
        {
            var dailyReward = dailyRewardsUI[DailyRewardsCore<DailyRewards>.instance.availableReward - 1];
            dailyReward.reward.watchedAdsCount = PlayerPrefs.GetInt("watchedAdsCount" + dailyReward.day);
            var watchesAdsCount = dailyReward.reward.watchedAdsCount;
            watchesAdsCount++;

            if (watchesAdsCount >= dailyReward.reward.adsCount)
            {
                if (dailyReward.day < 30)
                {
                    dailyRewards.ClaimPrize();
                    readyToClaim = false;
                    UpdateUI();

                    //var randomPrizeID = Random.Range(0, 14);
                    var randomPrizeID = luckyPrizesManager.CountMinPrizePuzzles();
                    for (int i = 0; i < dailyReward.reward.reward; i++)
                    {
                        luckyPrizesManager.AddPoints(randomPrizeID);
                    }

                    var addedPoints = PlayerPrefs.GetString("rewardAddedPoints");
                    addedPoints += dailyReward.reward.reward + ".";
                    PlayerPrefs.SetString("rewardAddedPoints", addedPoints);
                    rewardAddedPoints = PlayerPrefs.GetString("rewardAddedPoints").Split('.');

                    var addedIDs = PlayerPrefs.GetString("addedPrizeIDs");
                    addedIDs += randomPrizeID + ".";
                    PlayerPrefs.SetString("addedPrizeIDs", addedIDs);
                    addedPrizeIDs = PlayerPrefs.GetString("addedPrizeIDs").Split('.');
                }
                else
                {
                    if (DataProvider.instance.gameData.currentPlayerLevelIndex < 34)
                    {
                        GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
                        GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("",
                            "YOU NEED GET LAST LEVEL!", string.Empty, null, null, false);
                    }
                    else
                    {
                        DataProvider.instance.gameData.BTC = 0.05f;
                    }
                }
            }

            dailyReward.adsCountText.text = watchesAdsCount + "/" + dailyReward.reward.adsCount;
            PlayerPrefs.SetInt("watchedAdsCount" + dailyReward.day, watchesAdsCount);
        }

        public void ResetRewardedPrizes()
        {
            for (int i = 0; i < addedPrizeIDs.Length - 1; i++)
            {
                luckyPrizesManager.DecreasePoints(Int32.Parse(addedPrizeIDs[i]), Int32.Parse(rewardAddedPoints[i]));
            }

            for (int j = 0; j < dailyRewards.rewards.Count; j++)
            {
                PlayerPrefs.SetInt("watchedAdsCount" + j, 0);
                var reward = dailyRewards.GetReward(j + 1);
                dailyRewardsUI[j].adsCountText.text = 0 + "/" + reward.adsCount;
            }

            dailyRewards.isResetPrize = false;
        }

        public void CloseRewardClick()
        {
            var keepOpen = dailyRewards.keepOpen;
            panelReward.SetActive(false);
        }

        void OnEnable()
        {
            dailyRewards.onClaimPrize += OnClaimPrize;
            dailyRewards.onInitialize += OnInitialize;
        }

        void OnDisable()
        {
            if (dailyRewards != null)
            {
                dailyRewards.onClaimPrize -= OnClaimPrize;
                dailyRewards.onInitialize -= OnInitialize;
            }
        }

        // Initializes the UI List based on the rewards size
        private void InitializeDailyRewardsUI()
        {
            for (int i = 0; i < dailyRewards.rewards.Count; i++)
            {
                int day = i + 1;
                var reward = dailyRewards.GetReward(day);
                var watchedAdsCount = PlayerPrefs.GetInt("watchedAdsCount" + day);

                GameObject dailyRewardGo = Instantiate(dailyRewardPrefab, dailyRewardsGroup);

                DailyRewardUI dailyRewardUI = dailyRewardGo.GetComponent<DailyRewardUI>();

                dailyRewardUI.day = day;
                dailyRewardUI.reward = reward;
                reward.adsCount = GetDayAdsCount(day);
                dailyRewardUI.adsCountText.text = watchedAdsCount + "/" + reward.adsCount;
                dailyRewardUI.Initialize();
                dailyRewardsUI.Add(dailyRewardUI);
            }
        }

        public void UpdateUI()
        {
            dailyRewards.CheckRewards();

            if (dailyRewards.isResetPrize)
            {
                ResetRewardedPrizes();
            }

            bool isRewardAvailableNow = false;

            var lastReward = dailyRewards.lastReward;
            var availableReward = dailyRewards.availableReward;

            foreach (var dailyRewardUI in dailyRewardsUI)
            {
                var day = dailyRewardUI.day;

                if (day == availableReward)
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.UNCLAIMED_AVAILABLE;

                    isRewardAvailableNow = true;
                }
                else if (day <= lastReward)
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.CLAIMED;
                }
                else
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.UNCLAIMED_UNAVAILABLE;
                }

                dailyRewardUI.Refresh();
            }

            buttonClaim.gameObject.SetActive(isRewardAvailableNow);
            if (isRewardAvailableNow)
            {
                //SnapToReward();
                textTimeDue.text = "Claim all your days and win 0.05 BTC.";
            }
            
            readyToClaim = isRewardAvailableNow;
        }

        private void TimeDescender()
        {
            TimeSpan timeTemp = DateTime.Now.TimeOfDay;
            topListTimer.text = string.Format("Until next reward: " + "{0:00}:{1:00}:{2:00}", 23 - timeTemp.Hours, 59 - timeTemp.Minutes,
                60 - timeTemp.Seconds);
            timeInRepeater += TimeSpan.FromSeconds(1);
        }

    // Snap to the next reward
        public void SnapToReward()
        {
            //Canvas.ForceUpdateCanvases();

            var lastRewardIdx = dailyRewards.lastReward;

            // Scrolls to the last reward element
            if (dailyRewardsUI.Count - 1 < lastRewardIdx)
                lastRewardIdx++;

            if (lastRewardIdx > dailyRewardsUI.Count - 1)
                lastRewardIdx = dailyRewardsUI.Count - 1;

            var target = dailyRewardsUI[lastRewardIdx].GetComponent<RectTransform>();
        }

       

        private void CheckTimeDifference()
        {
            if (!readyToClaim)
            {
                TimeSpan difference = dailyRewards.GetTimeDifference();

                // If the counter below 0 it means there is a new reward to claim
                if (difference.TotalSeconds <= 0)
                {
                    readyToClaim = true;
                    UpdateUI();
                    //SnapToReward();
                    return;
                }

                string formattedTs = dailyRewards.GetFormattedTime(difference);

                textTimeDue.text = string.Format("Come back in {0} for your next reward", formattedTs);
            }
        }
        
        public int GetRemainedTime()
        {
            TimeSpan difference = dailyRewards.GetTimeDifference();
            return dailyRewards.GetTime(difference);
        }

        // Delegate
        private void OnClaimPrize(int day)
        {
            GetDayAdsCount(day);
            panelReward.SetActive(true);

            var reward = dailyRewards.GetReward(day);
            var unit = reward.unit;
            var rewardQt = reward.reward;

            imageReward.sprite = reward.sprite;
            if (rewardQt > 0)
            {
                textReward.text = string.Format("You got {0} {1}!", reward.reward, unit);
            }
            else
            {
                textReward.text = string.Format("You got {0}!", unit);
            }
        }

        private void OnInitialize(bool error, string errorMessage)
        {
            if (!error)
            {
                var showWhenNotAvailable = dailyRewards.keepOpen;
                var isRewardAvailable = dailyRewards.availableReward > 0;

                UpdateUI();

                //SnapToReward();
                CheckTimeDifference();
            }
        }

        private int GetDayAdsCount(int day)
        {
            /*if (day <= 1)
            {
                adsCount = 35;
            }

            else if (day <= 2)
            {
                adsCount = 30;
            }

            else if (day >= 3 && day < 6)
            {
                adsCount = 25;
            }

            else if (day <= 6)
            {
                adsCount = 30;
            }

            else if (day <= 7)
            {
                adsCount = 35;
            }

            else if (day >= 8 && day <= 29)
            {
                adsCount = 5;
            }

            else if (day <= 30)
            {
                adsCount = 50;
            }*/
            adsCount = 5;

            return adsCount;
        }

        public void ShowDailyBonusPanel()
        {
            dailyBonusPanel.anchoredPosition = _dailyBonusPanelInitPosition;
        }

        public void HideDailyBonusPanel()
        {
            var pos = dailyBonusPanel.anchoredPosition;
            pos.x -= 2000f;
            dailyBonusPanel.anchoredPosition = pos;
        }
    }
}