using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Accel : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] ParticleSystem Finishline_effect;
    [SerializeField] ParticleSystem Explosion_effect;
    [SerializeField] ParticleSystem Thrusting_effect;
    [SerializeField] AudioClip Finish_line_audio;
    [SerializeField] AudioClip collision_audio;

    AudioSource audiosource;
    bool is_alive = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (transform.position.y < 2f) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (!is_alive) return;
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Thrusting_effect.Play();
            rigidbody.AddRelativeForce(Vector3.up * Time.deltaTime * 1000f);
            if (!audiosource.isPlaying)
            {
                audiosource.Play();
            }
        }
        else audiosource.Stop();
        if (Input.GetKey(KeyCode.A)){
            rigidbody.freezeRotation = true;
            transform.Rotate(-1 * Time.deltaTime * 100f, 0,0);
            rigidbody.freezeRotation = false;
        }
        if (Input.GetKey(KeyCode.D)){
            rigidbody.freezeRotation = true;
            transform.Rotate(1 * Time.deltaTime * 100f, 0,0);
            rigidbody.freezeRotation = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!is_alive) return;
        switch (collision.gameObject.tag)
        {
            case "finish_line":
                audiosource.PlayOneShot(Finish_line_audio);
                Finishline_effect.Play();
                Startsuccesssequence();
                break;
            case "obstacle":
                audiosource.PlayOneShot(collision_audio);
                is_alive = false;
                Explosion_effect.Play();
                StartDestructionSequence();
                break;
            case "starting_point":
                break;
            default:
                Debug.Log("oops, you got hit");
                break;
        }
    }


    private void StartDestructionSequence()
    {
        GetComponent<Accel>().enabled = false;
        Invoke("Reloadlevel", 1f);
    }

    private void Startsuccesssequence()
    {
        GetComponent<Accel>().enabled = false;
        Invoke("LoadNextLevel", 1f);
    }

    private void Reloadlevel()
    {
       
        int currentsceneno = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentsceneno);
    }

    private void LoadNextLevel()
    {
        int currentsceneno = SceneManager.GetActiveScene().buildIndex;
        if (currentsceneno + 1 == SceneManager.sceneCountInBuildSettings) {currentsceneno = -1;}
        SceneManager.LoadScene(currentsceneno+1);
    }
}
