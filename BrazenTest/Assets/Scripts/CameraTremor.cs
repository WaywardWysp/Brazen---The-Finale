using UnityEngine;

public class CameraTremor : MonoBehaviour
{
    public float tremorRadius = 10f;
    public float maxTremorIntensity = 0.1f;
    public float tremorSpeed = 20f;
    public float stepInterval = 1f;

    private Transform playerCamera;
    private Vector3 originalCameraPosition;
    private float stepTimer = 0f;
    private bool isStepUp = true;

    void Start()
    {
        playerCamera = Camera.main.transform;
        originalCameraPosition = playerCamera.localPosition;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerCamera.position);

        if (distance < tremorRadius)
        {
            float intensity = Mathf.Lerp(maxTremorIntensity, 0, distance / tremorRadius);
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                stepTimer = 0f;
                isStepUp = !isStepUp;
            }

            float stepFactor = isStepUp ? 1f : -1f;
            float shakeAmount = intensity * stepFactor;
            float xShake = (Mathf.PerlinNoise(Time.time * tremorSpeed, 0) - 0.5f) * shakeAmount;
            float yShake = (Mathf.PerlinNoise(0, Time.time * tremorSpeed) - 0.5f) * shakeAmount;

            if (stepTimer == 0) // Apply shake only at step intervals
            {
                playerCamera.localPosition = originalCameraPosition + new Vector3(xShake, yShake, 0);
            }
        }
        else
        {
            playerCamera.localPosition = originalCameraPosition;
        }
    }
}
