using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class DamageFeedback : MonoBehaviour
{
    public static DamageFeedback Instance { get; private set; }

    [Header("Post-Process Volume")]
    [Tooltip("Volume with Bloom, ChromaticAberration & LensDistortion overrides")]
    [SerializeField] private Volume volume;

    private Bloom _bloom;
    private ChromaticAberration _chroma;
    private LensDistortion _lensDist;

    [Header("Pulse Settings")]
    [Tooltip("Peak values for the pulse")]
    public float bloomPeak = 8f;
    public float chromaPeak = 1f;
    public float distortionPeak = -0.3f;
    [Tooltip("Total duration of the up+down pulse")]
    public float effectTime = 0.4f;

    private float _bloomDefault;
    private float _chromaDefault;
    private float _distDefault;

    [Header("Camera Shake")]
    [Tooltip("How long the shake lasts")]
    public float shakeDuration = 0.25f;
    [Tooltip("Max offset (in world units) from the original camera position")]
    public float shakeMagnitude = 0.2f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // grab the overrides
        if (!volume.profile.TryGet(out _bloom) ||
            !volume.profile.TryGet(out _chroma) ||
            !volume.profile.TryGet(out _lensDist))
        {
            Debug.LogError("DamageFeedback requires Bloom, ChromaticAberration, and LensDistortion in the Volume profile.");
            enabled = false;
            return;
        }

        // cache defaults
        _bloomDefault = _bloom.intensity.value;
        _chromaDefault = _chroma.intensity.value;
        _distDefault = _lensDist.intensity.value;

    }

    public void TriggerFeedback()
    {
        // 1) Pulse post-process
        var seq = DOTween.Sequence();

        // ramp up to peaks
        seq.Append(DOTween.To(() => _bloom.intensity.value,
                             x => _bloom.intensity.value = x,
                             bloomPeak, effectTime * 0.5f));
        seq.Join(DOTween.To(() => _chroma.intensity.value,
                             x => _chroma.intensity.value = x,
                             chromaPeak, effectTime * 0.5f));
        seq.Join(DOTween.To(() => _lensDist.intensity.value,
                             x => _lensDist.intensity.value = x,
                             distortionPeak, effectTime * 0.5f));

        // ramp back down to defaults
        seq.Append(DOTween.To(() => _bloom.intensity.value,
                             x => _bloom.intensity.value = x,
                             _bloomDefault, effectTime * 0.5f));
        seq.Join(DOTween.To(() => _chroma.intensity.value,
                             x => _chroma.intensity.value = x,
                             _chromaDefault, effectTime * 0.5f));
        seq.Join(DOTween.To(() => _lensDist.intensity.value,
                             x => _lensDist.intensity.value = x,
                             _distDefault, effectTime * 0.5f));

        // 2) Shake camera
        StartCoroutine(SimpleCameraShake(shakeDuration, shakeMagnitude));
    }

    private IEnumerator SimpleCameraShake(float duration, float magnitude)
    {
        var camTransform = Camera.main.transform;
        var originalPos = camTransform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = (Random.value * 2f - 1f) * magnitude;
            float y = (Random.value * 2f - 1f) * magnitude;
            camTransform.localPosition = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        camTransform.localPosition = originalPos;
    }
}
