using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSignal_Initialization : MonoBehaviour
{
    void Start()
    {
        // Uncomment this method to enable OneSignal Debugging log output 
        // OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.INFO, OneSignal.LOG_LEVEL.INFO);
    
        // Replace 'YOUR_ONESIGNAL_APP_ID' with your OneSignal App ID.
        //OneSignal.StartInit("341ab2c5-d6ff-4c24-ae01-f93d3b84fbd0")
        OneSignal.StartInit("341ab2c5-d6ff-4c24-ae01-f93d3b84fbd0")
        //OneSignal.StartInit("270c4c87-b821-4519-9a40-906268814c51")
          .HandleNotificationOpened(HandleNotificationOpened)
          .Settings(new Dictionary<string, bool>() {
      { OneSignal.kOSSettingsAutoPrompt, false },
      { OneSignal.kOSSettingsInAppLaunchURL, false } })
          .EndInit();
    
        OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
    
        // The promptForPushNotifications function code will show the iOS push notification prompt. We recommend removing the following code and instead using an In-App Message to prompt for notification permission.
        OneSignal.PromptForPushNotificationsWithUserResponse(OneSignal_promptForPushNotificationsResponse);
    
        OSPermissionSubscriptionState state = OneSignal.GetPermissionSubscriptionState();
    }
    
    private void OneSignal_promptForPushNotificationsResponse(bool accepted)
    {
        Debug.Log("OneSignal_promptForPushNotificationsResponse: " + accepted);
    }
    
    // Gets called when the player opens the notification.
    private static void HandleNotificationOpened(OSNotificationOpenedResult result)
    {
     }
}
