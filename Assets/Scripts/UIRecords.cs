using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecords : MonoBehaviour {

    [SerializeField] GameObject recordsRoot;
    [SerializeField] GameObject recordGO;
    [SerializeField] Text text;
    [SerializeField] Text title;
    [SerializeField] Goal goal;

    enum ResultsTypes { Birding, Running, BirdRunning };

    struct RecordText
    {
        public bool isRecord;
        public string title;
        public string text;
        public RecordText(bool isRecord, string title, string text)
        {
            this.isRecord = isRecord;
            this.title = title;
            this.text = text;
        }
    }

    Dictionary<ResultsTypes, RecordText> texts = new Dictionary<ResultsTypes, RecordText>();

	void Start () {
        recordsRoot.SetActive(false);
	}

    private void OnEnable()
    {
        goal.OnRunGoal += HandleRunEvent;
    }

    private void OnDisable()
    {
        goal.OnRunGoal -= HandleRunEvent;
    }

    private void HandleRunEvent(RunPhase phase, float time)
    {
        if (phase == RunPhase.Goal)
        {
            StartCoroutine(ShowResults());
        }
    }

    public void ShowBirderResult(int score)
    {       
        texts.Add(ResultsTypes.Birding, new RecordText(
            true,
            "Birding",
            string.Format("Score: {0}", score.ToString("000"))
        ));
    }

    public void ShowBirderResult(int score, int record)
    {
        texts.Add(ResultsTypes.Birding, new RecordText(
            score > record,
            "Birding",
            string.Format("Score: {0} (Record: {1})", score.ToString("000"), record.ToString("000"))
        ));
    }

    public void ShowRunnerResult(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        texts.Add(ResultsTypes.Running, new RecordText(
            true,
            "Running",
            string.Format("{0}:{1}", minutes.ToString("00"), (time - minutes).ToString("00.000"))
        ));
    }

    public void ShowRunnerResult(float time, float record)
    {
        int recordMinutes = Mathf.FloorToInt(record / 60);
        int minutes = Mathf.FloorToInt(time / 60);
        texts.Add(ResultsTypes.Running, new RecordText(
            time < record,
            "Running",
            string.Format(
                "{0}:{1} (Record {2}:{3})",
                minutes.ToString("00"),
                (time - minutes).ToString("00.000"),
                recordMinutes.ToString("00"),
                (record - recordMinutes).ToString("00.000"))));
    }

    public void ShowBirdRunnerResult(float value)
    {        
        texts.Add(ResultsTypes.BirdRunning, new RecordText(
            true,
            "Birding x Running",
            string.Format("Score: {0}", value.ToString("00.0"))
        ));
    }

    public void ShowBirdRunnerResult(float value, float record)
    {
        texts.Add(ResultsTypes.BirdRunning, new RecordText(
            value > record,
            "Birding x Running",
            string.Format("Score: {0} (Record {1})", value.ToString("00.0"), record.ToString("00.0"))
        ));
    }

    IEnumerator<WaitForSeconds> ShowResults()
    {
        ResultsTypes[] typeOrder = {ResultsTypes.Birding, ResultsTypes.BirdRunning, ResultsTypes.Running};
        int showIndex = -1;
        bool waiting = false;
        float showStart = Time.time;
        while (true)
        {
            if (waiting)
            {
                if (Input.anyKeyDown && (showIndex > 0 || Time.time - showStart > 1))
                {
                    waiting = false;
                }
            } else
            {
                int nextIndex = showIndex + 1;
                if (nextIndex == 3)
                {
                    break;
                }
                if (texts.ContainsKey(typeOrder[nextIndex]))
                {
                    waiting = true;
                    var data =  texts[typeOrder[nextIndex]];
                    title.text = data.title;
                    text.text = data.text;
                    recordGO.SetActive(data.isRecord);
                    showIndex++;
                    if (nextIndex == 0) recordsRoot.SetActive(true);
                }
            } 
            yield return new WaitForSeconds(0.02f);
        }
        recordsRoot.SetActive(false);
    }
}
