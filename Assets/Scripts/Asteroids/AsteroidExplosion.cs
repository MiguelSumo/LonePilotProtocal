using System.Collections;
using UnityEngine;

public class AsteroidExplosion : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private float duration = 0.3f;
    [SerializeField] private float maxScale = 2.5f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        float time = 0f;
        Color startColor = sr.color;
        Vector3 startScale = Vector3.one;

        while (time < duration)
        {
            float t = time / duration;

            // Scale up
            transform.localScale = Vector3.Lerp(startScale, startScale * maxScale, t);

            // Fade out
            sr.color = new Color(startColor.r, startColor.g, startColor.b, 1f - t);

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}

