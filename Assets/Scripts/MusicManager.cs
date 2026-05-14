using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Muzica de fundal")]
    public AudioClip melodieFundal;

    [Header("Setari volum")]
    [Range(0f, 1f)]
    public float volum = 1f;

    private AudioSource audioSource;

    void Awake()
    {
        // Daca exista deja un MusicManager din alta scena, il pastram pe ala
        if (Instance != null && Instance != this)
        {
            // Daca managerul vechi nu are melodie, ii dam melodia de pe managerul nou
            if (Instance.melodieFundal == null && melodieFundal != null)
            {
                Instance.SeteazaMelodie(melodieFundal);
            }

            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        // Daca volumul salvat era 0 si nu iti dadeai seama, poti sterge PlayerPrefs sau il pui aici default 1
        volum = PlayerPrefs.GetFloat("VolumMuzica", 1f);

        PregatesteAudioSource();
        PornesteMuzica();
    }

    void PregatesteAudioSource()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        // Muzica de fundal trebuie sa fie 2D, nu dependenta de pozitia camerei
        audioSource.spatialBlend = 0f;

        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = volum;

        if (melodieFundal != null)
            audioSource.clip = melodieFundal;
    }

    public void SeteazaMelodie(AudioClip clipNou)
    {
        melodieFundal = clipNou;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        audioSource.clip = melodieFundal;
        PornesteMuzica();
    }

    public void PornesteMuzica()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (melodieFundal == null)
        {
            Debug.LogWarning("MusicManager: Nu ai pus nicio melodie in campul Melodie Fundal.");
            return;
        }

        audioSource.clip = melodieFundal;
        audioSource.volume = volum;
        audioSource.loop = true;
        audioSource.spatialBlend = 0f;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            Debug.Log("MusicManager: Muzica a pornit.");
        }
    }

    public void SeteazaVolum(float valoare)
    {
        volum = Mathf.Clamp01(valoare);

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        audioSource.volume = volum;

        PlayerPrefs.SetFloat("VolumMuzica", volum);
        PlayerPrefs.Save();
    }

    public float GetVolum()
    {
        return volum;
    }

    public void OpresteMuzica()
    {
        if (audioSource != null)
            audioSource.Stop();
    }
}