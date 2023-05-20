using System;
using System.Collections;
using System.Collections.Generic;
using MergeFactory;
using UnityEditor;
using UnityEngine;

public class DailyTasksController : MonoBehaviour
{
    private static DailyTasksController _instance;

    public static DailyTasksController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DailyTasksController>();
            }

            return _instance;
        }
    }
    
    public List<DailyTask> dailyTasks;
    public GameObject dailyTaskPref;
    public Transform dailyTaskParent;
    public GameObject dailyTaskPanel;
    public List<DailyTaskItem> dailyTaskItemList;
    public GameObject completeSignal;
    public float rewardValue;

    private DailyTaskItem _dailyTaskItem;

    public int DailyTask_DailyLogin
    {
        get { return PlayerPrefs.GetInt("DailyTask_DailyLogin"); }
        set { PlayerPrefs.SetInt("DailyTask_DailyLogin", value); }
    }

    public int DailyTask_BuyPlants
    {
        get { return PlayerPrefs.GetInt("DailyTask_BuyPlants"); }
        set { PlayerPrefs.SetInt("DailyTask_BuyPlants", value); }
    }
    
    public int DailyTask_MergePlants
    {
        get { return PlayerPrefs.GetInt("DailyTask_MergePlants"); }
        set { PlayerPrefs.SetInt("DailyTask_MergePlants", value); }
    }
    
    public int DailyTask_GetPlants
    {
        get { return PlayerPrefs.GetInt("DailyTask_GetPlants"); }
        set { PlayerPrefs.SetInt("DailyTask_GetPlants", value); }
    }
    
    public int DailyTask_OpenMysteryBoxes
    {
        get { return PlayerPrefs.GetInt("DailyTask_OpenMysteryBoxes"); }
        set { PlayerPrefs.SetInt("DailyTask_OpenMysteryBoxes", value); }
    }
    
    public int DailyTask_WatchVideo
    {
        get { return PlayerPrefs.GetInt("DailyTask_WatchVideo"); }
        set { PlayerPrefs.SetInt("DailyTask_WatchVideo", value); }
    }
    
    private void Start()
    {
        if (PlayerPrefs.GetString("dailyTaskDay") != String.Empty)
        {
            var today = DateTime.Today.Day.ToString();
            if (today != PlayerPrefs.GetString("dailyTaskDay"))
            {
                ResetDailyTasksProgress();
            }
        }
        else
        {
            PlayerPrefs.SetString("dailyTaskDay", DateTime.Today.Day.ToString()); 
        }

        for (int i = 0; i < dailyTasks.Count; i++)
        {
            _dailyTaskItem = Instantiate(dailyTaskPref, dailyTaskParent).GetComponent<DailyTaskItem>();
            _dailyTaskItem.taskName.text = dailyTasks[i].taskName;
            _dailyTaskItem.taskImage.sprite = dailyTasks[i].taskSprite;
            _dailyTaskItem.ID = i;
            dailyTaskItemList.Add(_dailyTaskItem);
        }

        IncrementDailyTaskEvent(DailyTaskType.DAILYLOGIN);
        
        SetProgressParameters();
        
        UpdateRewardValue();
    }

    private void SetProgressParameters()
    {
        SetProgressParametersGeneralMethod(0, DailyTask_DailyLogin);
        SetProgressParametersGeneralMethod(1, DailyTask_BuyPlants);
        SetProgressParametersGeneralMethod(2, DailyTask_MergePlants);
        SetProgressParametersGeneralMethod(3, DailyTask_GetPlants);
        SetProgressParametersGeneralMethod(4, DailyTask_OpenMysteryBoxes);
        SetProgressParametersGeneralMethod(5, DailyTask_WatchVideo);
    }

    private void SetProgressParametersGeneralMethod(int taskIndex, int taskIncrementingValue)
    {
        dailyTaskItemList[taskIndex].progressText.text = taskIncrementingValue + "/" + dailyTasks[taskIndex].taskTarget;
        dailyTaskItemList[taskIndex].progressBar.fillAmount = taskIncrementingValue / dailyTasks[taskIndex].taskTarget;
        if (taskIncrementingValue >= dailyTasks[taskIndex].taskTarget)
        {
            dailyTaskItemList[taskIndex].rewardButton.interactable = true;
            if (PlayerPrefs.GetInt("taskCompleted" + taskIndex) <= 0)
            {
                completeSignal.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("taskCompleted" + taskIndex)>0)
        {
            dailyTaskItemList[taskIndex].completedButton.SetActive(true);
        }
    }

    public void UpdateRewardValue()
    {
        rewardValue = (UnitManager.instance.units[UnitManager.instance.MaxItemID].price * 15f)/5;
        for (int i = 0; i < dailyTaskItemList.Count; i++)
        {
            dailyTaskItemList[i].rewardValue.text = rewardValue.ToShortHandString();
        }
    }

    public void ResetDailyTasksProgress()
    {
        DailyTask_DailyLogin = 0;
        DailyTask_BuyPlants = 0;
        DailyTask_MergePlants = 0;
        DailyTask_GetPlants = 0;
        DailyTask_OpenMysteryBoxes = 0;
        DailyTask_WatchVideo = 0;
        PlayerPrefs.SetInt("taskCompleted" + 0, 0);
        PlayerPrefs.SetInt("taskCompleted" + 1, 0);
        PlayerPrefs.SetInt("taskCompleted" + 2, 0);
        PlayerPrefs.SetInt("taskCompleted" + 3, 0);
        PlayerPrefs.SetInt("taskCompleted" + 4, 0);
        PlayerPrefs.SetInt("taskCompleted" + 5, 0);

        for (int i = 0; i < dailyTaskItemList.Count; i++)
        {
            dailyTaskItemList[i].completedButton.SetActive(false);
            dailyTaskItemList[i].progressBar.fillAmount = 0f;
            dailyTaskItemList[i].progressText.text = "0/" + dailyTasks[i].taskTarget;
            dailyTaskItemList[i].rewardButton.interactable = false;
        }
    }

    public void IncrementDailyTaskEvent(DailyTaskType type)
    {
        switch (type)
        {
            case DailyTaskType.DAILYLOGIN:
                if (DailyTask_DailyLogin < dailyTasks[0].taskTarget)
                {
                    DailyTask_DailyLogin++;
                    if (DailyTask_DailyLogin >= dailyTasks[0].taskTarget)
                    {
                        dailyTaskItemList[0].rewardButton.interactable = true;
                    }
                }
                break;
            case DailyTaskType.BUYPLANTS:
                if (DailyTask_BuyPlants < dailyTasks[1].taskTarget)
                {
                    DailyTask_BuyPlants++;
                    if (DailyTask_BuyPlants >= dailyTasks[1].taskTarget)
                    {
                        dailyTaskItemList[1].rewardButton.interactable = true;
                    }
                }
                break;
            case DailyTaskType.MERGEPLANTS:
                if (DailyTask_MergePlants < dailyTasks[2].taskTarget)
                {
                    DailyTask_MergePlants++;
                    if (DailyTask_MergePlants >= dailyTasks[2].taskTarget)
                    {
                        dailyTaskItemList[2].rewardButton.interactable = true;
                    }
                }
                break;
            case DailyTaskType.GETPLANTS:
                if (DailyTask_GetPlants < dailyTasks[3].taskTarget)
                {
                    DailyTask_GetPlants++;
                    if (DailyTask_GetPlants >= dailyTasks[3].taskTarget)
                    {
                        dailyTaskItemList[3].rewardButton.interactable = true;
                    }
                }
                break;
            case DailyTaskType.OPENMYSTERYBOX:
                if (DailyTask_OpenMysteryBoxes < dailyTasks[4].taskTarget)
                {
                    DailyTask_OpenMysteryBoxes++;
                    if (DailyTask_OpenMysteryBoxes >= dailyTasks[4].taskTarget)
                    {
                        dailyTaskItemList[4].rewardButton.interactable = true;
                    }
                }
                break;
            case DailyTaskType.WATCHVIDEO:
                if (DailyTask_WatchVideo < dailyTasks[5].taskTarget)
                {
                    DailyTask_WatchVideo++;
                    if (DailyTask_WatchVideo >= dailyTasks[5].taskTarget)
                    {
                        dailyTaskItemList[5].rewardButton.interactable = true;
                    }
                }
                break;
        }
        
        SetProgressParameters();
    }

    public void OpenDailyTaskPanel() 
    {
        dailyTaskPanel.SetActive(true);
        completeSignal.SetActive(false);
    }
}
