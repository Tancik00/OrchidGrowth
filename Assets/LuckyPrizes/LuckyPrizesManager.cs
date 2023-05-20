using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using MergeFactory;
using Mindravel.UI;
using UnityEngine.Purchasing;
using Random = UnityEngine.Random;

//Prize part
[System.Serializable]
public class Prizes
{
    public string name;
    public Sprite sprite;
    public float points;
    public float targhetPoints;
    public int ID;

    public float percent()
    {
        return points / targhetPoints;
    }

}

public class LuckyPrizesManager : MonoBehaviour
{
    [Header("Scripts")]
    //public GameObject GM;
    //private UIManager UI;
    public ads adScript;
    //private DebugManager DB;
    

    [Header("MainButtons & Panels")]
    public GameObject buttonWinPrizesBuble;
    public GameObject buttonWinPrizesSpin;
    public GameObject buttonRaffleDraw;
    public GameObject buttonRules;

    public GameObject panelWinPrizes;
    public GameObject panelRaffleDraw;
    public GameObject panelRules;

    [Header("Prizes Part")]
    public GameObject myPrizesParent;
    public Sprite puzzleSprite;
    public ScrollRect prizeScrollRect;

    [Header("Congrats Popup")]
    public GameObject CongratsPanel;
    public GameObject ticketCongratsPanel;
    public int CongratsId;
    public GameObject CongratsImage;
    public int clickedBuble;
    public GameObject CongratsAmountText;
    public GameObject bubleAnimationPrefa;
    
    public Prizes[] prizeDatabase;
    public GameObject[] bubbles;

    public Image rafflePrizeImage;
    private List<GameObject> prizesObjectsList = new List<GameObject>();
    private List<Prizes> bublesPrizes = new List<Prizes>();
    private List<Prizes> hidenPrizesList = new List<Prizes>();

    private int[] bublesPrizesIDs = new int[5];

    private bool AdsEnabled;
    private bool _isBubbleClicked;
    public int actualPrizeID;

    private void Awake()
    {
        // References setup
        //AD = GM.GetComponent<ads>();
        //UI = GM.GetComponent<UIManager>();
        //DB = GM.GetComponent<DebugManager>();
        
        AdsEnabled = true;

#if UNITY_EDITOR
        AdsEnabled = false;
#endif

        for (int i = 0; i < prizeDatabase.Length; i++)
        {
            prizeDatabase[i].ID = i;
            hidenPrizesList.Add(prizeDatabase[i]);
        }

        foreach (Transform child in myPrizesParent.transform)
        {
            prizesObjectsList.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }

        SetUpCurrentPrizes();
        
        if (PlayerPrefs.HasKey(DateTime.Today.ToString() + "_TodayPrizeId"))
        {
            actualPrizeID = PlayerPrefs.GetInt(DateTime.Today.ToString() + "_TodayPrizeId");
        }
        else
        {
            actualPrizeID = UnityEngine.Random.Range(0, prizeDatabase.Length);
            PlayerPrefs.SetInt(DateTime.Today.ToString() + "_TodayPrizeId", actualPrizeID);
        }
    }

    private void Start()
    {
        RandomizeBubbles();
        //buttonWinPrizesBubleClicked();
        setUpButtonOnClickEvents();
        spinSetUp();
        
        //CountMinPrizePuzzles();
    }

   
    public void RandomizeBubbles()
    {
        for (int i = 0; i < bubbles.Length; i++)
        {
            int prizeID = Random.Range(0, hidenPrizesList.Count);


            bublesPrizesIDs[i] = hidenPrizesList[prizeID].ID;

            Image puzzleImage = bubbles[i].GetComponentInChildren<Image>();
            if (hidenPrizesList[prizeID].points <= 3f)
                puzzleImage.sprite = hidenPrizesList[prizeID].sprite;
            else
                puzzleImage.sprite = puzzleSprite;

            bublesPrizes.Add(hidenPrizesList[prizeID]);
            hidenPrizesList.RemoveAt(prizeID);
        }
    }


    private void ChangeBubblePrize(int bubbleID)
    {
        int prizeID = Random.Range(0, hidenPrizesList.Count);

        //Debug.Log(prizeID + " - dosent fit - " + hidenPrizesList.Count);

        bublesPrizesIDs[bubbleID] = hidenPrizesList[prizeID].ID;

        Image puzzleImage = bubbles[bubbleID].GetComponentInChildren<Image>();
        if (hidenPrizesList[prizeID].points <= 3f)
            puzzleImage.sprite = hidenPrizesList[prizeID].sprite;
        else
            puzzleImage.sprite = puzzleSprite;

        bublesPrizes.Add(hidenPrizesList[prizeID]);
        hidenPrizesList.RemoveAt(prizeID);
        hidenPrizesList.Add(prizeDatabase[currentPrizeID]);
    }


    public void bubbleClicked(int bubbleID)
    {
        _isBubbleClicked = true;
        clickedBuble = bubbleID;

        int prizeID = bublesPrizesIDs[clickedBuble];

        float tempAmountGained = PointSystem(prizeID);
        //Sprite tempSprite = prizesObjectsList[prizeID].transform.GetChild(1).GetComponent<Image>().sprite;
        Sprite tempSprite = prizeDatabase[prizeID].sprite;

        conratzPanelSet(tempSprite, tempAmountGained);

        if (AdsEnabled)
        {
            if (adScript.rewardedAvailable)
            {
                adScript.bublePrize = true;
                adScript.ShowRewardVideo("BubblePrize_REWARD");
                adScript.wasRequested = true;
            }
            else
            {
                GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
                GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
            }
        }
        else
        {
            BubleAdReward();
        }
    }

    public void BubleAdReward()
    {
        currentPrizeID = bublesPrizesIDs[clickedBuble];

        bublesPrizes.RemoveAt(clickedBuble);

        CongratsPanel.SetActive(true);
        ChangeBubblePrize(clickedBuble);
    }


    public void congratsPanelActivate()
    {
        CongratsPanel.SetActive(true);
    }


    int currentPrizeID;
    public void AddPointToPrize()
    {
        CongratsPanel.SetActive(false);

        StartCoroutine(scrolToWinPrizeAndPop());

        if (_isBubbleClicked)
        {
            StartCoroutine(hideBubleAndScaleUp());

            GameObject tempBuble = Instantiate(bubleAnimationPrefa, bubbles[clickedBuble].transform);
            tempBuble.transform.position = bubbles[clickedBuble].transform.GetChild(0).position;
            _isBubbleClicked = false;
        }
        AddPoints(currentPrizeID);
    }

    public void AddPoints(int prizeID)
    {
        var receivedPoints = PlayerPrefs.GetInt(DateTime.Today + "receivedPoints");
        receivedPoints++;
        PlayerPrefs.SetInt(DateTime.Today + "receivedPoints", receivedPoints);
        float totalPoints = PointSystem(prizeID) + PlayerPrefs.GetFloat("ProgressPrize" + prizeDatabase[prizeID].name);
        PlayerPrefs.SetFloat("ProgressPrize" + prizeDatabase[prizeID].name, totalPoints);
        prizeDatabase[prizeID].points = PlayerPrefs.GetFloat("ProgressPrize" + prizeDatabase[prizeID].name);

        Slider sliderObject = prizesObjectsList[prizeID].GetComponentInChildren<Slider>();
        Text pointsText = sliderObject.GetComponentInChildren<Text>();

        float tempPercent = prizeDatabase[prizeID].points / prizeDatabase[prizeID].targhetPoints;

        if (tempPercent < 0.9f)
            pointsText.text = prizeDatabase[prizeID].points.ToString() + "/" + prizeDatabase[prizeID].targhetPoints;
        else if (tempPercent >= 0.95f && tempPercent < 0.98f)
            pointsText.text = prizeDatabase[prizeID].points.ToString("F1") + "/" + prizeDatabase[prizeID].targhetPoints;
        else
            pointsText.text = prizeDatabase[prizeID].points.ToString("F2") + "/" + prizeDatabase[prizeID].targhetPoints;

        sliderObject.value = (prizeDatabase[prizeID].points) / prizeDatabase[prizeID].targhetPoints;
    }

    public void DecreasePoints(int prizeID, float points)
    {
        float totalPoints = PlayerPrefs.GetFloat("ProgressPrize" + prizeDatabase[prizeID].name) - points;
        PlayerPrefs.SetFloat("ProgressPrize" + prizeDatabase[prizeID].name, totalPoints);
        prizeDatabase[prizeID].points = PlayerPrefs.GetFloat("ProgressPrize" + prizeDatabase[prizeID].name);

        Slider sliderObject = prizesObjectsList[prizeID].GetComponentInChildren<Slider>();
        Text pointsText = sliderObject.GetComponentInChildren<Text>();

        float tempPercent = prizeDatabase[prizeID].points / prizeDatabase[prizeID].targhetPoints;

        if (tempPercent < 0.9f)
            pointsText.text = prizeDatabase[prizeID].points.ToString() + "/" + prizeDatabase[prizeID].targhetPoints;
        else if (tempPercent >= 0.95f && tempPercent < 0.98f)
            pointsText.text = prizeDatabase[prizeID].points.ToString("F1") + "/" + prizeDatabase[prizeID].targhetPoints;
        else
            pointsText.text = prizeDatabase[prizeID].points.ToString("F2") + "/" + prizeDatabase[prizeID].targhetPoints;

        sliderObject.value = (prizeDatabase[prizeID].points) / prizeDatabase[prizeID].targhetPoints;
    }

    public int CountMinPrizePuzzles()
    {
        List<float> filledPercents = new List<float>();
        for (int i = 0; i < prizeDatabase.Length; i++)
        {
            var filledPercent = (PlayerPrefs.GetFloat("ProgressPrize" + prizeDatabase[i].name) * 100) /
                                prizeDatabase[i].targhetPoints;
            filledPercents.Add(filledPercent);
        }

        var min = filledPercents[0];
        var minElementIndex = 0;
        for (int j = 0; j < filledPercents.Count; j++)
        {
            if (filledPercents[j] < min)
            {
                min = filledPercents[j];
                minElementIndex = j;
            }
        }

        return minElementIndex;
    }

    IEnumerator scrolToWinPrizeAndPop()
    {
        yield return new WaitForSeconds(0.1f);

        float tempGoTo = (float)(currentPrizeID) / (prizesObjectsList.Count-1);

        Transform imageTransform = prizesObjectsList[currentPrizeID].transform.GetChild(1).transform;
        Image cell = prizesObjectsList[currentPrizeID].GetComponentInChildren<Image>();
        

        for (int i = 0; i < 20; i++)
        {
            float tempVar = Mathf.Lerp(prizeScrollRect.horizontalNormalizedPosition, tempGoTo, 0.35f);
            prizeScrollRect.horizontalNormalizedPosition = tempVar;
            yield return new WaitForFixedUpdate();
        }
        
        float scalePower = 0.02f;
        for (int i = 0; i < 20; i++)
        {
            imageTransform.localScale += (Vector3.one * scalePower);
            cell.color = Color.Lerp(Color.yellow, Color.white, 0.5f); // cum pizda masii lucreaza lerpu ista
            yield return new WaitForFixedUpdate();
        }
        for (int i = 0; i < 20; i++)
        {
            imageTransform.localScale -= (Vector3.one * scalePower);
            cell.color = Color.Lerp(Color.white, Color.yellow, 0f);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator hideBubleAndScaleUp()
    {
        Transform bubleHiden = bubbles[clickedBuble].transform.GetChild(0);
        yield return new WaitForSeconds(0.1f);
        bubleHiden.localScale *= 0;

        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 20; i++)
        {
            bubleHiden.localScale += (Vector3.one * 0.05f);
            yield return new WaitForFixedUpdate();
        }
    }


    void conratzPanelSet(Sprite sprite,float amount)
    {
        CongratsAmountText.GetComponent<Text>().text = amount.ToString();
        CongratsImage.GetComponent<Image>().sprite = sprite;

    }

    private float PointSystem(int id)
    {
        float tempPercent = prizeDatabase[id].points / prizeDatabase[id].targhetPoints;

        if (tempPercent < 0.9f)
            return 1f;
        else if (tempPercent >= 0.9f && tempPercent < 0.95f)
            return 0.5f;
        else if (tempPercent >= 0.95f && tempPercent < 0.98f)
            return 0.1f;
        else if (tempPercent >= 0.98f && tempPercent < 0.99f)
            return 0.05f;
        else if (tempPercent >= 0.99f && tempPercent < 0.999f)
            return 0.01f;
        else
            return 0f;
    }

    public GameObject bubleMenu;
    public GameObject spinMenu;

    public void buttonWinPrizesBubleClicked()
    {
        buttonClicked(buttonWinPrizesBuble);
        panelSetUp(panelWinPrizes);
        bubleMenu.SetActive(true);
        spinMenu.SetActive(false);
    }

    public void buttonWinPrizesSpinClicked()
    {
        buttonClicked(buttonWinPrizesSpin);
        panelSetUp(panelWinPrizes);
        bubleMenu.SetActive(false);
        spinMenu.SetActive(true);
    }

    public void buttonRaffleDrawClicked()
    {
        buttonClicked(buttonRaffleDraw);
        panelSetUp(panelRaffleDraw);
    }

    public void buttonRulesClicked()
    {
        buttonClicked(buttonRules);
        panelSetUp(panelRules);
    }

    void buttonClicked(GameObject btnObj)
    {
        buttonWinPrizesBuble.GetComponent<Image>().color = Color.white;
        buttonWinPrizesBuble.GetComponentInChildren<Text>().color = Color.white;
        buttonWinPrizesBuble.GetComponent<CanvasGroup>().alpha = 0.5f;

        buttonWinPrizesSpin.GetComponent<Image>().color = Color.white;
        buttonWinPrizesSpin.GetComponentInChildren<Text>().color = Color.white;
        buttonWinPrizesSpin.GetComponent<CanvasGroup>().alpha = 0.5f;

        buttonRaffleDraw.GetComponent<Image>().color = Color.white;
        buttonRaffleDraw.GetComponentInChildren<Text>().color = Color.white;
        buttonRaffleDraw.GetComponent<CanvasGroup>().alpha = 0.5f;

        buttonRules.GetComponent<Image>().color = Color.white;
        buttonRules.GetComponentInChildren<Text>().color = Color.white;
        buttonRules.GetComponent<CanvasGroup>().alpha = 0.5f;

        btnObj.GetComponent<Image>().color = Color.yellow;
        btnObj.GetComponentInChildren<Text>().color = Color.yellow;
        btnObj.GetComponent<CanvasGroup>().alpha = 1.0f;
    }

    void panelSetUp(GameObject panelObj)
    {
        panelWinPrizes.SetActive(false);
        panelRaffleDraw.SetActive(false);
        panelRules.SetActive(false);
        panelObj.SetActive(true);
    }
    
    private void SetUpCurrentPrizes()
    {
        for (int i = 0; i < prizeDatabase.Length; i++)
        {
            prizesObjectsList[i].SetActive(true);
            Text prizeName = prizesObjectsList[i].GetComponentInChildren<Text>();
            Image prizeImage = prizesObjectsList[i].transform.GetChild(1).GetComponent<Image>();
            Slider sliderObject = prizesObjectsList[i].GetComponentInChildren<Slider>();
            Text pointsText = sliderObject.GetComponentInChildren<Text>();

            prizeName.text = prizeDatabase[i].name;
            prizeImage.sprite = prizeDatabase[i].sprite;

            prizeDatabase[i].points = loadPlayerPrizeProgress(i);

            if (prizeDatabase[i].points < 7.5f)
                pointsText.text = prizeDatabase[i].points.ToString() + "/" + prizeDatabase[i].targhetPoints;
            else if (prizeDatabase[i].points >= 7.5f && prizeDatabase[i].points < 9.51f)
                pointsText.text = prizeDatabase[i].points.ToString("F1") + "/" + prizeDatabase[i].targhetPoints;
            else
                pointsText.text = prizeDatabase[i].points.ToString("F2") + "/" + prizeDatabase[i].targhetPoints;

            sliderObject.value = (prizeDatabase[i].points) / prizeDatabase[i].targhetPoints;

        }
    }

    private float loadPlayerPrizeProgress(int ID)
    {
        if (PlayerPrefs.HasKey("ProgressPrize" + prizeDatabase[ID].name))
        {
            return PlayerPrefs.GetFloat("ProgressPrize" + prizeDatabase[ID].name);
        }
        else
        {
            PlayerPrefs.SetFloat("ProgressPrize" + prizeDatabase[ID].name,0);
            return 0;
        }
    }
    
    void setUpButtonOnClickEvents()
    {
        buttonWinPrizesBuble.GetComponent<Button>().onClick.AddListener(delegate { buttonWinPrizesBubleClicked(); });
        buttonWinPrizesSpin.GetComponent<Button>().onClick.AddListener(delegate { buttonWinPrizesSpinClicked(); });
        buttonRaffleDraw.GetComponent<Button>().onClick.AddListener(delegate { buttonRaffleDrawClicked(); });
        buttonRules.GetComponent<Button>().onClick.AddListener(delegate { buttonRulesClicked(); });
    }

    int returnOnelowestPrizeIDForBubles()
    {
        int[] ids = new int[4];
        float[] points = new float[prizeDatabase.Length];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = prizeDatabase[i].points/ prizeDatabase[i].targhetPoints;
        }
        for (int j = 0; j < 4; j++)
        {
            for (int i = j + 1; i < prizeDatabase.Length; i++)
            {
                if (points[j] > points[i])
                {
                    float temp = points[i];
                    points[i] = points[j];
                    points[j] = temp;
                    ids[j] = prizeDatabase[i].ID;
                }
            }
        }
        return ids[Random.Range(0, 4)];
    }

    int returnOnelowestPrizeIDForBublesFromHidens()
    {
        int prizesToSearch = 4;

        int[] ids = new int[prizesToSearch];
        float[] points = new float[hidenPrizesList.Count-1];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = hidenPrizesList[i].points / hidenPrizesList[i].targhetPoints;
        }

        for (int j = 0; j < prizesToSearch; j++)
        {
            for (int i = j; i < points.Length; i++)
            {
                if (points[j] > points[i])
                {
                    float temp = points[i];
                    points[i] = points[j];
                    points[j] = temp;
                    ids[j] = i;
                }
            }
        }
        return ids[Random.Range(0, 4)];

    }

    // SPIN  // SPIN  // SPIN  // SPIN  // SPIN  //

    [Header("Spins")]
    public Button spinButton;
    public GameObject[] spinObjectList;
    private Outline[] outlines = new Outline[8];

    private Prizes[] spinPrizes = new Prizes[8];

    private static int spinItems = 8;

    void spinSetUp() // called on start
    {
        
        for (int i = 0; i < spinItems; i++)
        {
            outlines[i] = spinObjectList[i].GetComponent<Outline>();
            Image tempImage = spinObjectList[i].transform.GetChild(0).GetComponent<Image>();
            tempImage.sprite = prizeDatabase[i].sprite;
            spinObjectList[i].transform.GetChild(1).GetComponent<Text>().text = prizeDatabase[i].name;
        }
        SpinPanelsIconSet();
    }

    void SpinPanelsIconSet()
    {
        for (int i = 0; i < spinItems; i++)
        {
            Image tempImage = spinObjectList[i].transform.GetChild(0).GetComponent<Image>();
            tempImage.sprite = prizeDatabase[i].sprite;
            spinObjectList[i].transform.GetChild(1).GetComponent<Text>().text = prizeDatabase[i].name;
        }
    }


    public void ButtonStartSpin()
    {
       // Aici
       if (AdsEnabled)
       {
           if (adScript.rewardedAvailable)
           {
               ads.instance.spinPrize = true;
               ads.instance.ShowRewardVideo("SpinPrize_REWARD");
               ads.instance.wasRequested = true;
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
           //if (DB.debugMode && DB.adsActive)
            //{
                StartSpin();
            //}
            //else if (!DB.debugMode || !DB.adsActive)
                //return;
        }
    }


    public void StartSpin()
    {
        StartCoroutine(SpiningCorutine());
    }

    IEnumerator SpiningCorutine()
    {
        spinButton.interactable = false;
        int prize = returnOnelowestPrizeID();
        currentPrizeID = prize;
        int i = Random.Range(0, spinItems);
        float time = 0.01f;
        for (int j = 0; j <= 4; j++)
        {
            for (i = 0; i < spinItems; i++)
            {
                
                outlines[i].enabled = true;
                time += 0.005f;
                yield return new WaitForSeconds(time);
                
                if (j == 4 && i == prize)
                {
                    yield return new WaitForSeconds(0.5f);
                    outlines[i].enabled = false;
                    conratzPanelSet(prizeDatabase[prize].sprite, PointSystem(prize));
                    congratsPanelActivate();
                    break;
                }

                outlines[i].enabled = false;
            }
            i = 0;
        }
        spinButton.interactable = true;
    }

    int returnOnelowestPrizeID()
    {
        int[] ids = new int[4] {0,0,0,0 };
        float[] points = new float[8];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = prizeDatabase[i].points;
        }
        for (int j = 0; j < 4; j++)
        {
            for (int i = j; i < 8; i++)
            {
                if(points[j] > points[i])
                {
                    float temp = points[i];
                    points[i] = points[j];
                    points[j] = temp;
                    ids[j] = i;
                }
            }
        }
        
        return ids[Random.Range(0,4)];
    }


    // RAFFLE DRAW ADS

    public void ButtonActivateTicket()
    {
        Debug.Log("WTF");

        // Aici
        if (AdsEnabled)
        {
            if (adScript.rewardedAvailable)
            {
                ads.instance.ticket = true;
                ads.instance.ShowRewardVideo("Ticket_REWARD");
                ads.instance.wasRequested = true;
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
            //if (DB.debugMode && DB.adsActive)
            //{
                ActivateTicketReward();
            //}
            //else if (!DB.debugMode || !DB.adsActive)
                //return;
        }
    }

    public void ActivateTicketReward()
    {
        ticketCongratsPanel.SetActive(true);
         GetComponentInChildren<LuckyRaffleDraw>().ActivateTicket();
    }



    public void ButtonActivateUnlockTickets()
    {
        // Aici
        //if (UI.AdsEnabled)
        //{
        //    AD.spawnTickets = true;
        //    AD.ShowRewardVideo("SpawnTicket_REWARD");
        //    AD.wasRequested = true;
        //}
        //else
        //{
        //    if (DB.debugMode && DB.adsActive)
        //    {
        //        ActivateUnlockTicketReward();
        //    }
        //    else if (!DB.debugMode || !DB.adsActive)
        //        return;
        //}
    }

    public void ActivateUnlockTicketReward()
    {
        GetComponentInChildren<LuckyRaffleDraw>().unlockTicket();
    }

}
