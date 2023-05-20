using Mindravel.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class ShakingButtonAnimation : MonoBehaviour
{
    public bool isLoopAnim;
    private float delayTime;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        delayTime = GetRandomDelay();
    }

    private void Update()
    {
        if (isLoopAnim)
        {
            anim.SetTrigger("ShakeButton");
        }
        else
        {
            if (delayTime > 0)
            {
                delayTime -= Time.deltaTime;
                if (delayTime <= 0)
                {
                    anim.SetTrigger("ShakeButton");
                    delayTime = GetRandomDelay();
                }
            }   
        }
    }

    private float GetRandomDelay()
    {
        return Random.Range(10, 30);
    }
}
