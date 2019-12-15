using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FruitScapes.Extensions;
using FruitScapes.Data;
using FruitScapes.Components;
using FruitScapes.Events;
using UnityEngine.UI;
using FruitScapes.UI;

namespace FruitScapes.Task
{
    public class TaskManager : MonoBehaviour
    {
        [SerializeField] private int taskCount;
        [SerializeField] private int taskCompliteCount;
        [SerializeField] private int moveCount;
        [SerializeField] private Text moveCountText;
        [SerializeField] private GameObject taskPrefab;
        [SerializeField] private Transform taskParent;
        [SerializeField] private ObjectsData objData;
        [SerializeField] private GameUI myUI;
        

        private List<Task> taskList;
        private List<int> useList;

        private void Awake()
        {
            taskList = new List<Task>();
            useList = new List<int>();
            EventHolder.destroyFruits.AddListener(FillTask);
            moveCountText.text = moveCount.ToString();
            InitTask();
        }

        private void InitTask()
        {
            for (int i = 0; i < taskCount; i++)
            {
                taskList.Add(Instantiate(taskPrefab, taskParent.transform).GetComponent<Task>());
            }

            foreach (Task taskCode in taskList)
            {
                ObjectAppearance apperiance = objData.GetRandomAppearance();
                while (useList.Contains(apperiance.Id))
                    apperiance = objData.GetRandomAppearance();
                useList.Add(apperiance.Id);
                taskCode.Id = apperiance.Id;
                taskCode.SetSprite(apperiance.Sprite);
                taskCode.Init(taskCompliteCount);
            }
        }

        public void FillTask(List<Combine> combineList)
        {
            int complieteCount = 0;
            for (int i = 0; i < combineList.Count; i++)
            {
                complieteCount = 0;
                foreach (Task taskCode in taskList)
                {
                    if (taskCode.Id == combineList[i].Id)
                    {
                        taskCode.UpCount();
                    }
                    if (taskCode.iCompliete)
                    {
                        complieteCount++;
                    }
                }
            }
            if (complieteCount >= taskCount)
            {
                myUI.EndGame(true);
                return;
            }
            if (moveCount <= 0)
            {
                myUI.EndGame(false);
            }
        }

        public void MadeMove()
        {
            moveCount--;
            moveCountText.text = moveCount.ToString();
        }

    }
}
