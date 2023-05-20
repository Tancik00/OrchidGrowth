using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.Collections;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class TenjinSDKController : MonoBehaviour
{

    void Start()
    {
        TenjinConnect();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            TenjinConnect();
        }
    }

    public void TenjinConnect()
    {
        BaseTenjin instance = Tenjin.getInstance("FYZ2IR3YPJ8VVVNNMG7KXQQUR7YDAZPO");

#if UNITY_IOS
        if (new Version(Device.systemVersion).CompareTo(new Version("14.0")) >= 0)
        {
            // Tenjin wrapper for requestTrackingAuthorization
            instance.RequestTrackingAuthorizationWithCompletionHandler((status) =>
            {
                Debug.Log("===> App Tracking Transparency Authorization Status: " + status);
                instance.RegisterAppForAdNetworkAttribution();
                // Sends install/open event to Tenjin
                instance.Connect();

            });
        }
        else
        {
            instance.Connect();
        }

#elif UNITY_ANDROID

        // Sends install/open event to Tenjin
        instance.Connect();

#endif
    }
}
