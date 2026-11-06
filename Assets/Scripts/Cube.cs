using UnityEngine;
using System;

public class Cube : MonoBehaviour
{
    public event Action<Cube> PlatformTouched;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out _))
        {
            PlatformTouched?.Invoke(this);
        }
    }
}
