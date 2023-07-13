using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class ScoreCounter : MonoBehaviour
{
    private PlatformGenerator _platformGenerator;
    public int Score { get; private set; } = 0;

    public event UnityAction<int> OnScoreChanged;

    [Inject]
    private void Construct(PlatformGenerator platformGenerator)
    {
        _platformGenerator = platformGenerator;
        _platformGenerator.OnPlatformDeactivate += ScoreCount;
    }

    private void ScoreCount()
    {
        Score++;
        OnScoreChanged?.Invoke(Score);
    }

    private void OnDestroy() => _platformGenerator.OnPlatformDeactivate -= ScoreCount;
}
