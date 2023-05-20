using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoaderScript : MonoBehaviour
{
    public bool adLoaded ;

    private Transform tr;
    private Vector3 loaderRotation;
    private Image loadBarIMG;

    public GameObject AddText;
//    private DebugManager debugMNG;
    Coroutine turnBackCourutine;

    bool lanbadaON = false;

    // Start is called before the first frame update
    void Start()
    {
        //debugMNG = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DebugManager>();
        loadBarIMG = GetComponent<Image>();
        tr = transform;
        loadBarIMG.enabled = false;
        AddText.SetActive(true);

#if UNITY_EDITOR

        //if (debugMNG.adsActive)
        //    adLoaded = true;
        //else if (!debugMNG.adsActive)
        //    adLoaded = false;

#endif
        
    }

    private void OnEnable()
    {

        if (!adLoaded)
        {
            if (!lanbadaON)
            {
                Button btn = AddText.GetComponentInParent<Button>();
                btn.onClick.AddListener(() => checkIfAddOn());
                lanbadaON = true;
            }
        }
        else
        {
            AddText.SetActive(true);
            loadBarIMG.enabled = false;
        }
    }

    private void OnDisable()
    {
        if (turnBackCourutine != null)
        {
            StopCoroutine(turnBackCourutine);
        }
        AddText.SetActive(true);
        if (loadBarIMG != null) // this line was added last
            loadBarIMG.enabled = false;
    }

    void checkIfAddOn()
    {

//#if UNITY_EDITOR
//        adLoaded = debugMNG.adsActive;
//#endif

        if (!adLoaded)
        {
            loadBarIMG.enabled = true;
            AddText.SetActive(false);

            if (turnBackCourutine != null)
                StopCoroutine(turnBackCourutine);
            if (gameObject.active)
                turnBackCourutine = StartCoroutine(corutineSpinAndWaitForAdd());
            /*
            if (turnBackCourutine == null)
                turnBackCourutine = StartCoroutine(corutineSpinAndWaitForAdd());
            */
        }
        else
        {
            loadBarIMG.enabled = false;
            AddText.SetActive(true);

            if (turnBackCourutine != null)
                StopCoroutine(turnBackCourutine);
        } 
    }

    IEnumerator corutineSpinAndWaitForAdd()
    {

        while (!adLoaded )
        {
//#if UNITY_EDITOR
//            adLoaded = debugMNG.adsActive;
//#endif
            yield return new WaitForSeconds(0.1f);
        }

        AddText.SetActive(true);
        loadBarIMG.enabled = false;

    }
}
