using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayReload = 1.5f;
    [SerializeField] AudioClip successAudio;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    private AudioSource audioSource;
    
    bool isTransitioning = false;
    private void OnCollisionEnter(Collision other) {

        if (isTransitioning) return;

        switch (other.gameObject.tag)
        {
            case "Fuel":
                AddFuel();
                break;
            case "Friendly":
                // Do nothing for now;
                break;
            case "Finish":
                CompleteLevel();
                break;
            default:
                Die();
                break;
        }
    }
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    private void AddFuel() {
        // Add fuel to rocklet
        Debug.Log("Adding Fuel!!");
    }

    private void CompleteLevel() {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        successParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        Invoke("LoadNextLevel", delayReload);
    }

    private void Die() {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        Invoke("ReloadLevel", delayReload);
    }

    private void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    private void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentSceneIndex + 1 >= SceneManager.sceneCountInBuildSettings ? 0 : currentSceneIndex + 1;
        SceneManager.LoadScene(nextLevelIndex);
    }
}
