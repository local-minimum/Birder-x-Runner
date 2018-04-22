using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecords : MonoBehaviour {

    [SerializeField] GameObject recordsBirdingRoot;
    [SerializeField] GameObject recordBirdingGO;    
    [SerializeField] Text recordBirdingText;

    [SerializeField] GameObject recordsBirdRunningRoot;
    [SerializeField] GameObject recordBirdRunningGO;
    [SerializeField] Text recordBirdRunningText;

    [SerializeField] GameObject recordsRunningRoot;
    [SerializeField] GameObject recordRunningGO;
    [SerializeField] Text recordRunningText;

    [SerializeField] Goal goal;

    enum ResultsTypes { Birding, Running, BirdRunning };

    struct RecordText
    {
        public bool isRecord;
        public string text;
        public RecordText(bool isRecord, string text)
        {
            this.isRecord = isRecord;
            this.text = text;
        }
    }

    Dictionary<ResultsTypes, RecordText> texts = new Dictionary<ResultsTypes, RecordText>();

	void Start () {
        recordsBirdingRoot.SetActive(false);
        recordsBirdRunningRoot.SetActive(false);
        recordsRunningRoot.SetActive(false);
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
            string.Format("Score: {0}", score.ToString("000"))
        ));
    }

    public void ShowBirderResult(int score, int record)
    {
        texts.Add(ResultsTypes.Birding, new RecordText(
            score > record,
            string.Format("Score: {0} (Record: {1})", score.ToString("000"), record.ToString("000"))
        ));
    }

    public void ShowRunnerResult(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        texts.Add(ResultsTypes.Running, new RecordText(
            true,
            string.Format("{0}:{1}", minutes.ToString("00"), (time - minutes).ToString("00.000"))
        ));
    }

    public void ShowRunnerResult(float time, float record)
    {
        int recordMinutes = Mathf.FloorToInt(record / 60);
        int minutes = Mathf.FloorToInt(time / 60);
        texts.Add(ResultsTypes.Running, new RecordText(
            time < record,
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
            string.Format("Score: {0}", value.ToString("0.0"))
        ));
    }

    public void ShowBirdRunnerResult(float value, float record)
    {
        texts.Add(ResultsTypes.BirdRunning, new RecordText(
            value > record,
            string.Format("Score: {0} (Record {1})", value.ToString("0.0"), record.ToString("0.0"))
        ));
    }

    float showStart;
    int showIndex;
    IEnumerator<WaitForSeconds> ShowResults()
    {
        ResultsTypes[] typeOrder = {ResultsTypes.Birding, ResultsTypes.BirdRunning, ResultsTypes.Running};
        showIndex = 0;
        RecordText data;
        while (showIndex < typeOrder.Length)
        {
            yield return new WaitForSeconds(0.4f);
        
            switch (typeOrder[showIndex]) {
                case ResultsTypes.Birding:
                    data =  texts[ResultsTypes.Birding];
                    recordBirdingText.text = data.text;
                    recordBirdingGO.SetActive(data.isRecord);
                    recordsBirdingRoot.SetActive(true);
                    break;
                case ResultsTypes.BirdRunning:
                    data = texts[ResultsTypes.BirdRunning];
                    recordBirdRunningText.text = data.text;
                    recordBirdRunningGO.SetActive(data.isRecord);
                    recordsBirdRunningRoot.SetActive(true);
                    break;
                case ResultsTypes.Running:
                    data = texts[ResultsTypes.Running];
                    recordRunningText.text = data.text;
                    recordRunningGO.SetActive(data.isRecord);
                    recordsRunningRoot.SetActive(true);
                    break;
            }
            showIndex++;
        }
    }
}
