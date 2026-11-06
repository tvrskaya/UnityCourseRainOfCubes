using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<Cube> _cubesPool;

    public void Awake()
    {
        _cubesPool = new ObjectPool<Cube>(
            createFunc: () => Object.Instantiate(_cubePrefab),
            actionOnGet: (Cube cube) => ActionOnGet(cube.gameObject),
            actionOnRelease: (Cube cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (Cube cube) => Object.Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    public void SpawnInPosition(Vector3 position)
    {
        var cube = _cubesPool.Get();
        cube.transform.position = position;
    }

    public void Remove(Cube cube)
    {
        _cubesPool.Release(cube);
    }

    public void RemoveDelayed(Cube cube, float delay)
    {
        StartCoroutine(RemoveDelayedRoutine(cube, delay));
    }

    private IEnumerator RemoveDelayedRoutine(Cube cube, float delay)
    {
        yield return new WaitForSeconds(delay);
        _cubesPool.Release(cube);
    }

    private void ActionOnGet(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        obj.SetActive(true);
        obj.GetComponent<Renderer>().material.color = _cubePrefab.GetComponent<Renderer>().material.color;
    }

}
