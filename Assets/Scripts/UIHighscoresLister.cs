using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHighscoresLister : MonoBehaviour {

    [SerializeField] string scoreType;
    [SerializeField] ScoresSort sort;
    [SerializeField] HighScoresGateway gateway;
    [SerializeField] UIScoreBoard scoreBoardPrefab;

    private void Start()
    {
        gateway.GetHighscores(scoreType, ListScores, HandleError);
    }

    void ListScores(List<ScoreEntry> scores)
    {
        scores = gateway.PadScoreList(scores);
        for (int i = 0, l = scores.Count; i < l; i++)
        {
            UIScoreBoard board = Instantiate(scoreBoardPrefab, transform);
            board.SetScore(scores[i].rank, scores[i].name, scores[i].score);
        }

    }

    void HandleError(string errMsg)
    {
        Debug.LogError(errMsg);
    }
}
