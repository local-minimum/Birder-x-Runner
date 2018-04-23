using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHighscoresLister : MonoBehaviour {

    [SerializeField] string scoreType;
    [SerializeField] HighScoresGateway gateway;
    [SerializeField] UIScoreBoard scoreBoardPrefab;

    List<UIScoreBoard> boards = new List<UIScoreBoard>();
    private void Start()
    {
        LoadScores();    
    }

    public void LoadScores()
    {
        gateway.GetHighscores(scoreType, ListScores, HandleError);
    }

    void ListScores(List<ScoreEntry> scores)
    {
        scores = gateway.PadScoreList(scores);
        int i = 0;
        int l = boards.Count;
        while (i < l) {
            UIScoreBoard board = boards[i];
            board.SetScore(scores[i].rank, scores[i].name, scores[i].score);
            i++;
        }
        while (i < 10)
        {
            UIScoreBoard board = Instantiate(scoreBoardPrefab, transform);
            boards.Add(board);
            board.SetScore(scores[i].rank, scores[i].name, scores[i].score);
            i++;
        }

    }

    void HandleError(string errMsg)
    {
        Debug.LogError(errMsg);
    }
}
