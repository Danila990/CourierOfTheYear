using TMPro;
using UnityEngine;
using Zenject;

public class ScoreOutput : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreValue;

    private ScoreCounter _scoreCounter;
    private int _score;

    [Inject]
    private void Construct(ScoreCounter scoreCounter)
    {
        _scoreCounter = scoreCounter;
    }

    private void Start()
    {
        _scoreCounter.OnScoreChanged += ScoreChange;
    }

    private void ScoreChange(int score)
    {
        _scoreValue.text = score.ToString();
        _score = score;
    }

    private void OnDisable()
    {
        _scoreCounter.OnScoreChanged -= ScoreChange;
    }
}
