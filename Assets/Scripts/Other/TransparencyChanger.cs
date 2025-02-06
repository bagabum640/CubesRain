using System.Collections;
using UnityEngine;

public class TransparencyChanger : MonoBehaviour
{
    private const float DefaultAlpha = 1f;

    [SerializeField] private MeshRenderer _renderer;

    private float _delay;

    private void OnEnable() =>
        StartCoroutine(SmoothDisappearance());

    private IEnumerator SmoothDisappearance()
    {
        float timeRemaining = _delay;

        while (timeRemaining > 0)
        {
            SetAlpha(timeRemaining / _delay);

            timeRemaining -= Time.deltaTime;

            yield return null;
        }
    }

    public void SetDelay(float delay) =>
        _delay = delay;

    public void ResetAlpha() =>
        SetAlpha(DefaultAlpha);

    private void SetAlpha(float alpha)
    {
        Color color = _renderer.material.color;
        color.a = alpha;
        _renderer.material.color = color;
    }
}