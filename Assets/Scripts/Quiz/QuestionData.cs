using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] replies;

    public int correctReplyIndex;
    public Sprite QuestionImage;
}

[CreateAssetMenu(fileName = "New Category", menuName = "Quiz/Question Data")]
public class QuestionData : ScriptableObject
{
    public string category;
    public Question[] questions;
    public int targetScene; // Tambahkan properti ini untuk menentukan scene tujuan

    public int score;
}
