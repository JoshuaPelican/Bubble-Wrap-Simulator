using UnityEngine;
using TMPro;
using ScriptableObjectArchitecture;

public class VariableDisplay : MonoBehaviour
{
    [Header("Display Settings")]
    [SerializeField] BaseVariable Variable;
    [SerializeField] string Prefix;
    [SerializeField] string Suffix;

    TextMeshProUGUI variableTextMesh;

    private void Awake()
    {
        variableTextMesh = GetComponent<TextMeshProUGUI>();

        OnValueChanged();
    }

    private void OnEnable()
    {
        Variable.AddListener(OnValueChanged);
    }

    void OnValueChanged()
    {
        variableTextMesh.SetText(Prefix + Variable.ToString() + Suffix);
    }
}
