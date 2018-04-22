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
        List<ScoreEntry> scores = gateway.GetHighscores(scoreType);
        for (int i=0, l=scores.Count; i<l; i++)
        {
            UIScoreBoard board = Instantiate(scoreBoardPrefab, transform);
            board.SetScore(scores[i].rank, scores[i].name, scores[i].score);
        }
    }
}
