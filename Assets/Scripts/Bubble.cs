using UnityEngine;
using UnityEngine.EventSystems;
using ScriptableObjectArchitecture;

public class Bubble : MonoBehaviour
{
    [Header("Bubble Settings")]
    [SerializeField] int ClicksToPop = 1;
    [SerializeField] bool IsClickable = true;
    [SerializeField] GameObject NextObject;

    int clicks = 0;

    [Header("Variables")]
    [SerializeField] IntVariable TotalPops;

    [Header("Audio Settings")]
    [SerializeField] AudioClip ClickClip;
    [SerializeField] AudioClip PopClip;

    private void OnMouseDown()
    {
        if (!IsClickable)
            return;

        OnClick();
    }

    public void OnClick()
    {
        clicks++;

        if (clicks < ClicksToPop)
        {
            AudioManager.Instance.PlayEffect(ClickClip, 1f, 0, 0.2f);
            return;
        }

        Pop();
    }

    void Pop()
    {
        TotalPops.Value++;

        AudioManager.Instance.PlayEffect(PopClip, 1f, 0, 0.2f);

        if (NextObject)
            SpawnNextObject();

        Destroy(gameObject);
    }

    void SpawnNextObject()
    {
        GameObject nextObject = Instantiate(NextObject, transform.position, Quaternion.identity, transform.parent);
        nextObject.name = nextObject.name.Replace("(Clone)", "");
    }
}
