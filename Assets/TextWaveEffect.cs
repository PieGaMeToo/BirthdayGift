using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TextWaveEffect : MonoBehaviour
{
    [Header("Wave Motion Settings")]
    public float amplitude = 5f;
    public float frequency = 5f;
    public float speed = 2f;

    [Header("Color Wave Settings")]
    public float colorSpeed = 2f;
    public float colorFrequency = 1f;

    private TMP_Text textMesh;

    void Awake()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    void LateUpdate()
    {
        textMesh.ForceMeshUpdate();

        var textInfo = textMesh.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;
            Vector3[] verts = textInfo.meshInfo[materialIndex].vertices;
            Color32[] colors = textInfo.meshInfo[materialIndex].colors32;

            float wave = Mathf.Sin(Time.time * speed + i * frequency) * amplitude;
            for (int v = 0; v < 4; v++)
                verts[vertexIndex + v].y += wave;

            float colorPhase = Time.time * colorSpeed + i * colorFrequency;
            Color c = Color.HSVToRGB((Mathf.Sin(colorPhase) * 0.5f + 0.5f), 1f, 1f);
            for (int v = 0; v < 4; v++)
                colors[vertexIndex + v] = c;
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            meshInfo.mesh.colors32 = meshInfo.colors32;
            textMesh.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
