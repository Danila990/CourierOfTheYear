using TMPro;
using UnityEngine;
using Zenject;

public class EndGame : MonoBehaviour
{
    const string KEY_RECORD = "record";

    [SerializeField]
    private TMP_Text _recordValue;
    [SerializeField]
    private TMP_Text _scoreValue;

    private ScoreCounter _scoreCounter;

    private int _score;

    [Inject]
    private void Construct(ScoreCounter scoreCounter)
    {
        _scoreCounter = scoreCounter;
    }

    private void OnEnable()
    {
        _score = _scoreCounter.Score;
        SetScore();
        SetRecord();
    }

    private void SetRecord()
    {
        int record = SaveLoaderRecord.Load(KEY_RECORD);
        if (record >= _score)
        {
            _recordValue.text = record.ToString();
        }
        else
        {
            _recordValue.text = _score.ToString();
            SaveLoaderRecord.Save(KEY_RECORD, _score);
        }
    }

    private void SetScore()
    {
        _scoreValue.text = _score.ToString();
    }
}
