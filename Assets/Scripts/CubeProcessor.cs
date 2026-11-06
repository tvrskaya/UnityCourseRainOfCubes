using UnityEngine;

public class CubeProcessor : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private RandomTimer _timer;

    [SerializeField] private float _creationRadius = 10f;

    private float _maxRemoveTime = 5f;
    private float _minRemoveTime = 2f;

    private void OnEnable()
    {
        _timer.TimerExhausted += ProcessTimerExhausted;
    }

    private void OnDisable()
    {
        _timer.TimerExhausted -= ProcessTimerExhausted;
    }

    private void ProcessCubeTouched(Cube cube)
    {
        if (cube.TryGetComponent<ProcessedCube>(out _) == false)
        {
            cube.gameObject.AddComponent<ProcessedCube>();

            cube.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();

            cube.PlatformTouched -= ProcessCubeTouched;

            float timeToRemove = Random.Range(_minRemoveTime, _maxRemoveTime);

            _spawner.RemoveDelayed(cube, timeToRemove);
        }
    }

    private void ProcessTimerExhausted()
    {
        var randomPositionInCircle = Random.insideUnitCircle * _creationRadius;
        Vector3 position = new Vector3(randomPositionInCircle.x, 0, randomPositionInCircle.y) + transform.position;

        var cube = _spawner.SpawnInPosition(position);
        cube.PlatformTouched += ProcessCubeTouched;
    }
}
