using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newShake", menuName = "Utility/Camera Shake")]
public class CameraShake : ScriptableObject
{
    [Range(0.1f, 0.9f)]
    [SerializeField] float duration;
    [Range(0.1f, 0.9f)]
    [SerializeField] float magnitude;
    [SerializeField] OptionData data;
    public IEnumerator Shake()
    {
        Vector3 originalPos = FindObjectOfType<Camera>().transform.position;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude * data.Shake;
            float y = Random.Range(-1f, 1f) * magnitude * data.Shake;

            Camera.main.transform.position += new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.localPosition = originalPos;
    }

}
