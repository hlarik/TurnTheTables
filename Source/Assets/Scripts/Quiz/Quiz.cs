using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Soru
{
    [SerializeField]  string _question;
    public string Question { get { return _question; } }

    [SerializeField]  Cevap[] _answer;
    public Cevap[] Answer{ get { return _answer; } }

}

[System.Serializable]
public struct Cevap
{
    [SerializeField]  string _answer;
    public string Answer { get { return _answer; } }

    [SerializeField]  string _feedback;
    public string Feedback { get { return _feedback; } }

}



[CreateAssetMenu(fileName = "New Quiz", menuName = "Quizler/new Quiz")] ///????????
public class Quiz : ScriptableObject
{
    [SerializeField]  string _info = string.Empty;
    public string Info { get { return _info; } }

    [SerializeField] Soru[] _questions = null;
    public Soru[] Sorular { get { return _questions; } }



}
