using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Transform target;
    
    private Vector3 speed;
    private readonly Vector3 offsetPosition = new Vector3(2.1f, 6.875f, -1.68f);

    void Awake()
    {
        TryGetComponent(out cam);
    }

    private void OnEnable()
        {
        transform.parent.position = target.position + offsetPosition;
        }

    void Update()
    {
        transform.parent.position = Vector3.SmoothDamp(transform.parent.position, target.position + offsetPosition, ref speed, 0.3f, 32, Time.deltaTime);

        if (Input.GetMouseButtonDown(1))
        {
            CameraShake(0.05f, 0.2f);
        }
    }

    //Al explotar Bomba, enemigo golpear y morir

    public void CameraShake(float intensity, float duration)
    {
        StartCoroutine(Co_CameraShake(intensity, duration));
    }

    IEnumerator Co_CameraShake(float intensity, float duration)
    {
        float timeRemaining = duration;

        while (timeRemaining > 0)
        {
            cam.transform.localPosition = Random.insideUnitSphere * intensity;

            timeRemaining -= Time.unscaledDeltaTime;
            yield return null;
        }
        cam.transform.localPosition = Vector3.zero;
    }

}
