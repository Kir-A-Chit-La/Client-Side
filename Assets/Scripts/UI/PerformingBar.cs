using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformingBar : MonoBehaviour
{
    private ProgressBar _progressBar;
    private UITweener _tweener;
    private float _duration;
    private float _startOffset;
    private float _currentValue;

    public void SetPerformingTime(float duration, float startOffset = 0f)
    {
        _duration = duration;
        _startOffset = startOffset;
    }
    private void Awake()
    {
        _progressBar = GetComponent<ProgressBar>();
        _tweener = GetComponent<UITweener>();
    }
    private void OnEnable()
    {
        StartCoroutine(Increment());
    }
    private IEnumerator Increment()
    {
        _currentValue = _startOffset;
        while(_currentValue < _duration)
        {
            _currentValue += Time.deltaTime;
            _progressBar.SetProgress(_currentValue / _duration);
            yield return null;
        }

        _tweener.Disable();
    }
}
