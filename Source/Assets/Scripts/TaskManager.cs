using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class TaskManager : MonoBehaviour
{  
    public GameObject TaskUI;
    public Text[] tasks;
    public Animator anim1;
    public Text completeness;
    CanvasGroup cg;
    GameObject FadeManager;

    List<String> texts;
    public static int newTask = 0;
    public static int current_index = 0;
    string path = "Assets/Resources/Tasks/tasks.txt";

    // Start is called before the first frame update
    void Start()
    {
        FadeManager = GameObject.Find("FadeManager");
        TaskUI.SetActive(false);
        ReadFileIntoTaskArray();
        UpdateCompleteness();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TaskUI.SetActive(!TaskUI.activeSelf);
        }
        if (TaskUI.activeSelf)
        {
            Time.timeScale = 0;
        } 
        else
        {
            Time.timeScale = 1;
        }

    }

    //Read all tasks from file and write into the list
    void ReadFileIntoTaskArray()
    {
        StreamReader input_stream = new StreamReader(path);
        texts = new List<String>();
  
        while (!input_stream.EndOfStream)
        {
            string line = input_stream.ReadLine();
            texts.Add(line);
        }

    }

    //Add first tasks
    public void AddFirstTasks()
    {
        for( int i = 0; i < tasks.Length; i++)
        {
            tasks[i].text = texts[newTask];
            GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
            newTask++;
            current_index++;
        }
        GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(7).transform.GetChild(1).gameObject.SetActive(true);
    }
      
    //Add a new task 
    public void AddNewTask()
    {
        if(current_index < 10)
        {
            tasks[current_index].text = texts[newTask];
            GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(current_index).transform.GetChild(1).gameObject.SetActive(false);
            //cg = GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(index).transform.GetComponent<CanvasGroup>();
            //FadeManager.GetComponent<FadeManager>().FadeIn(cg);
            newTask++;
            current_index++;
            anim1.SetBool("fadeIn", false);
        }

    }

    //Remove a selected task
    public void RemoveTask(int index)
    {
        if( tasks[index].text != null)
        {
            tasks[index].text = null;
            GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(index).transform.GetChild(1).gameObject.SetActive(false);
            //AddNewTask();

            for (int i = index; i < current_index; i++)
            {
                tasks[i].text = tasks[i + 1].text;
                bool first = GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).transform.GetChild(1).gameObject.activeSelf;
                bool second = GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(i + 1).transform.GetChild(1).gameObject.activeSelf;

                if (first != second)
                {
                    GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(index).transform.GetChild(1).gameObject.SetActive(second);
                }
            }
            current_index--;
        }

    }

    //Remove completed tasks
    public void RemoveTasks()
    {
        for( int i = 0; i < tasks.Length; i++)
        {
            if (GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).transform.GetChild(1).gameObject.activeSelf)
            {
                //firstly, remove the task by animation slowly
                //then, add new task in place of removed one
                //cg = GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).transform.GetComponent<CanvasGroup>();
                //FadeManager.GetComponent<FadeManager>().FadeOut(cg);
                GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
                anim1.SetBool("fadeOut", true);
                current_index--;
                AddNewTask();
            }
        }
    }

    //Cross check for the completed task
    public void UpdateTask(int index)
    {
        if(index < current_index)
        {
            GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(index).transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    //Check whether the selected task is completed
    public bool CheckCompleteness(int index)
    {
        return GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(index).transform.GetChild(1).gameObject.activeSelf;
    }

    //Update the percent of completed tasks
    public void UpdateCompleteness()
    {
        double completed = 0;

        for (int i = 0; i < tasks.Length; i++)
        {
            if (CheckCompleteness(i))
            {
                completed++;
            }
        }
        double temp =  (completed / (double) tasks.Length) * 100;
        completeness.text = temp + "% Complete";
    }
}
