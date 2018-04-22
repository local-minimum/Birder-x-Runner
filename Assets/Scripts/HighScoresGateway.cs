using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScoresSort { Ascending, Descending };
public struct ScoreEntry
{
    public int rank;
    public string name;
    public string score;

    public int ScoreAsInt
    {
        get
        {
            return int.Parse(score);
        }
    }

    public float ScoreAsFloat
    {
        get
        {
            return float.Parse(score);
        }
    }

    public ScoreEntry(int rank, string name, string score)
    {
        this.rank = rank;
        this.name = name;
        this.score = score;
    }
}

public class HighScoresGateway : MonoBehaviour {

    [SerializeField]
    int nScores = 10;

    string host = "localhost";
    string service = "unitysocial";
    string game = "birderxrunner";
    string scoresURI = "http://{0}/{1}/{2}/{3}";

    string ScoresURI(string scoreType)
    {
        return string.Format(scoresURI, host, service, game, scoreType);
    }

    string ScoresURI(string scoreType, int count)
    {
        return string.Format(scoresURI, host, service, game, scoreType) + string.Format("?count={0}", count);
    }

    public List<ScoreEntry> GetHighscores(string scoreType)
    {
        string uri = ScoresURI(scoreType, nScores);
        Debug.Log(uri);
        List<ScoreEntry> scores = new List<ScoreEntry>();

        for(int i=scores.Count; i < nScores; i++)
        {
            scores.Add(new ScoreEntry(
                i + 1,
                "...",
                ""
            ));
        }

        return scores;
    }
}
