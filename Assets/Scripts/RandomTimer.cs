using System;
using System.Collections;
using UnityEngine;

public class RandomTimer : MonoBehaviour
{
    public event Action TimerExhausted;

    [SerializeField, Min(0f)] private float _maxTime = 3f;
    [SerializeField, Min(0f)] private float _minTime = 1f;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(TimerRoutine());
    }

    private void OnDisable()
    {
        _coroutine = null;
    }

    private IEnumerator TimerRoutine()
    {
        while (enabled)
        {
            float time = UnityEngine.Random.Range(_minTime, _maxTime);
            yield return new WaitForSeconds(time);
            TimerExhausted?.Invoke();
        }
    }

    private void OnValidate()
    {
        if (_minTime >= _maxTime)
            _minTime = _maxTime;
    }
}
