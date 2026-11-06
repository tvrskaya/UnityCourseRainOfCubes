using System;
using UnityEngine;

public class CubeToucher : MonoBehaviour
{
    public event Action<Cube> CubeTouched;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Cube>(out var cube))
        {
            CubeTouched?.Invoke(cube);
        }
    }
}
