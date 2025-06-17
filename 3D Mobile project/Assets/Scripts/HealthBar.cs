using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Transform target;
    private Image fillImage;

    public float heightMultiplier = 1.6f; // Adjust this for fine-tuning

    public void Initialize(Transform followTarget)
    {
        target = followTarget;
        fillImage = GetComponentInChildren<Image>();
    }

    public void UpdateHealthBar(float percent)
    {
        if (fillImage != null)
            fillImage.fillAmount = percent;
    }

    private void LateUpdate()
    {
        if (target == null || Camera.main == null) return;

        // Use enemy scale to determine vertical offset
        float dynamicYOffset = target.localScale.y * heightMultiplier;
        Vector3 worldPos = target.position + new Vector3(0, dynamicYOffset, 0);

        // Position and rotate to face camera
        transform.position = worldPos;
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180f, 0); // Flip to face player
    }
}
