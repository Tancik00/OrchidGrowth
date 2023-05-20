using System;
using MergeFactory;
using NiobiumStudios;
using UnityEngine;
using NotificationSamples;
using Unity.Notifications;

public class MyNotifications : MonoBehaviour
{
    [SerializeField] private GameNotificationsManager notificationsManager;
    public DailyRewardsInterface dailyRewardsInterface;

    private void StartNotification()
    {
        GameNotificationChannel channel = new GameNotificationChannel("orchid growth", "game notification", "!!!", GameNotificationChannel.NotificationStyle.Default, true, true, true, true);
        notificationsManager.Initialize(channel);
    }

    private void Start()
    {
        StartNotification();
    }

    public void ShowNotification()
    {
        ShowNotificationAfterDelay("orchid growth", "game notification!", DateTime.Now.AddSeconds(5));  
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            if (notificationsManager.Initialized)
            {
                notificationsManager.CancelAllNotifications();
            }
        }
        else
        {
            ShowNotifications();
            GlobalNotification();
        }
    }

    private void ShowNotifications()
    {
        //CreateNotification("Ground is full!", "Come to merge plants soon!", DateTime.Now.AddSeconds((GridManager.instance.FREESLOTS * 10) + 300));
        //CreateNotification("You achieved your maximum revenue!", "Come and unlock more plant!", DateTime.Now.AddMinutes(30));
        //CreateNotification("Hi!", "Your revenue was doubled❗", DateTime.Now.AddMinutes(60));
        //CreateNotification("You have money to buy a higher level plant.", "Come and get it", DateTime.Now.AddMinutes(90));
        //CreateNotification("Orchid Growth", "Upgrade your plant and get extra gift box❗", DateTime.Now.AddMinutes(120));
        //CreateNotification("What time is it now?⌚", "It's time to collect prize puzzles🧩", DateTime.Now.AddMinutes(150));
        //CreateNotification("Congrats!", "We've got a gift for you!", DateTime.Now.AddMinutes(180));
        //CreateNotification("100 Free iPhone 12 Pro Max!!", "Have you claimed it yet? Hurry now🚴", DateTime.Now.AddMinutes(210));
        //CreateNotification("Lucky Scratch", "⬛⬛⬛⬛ 👈  Scratch away to see how much you get", DateTime.Now.AddMinutes(240));
        //CreateNotification("iPhone is waiting for you❗", "Answer❤️ Reject💔", DateTime.Now.AddMinutes(300));
        //CreateNotification("Claim your gift now>>>", "10🧩 = FREE iPhone 11 Pro! Wait no more!", DateTime.Now.AddMinutes(360));
        //CreateNotification("🚗......DUDU", "Your GIFT - iPhone is being delivered.", DateTime.Now.AddMinutes(480));

        TimeSpan timeTemp = DateTime.Now.TimeOfDay;
        var startedNewLotteryTime = (((23 - timeTemp.Hours) * 60) + (59 - timeTemp.Minutes));
        CreateNotification("Don't miss the chance to win a brand new iMac Pro❗", "One more hour left till the lucky lottery starts❗", DateTime.Now.AddMinutes(startedNewLotteryTime - 60)); //за один час до начала лотереи
        //CreateNotification("Hi!", "Remember to come back for your money!", DateTime.Now.AddMinutes(1440));
        var timeUntilNextDailyBonus = dailyRewardsInterface.GetRemainedTime();
        if (timeUntilNextDailyBonus >= 0)
        {
            CreateNotification("Claim your daily bonus right now❗", "Win 0.05 Bitcoin in 30 days❗💰", DateTime.Now.AddMinutes(timeUntilNextDailyBonus)); //при возможности забрать следующий дэйли бонус
            CreateNotification("Don't miss the opportunity to receive 🤑 0.05 Bitcoin❗", "Claim your daily bonus now❗", DateTime.Now.AddMinutes(timeUntilNextDailyBonus + 360)); //после 6 часов если ещё не забрал доступный дэйли бонус
        }
    }

    private void GlobalNotification()
    {
        var currentTime = (DateTime.Now.Hour * 60) + DateTime.Now.Minute;
        var time6AM = (6 * 60) - currentTime;
        if (time6AM < 0)
        {
            time6AM = (24 * 60) + time6AM;
        }

        var time6PM = (18 * 60) - currentTime;
        if (time6PM < 0)
        {
            time6PM = (24 * 60) + time6PM;
        }

        var time12AM = (12 * 60) - currentTime;
        if (time12AM < 0)
        {
            time12AM = (24 * 60) + time12AM;
        }

        var time12PM = (24 * 60) - currentTime;
        if (time12PM < 0)
        {
            time12PM = (24 * 60) + time12PM;
        }

        for (int i = 0; i < 50; i++)
        {
            CreateNotification("What time is it now?⌚", "It's time to collect prize puzzles🧩", DateTime.Now.AddMinutes(time6AM));
            time6AM += (24 * 60);
            CreateNotification("[Transfer] Please confirm receipt.", "Your account is gonna receive AirPods puzzles. Claim it right now!!", DateTime.Now.AddMinutes(time6PM));
            time6PM += (24 * 60);
            CreateNotification("You've been chosen by Orchid Growth", "💘Exclusive prize for you! Claim now!", DateTime.Now.AddMinutes(time12AM));
            time12AM += (24 * 60);
            CreateNotification("💬1 New Message:", "Someone nearby win iPhone 12 Pro Max", DateTime.Now.AddMinutes(time12PM));
            time12PM += (24 * 60);
        }
    }

    private void CreateNotification(string title, string body, DateTime time)
    {
        IGameNotification notification = notificationsManager.CreateNotification();
        if (notification != null)
        {
            notification.Title = title;
            notification.Body = body;
            notification.DeliveryTime = time;
            notification.LargeIcon = "icon_0";
            notification.SmallIcon = "icon_1";

            notificationsManager.ScheduleNotification(notification);
        }
    }
    
    private void ShowNotificationAfterDelay(string title, string body, DateTime time)
    {
        IGameNotification createNotification = notificationsManager.CreateNotification();
        if (createNotification != null)
        {
            createNotification.Title = title;
            createNotification.Body = body;
            createNotification.DeliveryTime = time;

            notificationsManager.ScheduleNotification(createNotification);

            /*var notificationToDisplay = notificationsManager.ScheduleNotification(createNotification);
            notificationToDisplay.Reschedule = true;*/
        }
        else
        {
            Debug.Log("=== NOTIFICATION IS NULL");
        }
    }
}
