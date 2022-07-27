using UnityEngine;
using ScriptableObjectArchitecture;

public class Bubble : MonoBehaviour
{
    [Header("Bubble Settings")]
    [SerializeField] Sprite PoppedSprite;
    public bool IsPopped = false;

    [Header("Audio Settings")]
    [SerializeField] AudioClip PopClip;

    [Header("Variables")]
    [SerializeField] IntVariable TotalPops;

    [Header("Components")]
    [SerializeField]SpriteRenderer spriteRenderer;


    private void OnMouseDown()
    {
        if (IsPopped)
            return;

        Pop();
    }


    public void Pop()
    {
        TotalPops.Value++;
        AudioManager.Instance.PlayEffect(PopClip, 1f, 0, 0.2f);
        SetAsPopped();
    }

    public void SetAsPopped()
    {
        IsPopped = true;

        spriteRenderer.sprite = PoppedSprite;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
    }
}
