using UnityEngine;
using System;
using Zenject;

public class PlayerGameOver : MonoBehaviour
{
    [SerializeField] private EndGame _endGamePanel;
    [SerializeField] private ScoreOutput _UIpanel;
    [Inject]
    private void Construct(ScoreOutput scoreOutput, EndGame endGamePanel)
    {
        _UIpanel = scoreOutput;
        _endGamePanel = endGamePanel;
    }

    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0;

        _UIpanel.gameObject.SetActive(false);
        _endGamePanel.gameObject.SetActive(true);
    }
}
