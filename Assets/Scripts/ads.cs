using UnityEngine;
//using Firebase.Analytics;
using MergeFactory;
using Mindravel.UI;
using NiobiumStudios;

public class ads : MonoBehaviour
{
    //public UIManager uiManager;
    //public SpawnMoney spawnMoneyScript;
    public static ads instance;

    [Header("Scripts")]
    public LuckyPrizesManager luckyPrize;
    public DailyRewardsInterface dailyRewardsInterface;
    public LandFillManager landFillMNG;
    public WelcomeBackScreen welcomeScript;
    public GiftboxPanel giftboxScript;
    public Item_UnitStore itemUnitScript;
    public Coin2XManager coinManager;
    public UnitStore unitStore;
    public GetPlantButtonOnMainMenu getPlantButtonOnMainMenu;

    //old
    public bool paypal100Panel;
    public bool amazon100Panel;
    public bool card100000Panel;
    public bool spinPrize;
    public bool ticket;
    public bool bublePrize;
    public bool addASlot;
    public bool bubblePuzzle;
    public bool spawnTickets;
    public bool luckyPuzzle;
    public bool achievementDoubleReward;
    public bool betterCrates;
    public bool fasterCrates;
    public bool moreMystry;
    public bool buyCoins;
    public bool buyMegaCoins;
    public bool buyLandFill;
    public bool buyStarterPack;
    public bool buyMegaPack;
    public bool claimMergePlantReward;
    public bool unlockStoreItem;
    public bool flyingBubblePuzzle;
    public bool getFreeItemOnMainMenu;

    [Header("Ad Values")]
    public bool rewardedAvailable;
    private string tempPlacement;
    public bool wasRequested;
    public LoaderScript[] loaders;
    public GameObject loadPanel;
    public int openedMistryBoxCount;

    //private bool _isFirstOpened;
     
    private void Awake()
    {
        instance = this;

        string YOUR_APP_KEY = "";

        if (Application.platform == RuntimePlatform.Android)
        {
            YOUR_APP_KEY = "d95c8469";
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            YOUR_APP_KEY = "d95c4ae1"; // orchid growth 
        }

        var betterCrates = false;

        IronSource.Agent.init(YOUR_APP_KEY, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL,
            IronSourceAdUnits.OFFERWALL, IronSourceAdUnits.BANNER);
        IronSource.Agent.validateIntegration();

        //_isFirstOpened = true;

        //if (!PlayerPrefs.HasKey("FirstTimeIn") || PlayerPrefs.GetInt("FirstTimeIn") == 0)
        //PlayerPrefs.SetInt("FirstTimeIn", 1);
        //else
        IronSource.Agent.loadInterstitial();
        //IronSource.Agent.showInterstitial();
        /**if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial();
        }*/

        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM, "WinPrizes_banner");
        IronSource.Agent.displayBanner();
    }

    // Start is called before the first frame update
    void Start()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM, "WinPrizes_banner");
        IronSource.Agent.displayBanner();

        //GameObject tempObject = GameObject.FindGameObjectWithTag("GameManager");
        //uiManager = tempObject.GetComponent<UIManager>();
        //GameObject casinoSystemObject = GameObject.FindGameObjectWithTag("CasinoSystem");
        //spawnMoneyScript = casinoSystemObject.GetComponent<SpawnMoney>();


        setupCallbacks();
        //IronSource.Agent.loadInterstitial();
    }


    public void ShowRewardVideo(string placement)
    {
        tempPlacement = placement;

        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo(placement);
        }
        else
        {
            Debug.Log("Ad Not Loaded Yet!");
        }
    }
    
    public void ShowInterestial(string placement)
    {
        if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial(placement);
        }
    }
    
    void setupCallbacks()
    {
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
        IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
        IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
        IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
        IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
        IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
        IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;
    }
    
    //REWARD VIDEO IRON SOURCE
    
    void RewardedVideoAdOpenedEvent()
    {
        //AudioManager.instance.PauseSound("Theme");
        //AudioManager.instance.SoundState(false);
        Time.timeScale = 0f;    // pause
        print("IronSource - RewardedVideoAdOpenedEvent");
    }
    
    void RewardedVideoAdClosedEvent()
    {
        Time.timeScale = 1f;
        //AudioManager.instance.PlayTheme();
        //AudioManager.instance.SoundState(true);
        print("IronSource - RewardedVideoAdClosedEvent");
    }
    
    void RewardedVideoAvailabilityChangedEvent(bool available)
    {
        print("IronSource - RewardedVideoAvailabilityChangedEvent ->" + available);
        //Change the in-app 'Traffic Driver' state according to availability.
        bool rewardedVideoAvailability = available;
        /*for (int i = 0; i < loaders.Length; i++)
        {
            loaders[i].adLoaded = available;
        }*/
        rewardedAvailable = available;

        if (wasRequested && available)
        {
            if(tempPlacement != null)
                ShowRewardVideo(tempPlacement);
        }
    }
    
    void RewardedVideoAdStartedEvent()
    {
        //AudioManager.instance.PauseSound("Theme");
        //AudioManager.instance.SoundState(false);
        Time.timeScale = 0f;    // pause
        print("IronSource - RewardedVideoAdStartedEvent");
    }
    //Invoked when the video ad finishes playing.
    void RewardedVideoAdEndedEvent()
    {
       // Time.timeScale = 1f;
        print("IronSource - RewardedVideoAdEndedEvent");
    }
    
    void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
    {
        print("ironSource Reward Video Finished");

        //SUCESFULL REWARD VIDEO WATCHED FULL

        wasRequested = false;
        
        switch (ssp.getPlacementName())
        {
            case "Add10Land_REWARDED":
                landFillMNG.AddLandFills(10);
                break;

            case "Welcome2X_REWARDED":
                welcomeScript.WelcomeDoubleReward();
                break;

            case "UpgradeBox_REWARDED":
                giftboxScript.UpgradeBoxReward();
                break;

            case "FreeItem_REWARDED":
                itemUnitScript.FreeItemReward();
                break;
            case "DoubleCoins_REWARDED":
                coinManager.DoubleCoinsReward();
                break;
           default:
                break;
        }

        //else if (ssp.getPlacementName() == "PayPal100Dolars_REWARD" || paypal100Panel)
        //{
        //    paypal100Panel = false;
        //    uiManager.ActivateDolarWithPanel();
        //    ///
        //}
        //else if (ssp.getPlacementName() == "Amazon100Dolars_REWARD" || amazon100Panel)
        //{
        //    amazon100Panel = false;
        //    uiManager.ActivateCoinsWithPanel();

        //    ///
        //}
        //else if (ssp.getPlacementName() == "Card10000Dolars_REWARD" || card100000Panel)
        //{
        //    card100000Panel = false;
        //    uiManager.ActivateFruitsWithPanel();
        //    ///
        //}
        if (ssp.getPlacementName() == "SpinPrize_REWARD" || spinPrize)
        {
            spinPrize = false;
            luckyPrize.StartSpin();
        }
        else if (ssp.getPlacementName() == "Ticket_REWARD" || ticket)
        {
           ticket = false;
           luckyPrize.ActivateTicketReward();
        } 
        else if (ssp.getPlacementName() == "BubblePrize_REWARD" || bublePrize)
        {
           bublePrize = false;
           luckyPrize.BubleAdReward();
        } 
        else if (ssp.getPlacementName() == "AddSlot_REWARDED" || addASlot)
        {
            addASlot = false;
           GridManager.instance.AddSlot();
        }  
        else if (ssp.getPlacementName() == "BubblePuzzle_REWARDED" || bubblePuzzle)
        {
            bubblePuzzle = false;
            FlyingPuzzle.Instance.GetBubblePrize();
        }
        else if (ssp.getPlacementName() == "LuckyPuzzle_REWARD" || luckyPuzzle)
        {
            luckyPuzzle = false;
            dailyRewardsInterface.Claim();
        }
        else if (ssp.getPlacementName() == "AchievementDoubleReward_REWARD" || achievementDoubleReward)
        {
            achievementDoubleReward = false;
            AchievementsManager.instance.GetAchievementDoubleReward();
        }
        else if (ssp.getPlacementName() == "BetterCrates_REWARD" || betterCrates)
        {
            betterCrates = false;
            ShopManager.instance.UpgradeCrate();
        }
        else if (ssp.getPlacementName() == "FasterCrates_REWARD" || fasterCrates)
        {
            fasterCrates = false;
            ShopManager.instance.SetFasterCrates();
        }
        else if (ssp.getPlacementName() == "MoreMystry_REWARD" || moreMystry)
        {
            moreMystry = false;
            ShopManager.instance.SetMoreMystry();
        }
        else if (ssp.getPlacementName() == "BuyCoins_REWARD" || buyCoins)
        {
            buyCoins = false;
            ShopManager.instance.CoinsPurchasedSuccessfully();
        }
        else if (ssp.getPlacementName() == "BuyMegaCoins_REWARD" || buyMegaCoins)
        {
            buyMegaCoins = false;
            ShopManager.instance.MegaCoinsPurchasedSuccessfully();
        }
        else if (ssp.getPlacementName() == "BuyLandFill_REWARD" || buyLandFill)
        {
            buyLandFill = false;
            ShopManager.instance.FillLand();
        }
        else if (ssp.getPlacementName() == "BuyStarterPack_REWARD" || buyStarterPack)
        {
            buyStarterPack = false;
            ShopManager.instance.StarterPackPurchasedSuccessfully();
        }
        else if (ssp.getPlacementName() == "BuyMegaPack_REWARD" || buyMegaPack)
        {
            buyMegaPack = false;
            ShopManager.instance.MegaPackPurchasedSuccessfully();
        }
        else if (ssp.getPlacementName() == "ClaimMergePlant_REWARD" || claimMergePlantReward)
        {
            claimMergePlantReward = false;
            GUIManager.Instance.ClaimMergePlantReward();
        }
        else if (ssp.getPlacementName() == "UnlockStoreItem_REWARD" || unlockStoreItem)
        {
            unlockStoreItem = false;
            unitStore.UnlockStoreItem();
        }
        else if (ssp.getPlacementName() == "FlyingBubblePuzzle_REWARDED" || flyingBubblePuzzle)
        {
            flyingBubblePuzzle = false;
            FlyingPuzzle.Instance.GetFlyingBubblePrize();
        }
        else if (ssp.getPlacementName() == "FreeItemOnMainMenu_REWARD" || getFreeItemOnMainMenu)
        {
            getFreeItemOnMainMenu = false;
            getPlantButtonOnMainMenu.GetPlantForFree();
        }
        //else if (ssp.getPlacementName() == "SpawnTicket_REWARD" || ticket)
        //{
        //    spawnTickets = false;
        //    luckyPrize.ActivateUnlockTicketReward();
        //}

        //   if (ssp.getPlacementName() == "PLACMENT_NAME") to check what to give
        AchievementsManager.instance.IncrementAchievementEvent(AchievementType.WATCHADS);
        DailyTasksController.instance.IncrementDailyTaskEvent(DailyTaskType.WATCHVIDEO);
    }
    //Invoked when the Rewarded Video failed to show
    //@param description - string - contains information about the failure.
    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
        Time.timeScale = 1f;
       /// AudioManager.instance.PlayTheme();
        print("IronSource -RewardedVideoAdShowFailedEvent");
        print(error.getDescription());
    }
    
    //INTERESTIAL
    //Invoked when the initialization process has failed.
    //@param description - string - contains information about the failure.
    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
        print("IronSource -InterstitialAdLoadFailedEvent");
        print(error.getDescription());
    }

    //Invoked right before the Interstitial screen is about to open.
    void InterstitialAdShowSucceededEvent()
    {
        print("IronSource -InterstitialAdShowSucceededEvent");
    }

    //Invoked when the ad fails to show.
    //@param description - string - contains information about the failure.
    void InterstitialAdShowFailedEvent(IronSourceError error)
    {
        print("IronSource -InterstitialAdShowFailedEvent");
        print(error.getDescription());
    }

    // Invoked when end user clicked on the interstitial ad
    void InterstitialAdClickedEvent()
    {
        print("IronSource - InterstitialAdClickedEvent");
        IronSource.Agent.loadInterstitial();
    }

    //Invoked when the interstitial ad closed and the user goes back to the application screen.
    void InterstitialAdClosedEvent()
    {
        print("IronSource -InterstitialAdClosedEvent");
        /*if (_isFirstOpened) 
        {
            if (PlayerPrefs.GetInt("TUT_1_COMPLETE") != 0)
            {
                winPrizePanel.SetActive(true);
                _isFirstOpened = false;
            }
        }*/
    }

    //Invoked when the Interstitial is Ready to shown after load function is called
    void InterstitialAdReadyEvent()
    {
        //IronSource.Agent.showInterstitial();
        print("IronSource - InterstitialAdReadyEvent");
        loadPanel.SetActive(false);
    }

    //Invoked when the Interstitial Ad Unit has opened
    void InterstitialAdOpenedEvent()
    {
        print("IronSource - InterstitialAdOpenedEvent");
    }
    
    //Invoked once the banner has loaded
    void BannerAdLoadedEvent()
    {
    }
    //Invoked when the banner loading process has failed.
    //@param description - string - contains information about the failure.
    void BannerAdLoadFailedEvent(IronSourceError error)
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM);
    }
    // Invoked when end user clicks on the banner ad
    void BannerAdClickedEvent()
    {
    }
    //Notifies the presentation of a full screen content following user click
    void BannerAdScreenPresentedEvent()
    {
    }
    //Notifies the presented screen has been dismissed
    void BannerAdScreenDismissedEvent()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM, "WinPrizes_banner");
        IronSource.Agent.displayBanner();
    }
    //Invoked when the user leaves the app
    void BannerAdLeftApplicationEvent()
    {
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }
}
