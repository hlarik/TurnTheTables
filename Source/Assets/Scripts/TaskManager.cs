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

    string[] texts = new string[20];
    public static int newTask = 0;
    string path = "Assets/Resources/Tasks/tasks.txt";

    // Start is called before the first frame update
    void Start()
    {
        FadeManager = GameObject.Find("FadeManager");
        TaskUI.SetActive(false);
        ReadFileIntoTaskArray();
        //AddFirstTasks();
        //AddNewTask(newTask);
        //AddNewTask(newTask);
        //AddNewTask(newTask);
        //RemoveTask(1);
        //UpdateTask(0);
        //UpdateCompleteness();
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

    void ReadFileIntoTaskArray()
    {
        StreamReader input_stream = new StreamReader(path);
        int i = 0;
        while (!input_stream.EndOfStream)
        {
            string line = input_stream.ReadLine();
            texts[i] = line;
            i++;
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
        }
        GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(7).transform.GetChild(1).gameObject.SetActive(true);
    }
      
    //Add a new task 
    public void AddNewTask(int index)
    {
        tasks[index].text = texts[newTask];
        GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(index).transform.GetChild(1).gameObject.SetActive(false);
        //cg = GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(index).transform.GetComponent<CanvasGroup>();
        //FadeManager.GetComponent<FadeManager>().FadeIn(cg);
        newTask++;
        anim1.SetBool("fadeIn", false);
    }

    //Remove a selected task
    public void RemoveTask(int index)
    {
        if( tasks[index].text != null)
        {
            tasks[index].text = null;
            GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(index).transform.GetChild(1).gameObject.SetActive(false);
            AddNewTask(index);
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
                AddNewTask(i);
            }
        }
    }

    //Cross check for the completed task
    public void UpdateTask(int index)
    {
        GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(index).transform.GetChild(1).gameObject.SetActive(true);
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
