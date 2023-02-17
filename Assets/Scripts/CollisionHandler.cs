using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 2f;
    [SerializeField] AudioClip explosionClip;
    [SerializeField] AudioClip successClip;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem successParticle;
    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable; //toggles collision
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisable){return;}
        switch (other.gameObject.tag)
        {
            case "Friendly":
                //Debug.Log("Frienldy Collision");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    //Todo: Add Particle effect
    void StartSuccessSequence()
    {
        //if (isTransitioning){return;}
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successClip);
        successParticle.Play();
        isTransitioning = true;
        Invoke("NextLevel", delayTime);
    }

    void StartCrashSequence()
    {
        //if (isTransitioning){return;}
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(explosionClip);
        explosionParticle.Play();
        isTransitioning = true;
        Invoke("ReloadLevel", delayTime);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
