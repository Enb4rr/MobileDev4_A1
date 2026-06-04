using UnityEngine;

public class ExitZone : MonoBehaviour
{
    private static readonly int EmissionIntensity = Shader.PropertyToID("_EmissionIntensity");
    
    public int ZoneIndex { get; set; }
    
    private Renderer materialRenderer;
    
    private void Awake()
    {
        materialRenderer = GetComponent<Renderer>();
        materialRenderer.material.SetFloat(EmissionIntensity, 0f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.OnBallEnteredZone(ZoneIndex);
    }
    
    public void Activate()
    {
        materialRenderer.material.SetFloat(EmissionIntensity, 10f);
    }

    public void Deactivate()
    {
        materialRenderer.material.SetFloat(EmissionIntensity, 0f);
    }
}
