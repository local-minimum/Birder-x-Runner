using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreBoard : MonoBehaviour {
    [SerializeField] Text Rank;
    [SerializeField] Text Name;
    [SerializeField] Text Score;
    string maxName = "Local Minimum";

    public void SetScore(int rank, string name, string score)
    {
        Rank.text = rank.ToString();
        int l = Mathf.Min(maxName.Length, name.Length);
        Name.text = name.Substring(0, l).PadRight(maxName.Length > l ? maxName.Length - l : 0);
        Score.text = score;
    }
}
