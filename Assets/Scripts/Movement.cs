using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 1000.0f;
    [SerializeField] float rotationStrength = 125.0f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineSFX);
            }
            if (!mainBoosterParticles.isPlaying)
            {
                mainBoosterParticles.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainBoosterParticles.Stop();
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
            if (!rightBoosterParticles.isPlaying)
            {
                leftBoosterParticles.Stop();
                rightBoosterParticles.Play();
            }
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
            if (!leftBoosterParticles.isPlaying)
            {
                rightBoosterParticles.Stop();
                leftBoosterParticles.Play();
            }
        }
        else
        {
            rightBoosterParticles.Stop();
            leftBoosterParticles.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
    }
}
