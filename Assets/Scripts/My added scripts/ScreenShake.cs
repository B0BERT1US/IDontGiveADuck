using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;

    [Header("Defaults")]
    [Tooltip("Default shake duration if not specified in code.")]
    public float defaultDuration = 0.12f;

    [Tooltip("Default shake strength (position).")]
    public float defaultAmplitude = 0.08f;

    [Tooltip("How quickly shake strength fades over time (1 = linear, higher = faster).")]
    public float damping = 1.5f;

    [Tooltip("How often direction changes (higher = more jitter).")]
    public float frequency = 25f;

    [Header("Rotation Shake")]
    [Tooltip("Enable slight camera rotation shake.")]
    public bool useRotation = true;

    [Tooltip("Max angle offset in degrees when rotation shake is enabled.")]
    public float rotationAmplitude = 4f;

    private Vector3 originalPos;
    private Quaternion originalRot;
    private Coroutine shakeRoutine;

    private void Awake()
    {
        Instance = this;
        originalPos = transform.localPosition;
        originalRot = transform.localRotation;
    }

    public void Shake(float? duration = null, float? amplitude = null)
    {
        float dur = duration ?? defaultDuration;
        float amp = amplitude ?? defaultAmplitude;

        // Guard: never allow zero/neg duration
        if (dur <= 0f) dur = 0.01f;

        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(DoShake(dur, amp));
    }

    private IEnumerator DoShake(float duration, float amplitude)
    {
        float elapsed = 0f;
        Vector3 randomOffset = Vector3.zero;
        float nextChangeTime = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // Pick a new random direction at the chosen frequency
            if (elapsed >= nextChangeTime)
            {
                Vector2 dir = Random.insideUnitCircle.normalized; // safe 2D random
                randomOffset = new Vector3(dir.x, dir.y, 0f) * amplitude;
                nextChangeTime = elapsed + (1f / Mathf.Max(1f, frequency));
            }

            // Clamp base to avoid negative -> NaN
            float baseVal = Mathf.Clamp01(1f - (elapsed / duration));
            float damp = Mathf.Pow(baseVal, Mathf.Max(0.0001f, damping));

            // Position shake
            transform.localPosition = originalPos + randomOffset * damp;

            // Rotation shake
            if (useRotation)
            {
                float rotZ = Random.Range(-rotationAmplitude, rotationAmplitude) * damp;
                transform.localRotation = Quaternion.Euler(0f, 0f, rotZ);
            }

            yield return null;
        }

        // Reset
        transform.localPosition = originalPos;
        transform.localRotation = originalRot;
        shakeRoutine = null;
    }
}