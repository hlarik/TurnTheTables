using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class TaskManager : MonoBehaviour
<<<<<<< HEAD
{

    /*public struct Tasks
    {
        public string taskName;
        public int taskIndex;
    }*/

    public GameObject TaskUI;
    public Text[] tasks;
    //public Tasks[] tasksInfo;
    //public Animator anim1;
=======
{  
    public GameObject TaskUI;
    public Text[] tasks;
    public Animator anim1;
>>>>>>> parent of fed4654... Revert "Merge branch 'master' of https://github.com/hlarik/TurnTheTables"
    public Text completeness;
    CanvasGroup cg;
    GameObject FadeManager;

<<<<<<< HEAD
    List<String> texts;
    public static int newTask = 0;
    public static int current_index = 0;
    string path = "Assets/Resources/Tasks/tasks.txt";
    GameObject[] images = new GameObject[10];
=======
    string[] texts = new string[20];
    public static int newTask = 0;
    string path = "Assets/Resources/Tasks/tasks.txt";
>>>>>>> parent of fed4654... Revert "Merge branch 'master' of https://github.com/hlarik/TurnTheTables"

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        FindImages();
        FadeManager = GameObject.Find("FadeManager");
        TaskUI.SetActive(false);
        //ReadFileIntoTaskArray();
        UpdateCompleteness();

    }

    void FindImages()
    {
        for(int i = 0; i < tasks.Length; i++)
        {
            images[i] = GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).transform.GetChild(1).gameObject;
            images[i].SetActive(false);
        }
=======
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
>>>>>>> parent of fed4654... Revert "Merge branch 'master' of https://github.com/hlarik/TurnTheTables"
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

<<<<<<< HEAD
    //Read all tasks from file and write into the list
    void ReadFileIntoTaskArray()
    {
        StreamReader input_stream = new StreamReader(path);
        texts = new List<String>();
  
        while (!input_stream.EndOfStream)
        {
            string line = input_stream.ReadLine();
            texts.Add(line);
=======
    void ReadFileIntoTaskArray()
    {
        StreamReader input_stream = new StreamReader(path);
        int i = 0;
        while (!input_stream.EndOfStream)
        {
            string line = input_stream.ReadLine();
            texts[i] = line;
            i++;
>>>>>>> parent of fed4654... Revert "Merge branch 'master' of https://github.com/hlarik/TurnTheTables"
        }

    }

    //Add first tasks
    public void AddFirstTasks()
    {
        for( int i = 0; i < tasks.Length; i++)
        {
            tasks[i].text = texts[newTask];
<<<<<<< HEAD
            images[i].SetActive(false);
            newTask++;
            current_index++;
        }
    }
      
    //Add a new task 
    public void AddNewTask(string taskExplanation)
    {
        if(current_index < 10)
        {
            tasks[current_index].text = taskExplanation;
            images[current_index].SetActive(false);
            //cg = GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(index).transform.GetComponent<CanvasGroup>();
            //FadeManager.GetComponent<FadeManager>().FadeIn(cg);
            //newTask++;
            current_index++;
            //anim1.SetBool("fadeIn", false);
        }

    }

    //Remove a selected task
    public void RemoveTask(string explanation)
    {
        int index = GetIndexOfTask(explanation);
        if( tasks[index].text != null)
        {
            tasks[index].text = null;
            images[index].SetActive(false);
            //AddNewTask();

            for (int i = index; i < current_index; i++)
            {
                tasks[i].text = tasks[i + 1].text;
                bool first = images[i].activeSelf;
                bool second = images[i+1].activeSelf;

                if (first != second)
                {
                    images[i].SetActive(second);
                }
            }
            current_index--;
=======
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
>>>>>>> parent of fed4654... Revert "Merge branch 'master' of https://github.com/hlarik/TurnTheTables"
        }
    }

    //Remove completed tasks
    public void RemoveTasks()
    {
        for( int i = 0; i < tasks.Length; i++)
        {
<<<<<<< HEAD
            if (images[i].activeSelf)
=======
            if (GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).transform.GetChild(1).gameObject.activeSelf)
>>>>>>> parent of fed4654... Revert "Merge branch 'master' of https://github.com/hlarik/TurnTheTables"
            {
                //firstly, remove the task by animation slowly
                //then, add new task in place of removed one
                //cg = GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).transform.GetComponent<CanvasGroup>();
                //FadeManager.GetComponent<FadeManager>().FadeOut(cg);
<<<<<<< HEAD
                images[i].SetActive(false);
                //anim1.SetBool("fadeOut", true);
                current_index--;
=======
                GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
                anim1.SetBool("fadeOut", true);
                AddNewTask(i);
>>>>>>> parent of fed4654... Revert "Merge branch 'master' of https://github.com/hlarik/TurnTheTables"
            }
        }
    }

<<<<<<< HEAD
    public int GetIndexOfTask(string taskExplanation)
    {
        int found = -1;
        for(int i = 0; i < tasks.Length; ++i)
        {
            if (tasks[i].text == taskExplanation)
                found = i;
        }
        return found;
    }

    //Cross check for the completed task
    public void UpdateTask(string taskExplanation)
    {
        int index = GetIndexOfTask(taskExplanation);
        if (index < current_index)
        {
            images[index].SetActive(true);
        }
        UpdateCompleteness();
=======
    //Cross check for the completed task
    public void UpdateTask(int index)
    {
        GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(index).transform.GetChild(1).gameObject.SetActive(true);
>>>>>>> parent of fed4654... Revert "Merge branch 'master' of https://github.com/hlarik/TurnTheTables"
    }

    //Check whether the selected task is completed
    public bool CheckCompleteness(int index)
    {
<<<<<<< HEAD
        return images[index].activeSelf;
=======
        return GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(index).transform.GetChild(1).gameObject.activeSelf;
>>>>>>> parent of fed4654... Revert "Merge branch 'master' of https://github.com/hlarik/TurnTheTables"
    }

    //Update the percent of completed tasks
    public void UpdateCompleteness()
    {
        double completed = 0;
<<<<<<< HEAD
        double total = 0;

        for (int i = 0; i < tasks.Length; i++)
        {
            if (tasks[i].text != "")
            {
                total++;
                Debug.Log("total=" + total);
                if (CheckCompleteness(i))
                {
                    completed++;
                }
            }
        }
        double temp =  (completed / total) * 100;
=======

        for (int i = 0; i < tasks.Length; i++)
        {
            if (CheckCompleteness(i))
            {
                completed++;
            }
        }
        double temp =  (completed / (double) tasks.Length) * 100;
>>>>>>> parent of fed4654... Revert "Merge branch 'master' of https://github.com/hlarik/TurnTheTables"
        completeness.text = temp + "% Complete";
    }
}
