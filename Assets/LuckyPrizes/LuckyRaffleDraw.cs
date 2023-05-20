using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class LuckyRaffleDraw : MonoBehaviour
{
    public int hoursSubstacted = 0;

    public Text timerTextOnCongratsPanel;
    public Text ticketIDTextOnCongratsPanel;

    public GameObject historyRafflePref;
    public Transform historyRafflePrefParent;

    LuckyPrizesManager LPM;

    // PRIZE
    Transform prizePanel;

    Image prizeImage;
    Text prizeName;
    Text prizeTimer;

    int actualPrizeID;

    TimeSpan timeInRepeater = TimeSpan.Zero;

    // MyTikets
    Transform myTikets;
    GameObject ticketParent;

    // WiningHistory
    Transform historyPrize;
    //GameObject winingTicketsParent;
    LuckyRaffleWinningTicket[] winingTickets = new LuckyRaffleWinningTicket[5];
    GameObject placementObjectIfNoHistory;

    public int totalHistoryItem
    {
        get
        {
            return PlayerPrefs.GetInt("totalHistoryItem", 0);
        }
        set
        {
            PlayerPrefs.SetInt("totalHistoryItem", value);
        }
    }

    private void Awake()
    {
        LPM = GetComponentInParent<LuckyPrizesManager>();

        setTodayPrizeID();

        Transform tempChild = transform.GetChild(1);

        setPrizeReferences(tempChild);


        myTikets = tempChild.GetChild(2);
        ticketParent = myTikets.transform.GetChild(1).gameObject;


        historyPrize = tempChild.GetChild(3);
        //winingTicketsParent = historyPrize.GetChild(0).gameObject;
        //placementObjectIfNoHistory = historyPrize.GetChild(3).gameObject;

        foreach (var item in tickets)
        {
            Button addOne = item.gameObject.transform.GetChild(0).GetComponent<Button>();
            Button unlockTicket = item.gameObject.transform.GetChild(1).GetComponent<Button>();
            addOne.onClick.AddListener(delegate { LPM.ButtonActivateUnlockTickets(); }) ;
            unlockTicket.onClick.AddListener(delegate { LPM.ButtonActivateTicket(); });
        }

        for (int i = 0; i < tickets.Count; i++)
        {
            tickets[i].activateActivationTicket();
        }

    }


    void setTodayPrizeID()
    {
        if (PlayerPrefs.HasKey(DateTime.Today.ToString() + "_TodayPrizeId"))
        {
            actualPrizeID = PlayerPrefs.GetInt(DateTime.Today.ToString() + "_TodayPrizeId");
        }
        else
        {
            actualPrizeID = LPM.actualPrizeID;
            PlayerPrefs.SetInt(DateTime.Today.ToString() + "_TodayPrizeId", actualPrizeID);
        }
    }


    void setPrizeReferences(Transform tempChild)
    {
        prizePanel = tempChild.GetChild(1);
        prizeTimer = prizePanel.GetChild(3).GetComponent<Text>();
    }

    private void Start()
    {
        //spawnTickets(6);
        StartTicketsSetup();

        InvokeRepeating("TimeDescender", 0, 1);

        SetRaffleHistoryValues();
    }
    
    void clearTicketsAndSetNew()
    {
        foreach (var item in tickets)
        {
            item.resetTicket();
            item.gameObject.SetActive(false);
        }
        for (int i = 0; i < tickets.Count; i++)
        {
            tickets[i].gameObject.SetActive(true);
            tickets[i].activateActivationTicket();
        }
        
    }
    
    void TimeDescender()
    {
        TimeSpan timeTemp = DateTime.Now.TimeOfDay;
        //timeTemp += TimeSpan.FromHours(3);
        prizeTimer.text = string.Format("{0:00}:{1:00}:{2:00}", 23 - timeTemp.Hours, 59 - timeTemp.Minutes,60 - timeTemp.Seconds);
        timerTextOnCongratsPanel.text = "After " + prizeTimer.text + " to draw the wining number";                      
        timeInRepeater += TimeSpan.FromSeconds(1);
        if(!PlayerPrefs.HasKey(DateTime.Today.ToString() + "_TodayPrizeId"))
        { 
            // TODO set another prize
            setTodayPrizeID();
            clearTicketsAndSetNew();
            SetRaffleHistoryValues();
            RaffleController.Instance.raffleNumber = 1000 + (PlayerPrefs.GetInt("totalHistoryItem") + 1);
            RaffleController.Instance.raffleNumberText.text = RaffleController.Instance.raffleNumber.ToString();
        }
    }

    private void OnEnable()
    {
        //LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetChild(0).GetComponent<RectTransform>());
    }

    // TICKETS

    public List<LuckyRaffleTicket> tickets = new List<LuckyRaffleTicket>();

    int ticketsNumberOpenedToday;

    int ticketsOnScren = 6;
    
    string[] ticketsOpenedArray;

    
    void openTickets()
    {

        for (int i = 0; i < PlayerPrefs.GetInt(DateTime.Today.ToString() + "_TicketsOpened"); i++)
        {
            tickets[i].gameObject.SetActive(true);
        }
    }
    
    void StartTicketsSetup()
    {
        ticketsNumberOpenedToday = PlayerPrefs.GetInt(DateTime.Today.ToString() + "_TicketsOpened");
        ticketsOpenedArray = PlayerPrefs.GetString(DateTime.Today.ToString() + "_TicketsOpenedCode").Split(' ');

        openTickets();

        for (int i = 0; i < ticketsNumberOpenedToday; i++)
        {
            try
            {
                tickets[i].ticketIsActivated(ticketsOpenedArray[i]);
            }
            catch (Exception ex)
            {
                Debug.Log("Error here, more ticket must be oppened.");
            }
        }
        //tickets[ticketsNumberOpenedToday].gameObject.SetActive(true);
        if(PlayerPrefs.GetInt(DateTime.Today.ToString() + "_LastTicketState") == 1)
        {
            tickets[ticketsNumberOpenedToday].activateActivationTicket();
        }
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetChild(1).GetChild(2).GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetChild(1).GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
    
    public void ActivateTicket()
    {
        // add on activate tickets
        int tempTickNumb = PlayerPrefs.GetInt(DateTime.Today.ToString() + "_TicketsOpened");
        PlayerPrefs.SetInt(DateTime.Today.ToString() + "_TicketsOpened", tempTickNumb+1);
        // add code on ticket
        string stringTcketsIDs = PlayerPrefs.GetString(DateTime.Today.ToString() + "_TicketsOpenedCode");
        string TicketID = randomNumGen().ToString();
        stringTcketsIDs = stringTcketsIDs + TicketID + " ";
        PlayerPrefs.SetString(DateTime.Today.ToString() + "_TicketsOpenedCode", stringTcketsIDs);
        
        tickets[tempTickNumb].activateTicket(TicketID);
        ticketIDTextOnCongratsPanel.text = TicketID;
        //tickets[tempTickNumb + 1].gameObject.SetActive(true);

        //if(tempTickNumb > 6)
        //{
        //    tickets[tempTickNumb].gameObject.SetActive(true);
        //    tickets[tempTickNumb].activateActivationTicket();
        //}
        PlayerPrefs.SetInt(DateTime.Today.ToString() + "_LastTicketState", 0);
        IncreaseHistoryItem();
    }

    private void SetRaffleHistoryValues()
    {
        if (PlayerPrefs.GetString("initialDate") == String.Empty)
        {
            PlayerPrefs.SetString("initialDate", DateTime.Today.ToString());
        }

        if (totalHistoryItem!=0)
        {
            int day = -1;
            int roundNumber = totalHistoryItem;

            if (historyRafflePrefParent.childCount != 0)
            {
                for (int j = 0; j < historyRafflePrefParent.childCount; j++)
                {
                    Destroy(historyRafflePrefParent.GetChild(j).gameObject);                                        
                }                                                           
            }
            
            for (int i = 0; i < totalHistoryItem; i++)                                                                                       
            {
                var raffleHistoryItem = Instantiate(historyRafflePref, historyRafflePrefParent);                                             
                var raffleHistoryInfo = raffleHistoryItem.GetComponent<RaffleHistoryInfo>();
                raffleHistoryInfo.raffleNumberTxt.text = "The " + (1000 + roundNumber) + " round";
                raffleHistoryInfo.winningNumber.text = PlayerPrefs.GetString(DateTime.Today.AddDays(day).ToString() + "winningTicketNumber");
                raffleHistoryInfo.ticketsIDs.text = PlayerPrefs.GetString(DateTime.Today.AddDays(day).ToString() + "_TicketsOpenedCode");
                day--;
                roundNumber--;
            }                                                                                                                                
        }
    }

    private void IncreaseHistoryItem()
    {
        var ticketsOpened = PlayerPrefs.GetInt(DateTime.Today.ToString() + "_TicketsOpened");          
        if (ticketsOpened == 1)                                                                        
        {                                                                                             
            totalHistoryItem++;   
            
            PlayerPrefs.SetString(DateTime.Today.ToString() + "winningTicketNumber", randomNumGen().ToString());   
        }                                                                                              
    }                                                      

    public void unlockTicket()
    {
        Debug.Log(tickets[PlayerPrefs.GetInt(DateTime.Today.ToString() + "_TicketsOpened") + 1].name);
        tickets[PlayerPrefs.GetInt(DateTime.Today.ToString() + "_TicketsOpened") ].activateActivationTicket();
        PlayerPrefs.SetInt(DateTime.Today.ToString() + "_LastTicketState", 1);
    }


    int randomNumGen()
    {
        int nmbr = UnityEngine.Random.Range(695356465,865456460);
        return nmbr;
    }
}
