using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISubmitHighscore : MonoBehaviour {
    [SerializeField]
    HighScoresGateway gateway;

    [SerializeField]
    InputField playerName;

    [SerializeField]
    Button btn;

    [SerializeField]
    Text status;

    string refName = "Local Minimum";

    private void Start()
    {
        if (GeneralManager.CanSubmit)
        {
            float duration = GeneralManager.RecentRunTime;
            int minutes = Mathf.FloorToInt(duration / 60);

            status.text = string.Format("B: {0}\nBxR: {1}\nR: {2}:{3}",
                GeneralManager.RecentRunBirding.ToString("0"),
                GeneralManager.RecentRunBirdingXRunning.ToString("0.00"),
                minutes.ToString("00"),
                (duration - minutes).ToString("00.00")
            );
        }
    }

    public void OnNameChange()
    {
        playerName.text = gateway.SecureName(playerName.text, refName.Length);
        btn.interactable = playerName.text.Trim().Length > 0;
    }

    

    public void Submit()
    {
        if (GeneralManager.CanSubmit) {
            playerName.interactable = false;
            btn.interactable = false;
            RunResult result = GeneralManager.GetSubmiss();
            if (result.birdingScore > 0)
            {
                gateway.PostResult(
                    "birding",
                    playerName.text,
                    result.birdingScore.ToString(),
                    e => SetOwnPosition("birding", e[0].rank),
                    e => Debug.LogError(e)
                );
            }
            if (result.birdingSpeed > 0)
            {
                gateway.PostResult(
                    "birdingxrunning",
                    playerName.text,
                    result.birdingSpeed.ToString("0.00"),
                    e => SetOwnPosition("birdingxrunning", e[0].rank),
                    e => Debug.LogError(e)
                );
            }
            gateway.PostResult(
                "running",
                playerName.text,
                result.time.ToString("0.00"),
                e => SetOwnPosition("running", e[0].rank),
                e => Debug.LogError(e)
            );
        }
    }

    Dictionary<string, int> ranks = new Dictionary<string, int>();
    [SerializeField] UIHighscoresLister birdingScores;
    [SerializeField] UIHighscoresLister birdingxrunningScores;
    [SerializeField] UIHighscoresLister runningScores;

    void SetOwnPosition(string scoreType, int rank)
    {
        ranks[scoreType] = rank;
        if (rank <= 10)
        {
            if (scoreType == "running")
            {
                runningScores.LoadScores();
            } else if (scoreType == "birding")
            {
                birdingScores.LoadScores();
            } else
            {
                birdingxrunningScores.LoadScores();
            }
        }
        status.text = GetRankMsg();
    }

    string GetRankMsg()
    {
        string msg = "";
        if (ranks.ContainsKey("birding"))
        {
            msg += string.Format("B: #{0}", ranks["birding"]);
        }
        if (ranks.ContainsKey("birdingxrunning"))
        {
            msg += string.Format("\nB+R: #{0}", ranks["birdingxrunning"]);
        }
        if (ranks.ContainsKey("running"))
        {
            msg += string.Format("\nR: #{0}", ranks["running"]);
        }
        return msg;
    }
}
