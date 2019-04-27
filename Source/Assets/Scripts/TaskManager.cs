using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class TaskManager : MonoBehaviour
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
    public Text completeness;
    CanvasGroup cg;
    GameObject FadeManager;

    List<String> texts;
    public static int newTask = 0;
    public static int current_index = 0;
    string path = "Assets/Resources/TaskToDo/tasks.txt";
    GameObject[] images = new GameObject[10];

    // Start is called before the first frame update
    void Start()
    {
        TaskUI.SetActive(true);
        FindImages();
        FadeManager = GameObject.Find("FadeManager");
        ReadFileIntoTaskArray();
        UpdateCompleteness();
    }

    void FindImages()
    {
        for (int i = 0; i < tasks.Length; i++)
        {
            images[i] = GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).transform.GetChild(1).gameObject;
            images[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.T))
        {
            TaskUI.SetActive(!TaskUI.activeSelf);
        }*/
        /*if (TaskUI.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }*/

    }

    public void ActivateDeactivateTaskManager()
    {
        //TaskUI.SetActive(!TaskUI.activeSelf);
        TaskUI.transform.GetChild(0).GetComponent<Animator>().SetBool("isOpen", true);
    }

    public void DeactivateTaskManager()
    {
        //TaskUI.SetActive(!TaskUI.activeSelf);
        TaskUI.transform.GetChild(0).GetComponent<Animator>().SetBool("isOpen", false);
    }

    //Read all tasks from file and write into the list
    void ReadFileIntoTaskArray()
    {
        /*StreamReader input_stream = new StreamReader(path);
        texts = new List<String>();

        while (!input_stream.EndOfStream)
        {
            string line = input_stream.ReadLine();
        }*/
        string line = null;
        StreamReader reader = new StreamReader(path);
        while ((line = reader.ReadLine()) != null)
        {
            if (current_index < 10)
            {
                tasks[current_index].text = line;
                current_index++;
            }
        }

    }

    //Add first tasks
    public void AddFirstTasks()
    {
        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i].text = texts[newTask];
            images[i].SetActive(false);
            newTask++;
            current_index++;
        }
    }

    //Add a new task 
    public void AddNewTask(string taskExplanation)
    {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
        {
            file.WriteLine(taskExplanation);
        }
            if (current_index < 10)
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

    public Text[] GetAllTasks()
    {
        return tasks;
    }

    //Remove a selected task
    public void RemoveTask(string explanation)
    {
        string line = null;
        var fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        //var writer = new StreamWriter(fs);
        var reader = new StreamReader(fs);
        List<String> lines = new List<string>();
        while ((line = reader.ReadLine()) != null)
        {
            if (String.Compare(line, explanation) == 0)
                continue;

            lines.Add(line);
            //writer.WriteLine(line);
        }
        reader.Close();
        File.WriteAllLines(path, lines.ToArray());


        /*using (StreamReader reader = new StreamReader(path))
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (String.Compare(line, explanation) == 0)
                        continue;

                    writer.WriteLine(line);
                }
            }
        }*/
        int index = GetIndexOfTask(explanation);
        if (tasks[index].text != null)
        {
            tasks[index].text = null;
            images[index].SetActive(false);
            //AddNewTask();

            for (int i = index; i < current_index; i++)
            {
                tasks[i].text = tasks[i + 1].text;
                bool first = images[i].activeSelf;
                bool second = images[i + 1].activeSelf;

                if (first != second)
                {
                    images[i].SetActive(second);
                }
            }
            current_index--;
        }
    }

    //Remove completed tasks
    public void RemoveTasks()
    {
        for (int i = 0; i < tasks.Length; i++)
        {
            if (images[i].activeSelf)
            {
                //firstly, remove the task by animation slowly
                //then, add new task in place of removed one
                //cg = GameObject.Find("TaskManagerCanvas").transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).transform.GetComponent<CanvasGroup>();
                //FadeManager.GetComponent<FadeManager>().FadeOut(cg);
                images[i].SetActive(false);
                //anim1.SetBool("fadeOut", true);
                current_index--;
            }
        }
    }

    public int GetIndexOfTask(string taskExplanation)
    {
        int found = -1;
        for (int i = 0; i < tasks.Length; ++i)
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
    }

    //Check whether the selected task is completed
    public bool CheckCompleteness(int index)
    {
        return images[index].activeSelf;
    }

    //Update the percent of completed tasks
    public void UpdateCompleteness()
    {
        double completed = 0;
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
        double temp = (completed / total) * 100;
        completeness.text = temp + "% Complete";
    }
}
