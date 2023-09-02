using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeShake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageShake(float duration, float magnitude)
    {
        StartCoroutine(DoDamageShake(duration,magnitude));
    }

    private IEnumerator DoDamageShake(float duration, float magnitude)
    {
        var pos = transform.localPosition;

        var elapsed = 0f;

        while (elapsed < duration)
        {
            var x = pos.x + Random.Range(-1.0f, 1.0f) * magnitude;
            var y = pos.y + Random.Range(-1.0f, 1.0f) * magnitude;

            transform.localPosition = new Vector3(x, y, pos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = pos;
    }
}
