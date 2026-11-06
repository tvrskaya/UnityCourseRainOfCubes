using UnityEngine;

public class CubeProcessor : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private CubeToucher _toucher;
    [SerializeField] private RandomTimer _timer;

    [SerializeField] private float _creationRadius = 10f;

    private float _maxRemoveTime = 5f;
    private float _minRemoveTime = 2f;

    private void OnEnable()
    {
        _toucher.CubeTouched += ProcessCubeTouched;
        _timer.TimerExhausted += ProcessTimerExhausted;
    }

    private void OnDisable()
    {
        _toucher.CubeTouched -= ProcessCubeTouched;
        _timer.TimerExhausted -= ProcessTimerExhausted;
    }

    private void ProcessCubeTouched(Cube cube)
    {
        if (cube.TryGetComponent<ProcessedCube>(out _) == false)
        {
            cube.gameObject.AddComponent<ProcessedCube>();

            cube.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();

            float timeToRemove = Random.Range(_minRemoveTime, _maxRemoveTime);

            _spawner.RemoveDelayed(cube, timeToRemove);
        }
    }

    private void ProcessTimerExhausted()
    {
        Vector3 position = Random.insideUnitCircle * _creationRadius;
        position += transform.position;

        _spawner.SpawnInPosition(position);
    }
}
