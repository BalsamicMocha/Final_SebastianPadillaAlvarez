using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessManager : MonoBehaviour
{
    public static PostProcessManager Instance;

    private PostProcessVolume volume;
    private Vignette vignette;
    private ChromaticAberration chroma;

    private void Awake()
    {
        // Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Buscar el PostProcessVolume en la escena
        volume = FindObjectOfType<PostProcessVolume>();

        if (volume != null && volume.profile != null)
        {
            // Intentar obtener el Vignette del perfil
            volume.profile.TryGetSettings(out vignette);
            volume.profile.TryGetSettings(out chroma);
        }
    }
    public void TriggerChromaticAberration()
    {
        if (chroma != null)
        {
            chroma.intensity.value = 200; // Ej: 1 = máximo
        }
    }
    public void ResetChromaticAberration()
    {
        if (chroma != null)
        {
            chroma.intensity.value = 0f;
        }
    }

    public void TriggerDeathVignette()
    {
        if (vignette != null)
        {
            // Aumentar intensidad del vignette (0 = nada, 1 = pantalla casi negra)
            vignette.intensity.value = 0.8f;
        }
    }

    public void ResetVignette()
    {
        if (vignette != null)
        {
            // Regresar intensidad a algo más normal
            vignette.intensity.value = 0.2f;
        }
    }
}
