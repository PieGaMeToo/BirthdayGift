using UnityEngine;

public class PulseEmission : MonoBehaviour
{
    public Material mat;             // assign your star material
    public float minIntensity = 0.2f;
    public float maxIntensity = 1f;
    public float speed = 0.3f;

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, t);

        // URP uses HDR emission
        Color baseColor = Color.white * intensity;

        // "_BaseColor" or "_EmissionColor" depends on shader
        mat.SetColor("_EmissionColor", baseColor);
    }
}
