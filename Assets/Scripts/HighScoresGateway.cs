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

    string host = "212.85.82.181";
    string service = "unitysocial";
    string game = "birderxrunner";
    string scoresURI = "http://{0}/{1}/{2}/{3}";

    string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.?!_- 1234567890";

    string ScoresURI(string scoreType)
    {
        return string.Format(scoresURI, host, service, game, scoreType);
    }

    string ScoresURI(string scoreType, int count)
    {
        return string.Format(scoresURI, host, service, game, scoreType) + string.Format("?count={0}", count);
    }

    public void GetHighscores(string scoreType, System.Action<List<ScoreEntry>> callback)
    {
        string uri = ScoresURI(scoreType, nScores);
        Debug.Log(uri);
        UnityWebRequest.Post
        WWW www = WWW.LoadFromCacheOrDownload(uri, cacheIdx);
        StartCoroutine(Downloader(www, callback));
        
    }


    public List<ScoreEntry> PadScoreList(List<ScoreEntry> scores)
    {
        for (int i = scores.Count; i < nScores; i++)
        {
            scores.Add(new ScoreEntry(
                i + 1,
                "...",
                ""
            ));
        }

        return scores;
    }

    IEnumerator<WaitForSeconds> Downloader(WWW www, System.Action<List<ScoreEntry>> callback)
    {
        while (!www.isDone)
        {
            yield return new WaitForSeconds(0.25f);
        }
        callback(ParseList(www.text));
    }

    public string SecureName(string name, int maxLenght)
    {
        string clean = "";
        for (int i = 0; i<name.Length; i++)
        {
            if (allowedCharacters.IndexOf(name[i]) >= 0) {
                clean += name[i];
            }
        }
        return clean.Substring(0, Mathf.Min(clean.Length, maxLenght));
    }

    public ScoreEntry ParseEntry(string line)
    {
        string[] row = line.Split('\t');
        if (row.Length == 3)
        {
            return new ScoreEntry(
                int.Parse(row[0]),
                row[1],
                row[2]
            );
        }
        return new ScoreEntry();
    }

    public List<ScoreEntry> ParseList(string s)
    {
        List<ScoreEntry> results = new List<ScoreEntry>();
        foreach(string line in s.Split('\n'))
        {
            ScoreEntry e = ParseEntry(line);
            if (e.rank != 0)
            {
                results.Add(e);
            }
        }
        return results;
    }
}
