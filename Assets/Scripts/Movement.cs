using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip engineThrusters;
    [SerializeField] AudioClip sideThrusters;
    [SerializeField] ParticleSystem engineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    Rigidbody playerRB;
    AudioSource audioSourceA;
    //AudioSource audioSourceB; //this didn't work

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        audioSourceA = GetComponent<AudioSource>();
        //audioSourceB = GetComponent<AudioSource>(); //this didn't work
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

        void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        playerRB.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!engineParticles.isPlaying)
        {
            engineParticles.Play();
        }
        if (!audioSourceA.isPlaying)
        {
            audioSourceA.PlayOneShot(engineThrusters);
        }
    }

    private void StopThrusting()
    {
        engineParticles.Stop();
        audioSourceA.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
        //if (!audioSourceB.isPlaying)  //this didn't work
        //{
        //    audioSourceB.PlayOneShot(sideThrusters);
        //}
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    private void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
        //audioSourceB.Stop();          //this didn't work
    }

    void ApplyRotation(float rotationThisFrame)
    {
        playerRB.freezeRotation = true; //Freezing Rotation so manual rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        playerRB.freezeRotation = false; //Unfreezing rotation for physics
    }
}
