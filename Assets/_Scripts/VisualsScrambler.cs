using UnityEngine;

public class VisualsScrambler : MonoBehaviour
{
    [SerializeField] private Transform[] hatOptions;
    [SerializeField] private Material[] colorOptions;
    [SerializeField] private Material[] faceOptions;
    [SerializeField] private SkinnedMeshRenderer mesh;

    private int _hatIndex = -1;
    private int _colorIndex = -1;
    private int _faceIndex = -1;

    private void Start()
    {
        RandomizeVisuals();
    }

    private void OnDisable()
    {
        if (VisualsManager.Instance == null)
            return;

        if (_hatIndex >= 0)
            VisualsManager.Instance.ReleaseHat(_hatIndex);

        if (_colorIndex >= 0)
            VisualsManager.Instance.ReleaseColor(_colorIndex);

        if (_faceIndex >= 0)
            VisualsManager.Instance.ReleaseFace(_faceIndex);
    }

    private void RandomizeVisuals()
    {
        _hatIndex = VisualsManager.Instance.GetUniqueHat(hatOptions.Length);
        _colorIndex = VisualsManager.Instance.GetUniqueColor(colorOptions.Length);
        _faceIndex = VisualsManager.Instance.GetUniqueFace(faceOptions.Length);

        ApplyHat();
        ApplyMaterials();
    }

    private void ApplyHat()
    {
        for (int i = 0; i < hatOptions.Length; i++)
        {
            hatOptions[i].gameObject.SetActive(i == _hatIndex);
        }
    }

    private void ApplyMaterials()
    {
        Material[] materials = mesh.materials;

        materials[0] = colorOptions[_colorIndex];
        materials[1] = faceOptions[_faceIndex];

        mesh.materials = materials;
    }
}