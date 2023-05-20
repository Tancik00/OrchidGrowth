using System.Collections;
using System.Collections.Generic;
using MergeFactory;
using UnityEngine;
using UnityEngine.UI;

public class DailyTaskItem : MonoBehaviour
{
   public Text taskName;
   public Image taskImage;
   public Text progressText;
   public Image progressBar;
   public Button rewardButton;
   public Text rewardValue;
   public GameObject completedButton;
   public int ID;

   public void GetReward()
   {
      DataProvider.instance.gameData.playerCoins += DailyTasksController.instance.rewardValue;
      PlayerPrefs.SetInt("taskCompleted" + ID, 1);
      completedButton.SetActive(true);
   }
}
