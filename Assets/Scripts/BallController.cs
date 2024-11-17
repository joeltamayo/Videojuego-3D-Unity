using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using UnityEngine.Audio;


public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float impulseForce = 3f;
    private bool ignoreNextcollision;

    private Vector3 startPosition;

    [HideInInspector]
    public int perfectPass;

    public float superSpeed = 8;

    private bool isSuperSpeedActive;

    public int perfectPassCount = 3;

    public GameObject splash;

    public AudioSource collisionAudio;
    public AudioSource loseAudio;
    public AudioSource breakAudio;
    public AudioSource fastAudio;



    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionAudio.Play();

        addSplash(collision);

        if (ignoreNextcollision)
        {
            return;
        }

        if (isSuperSpeedActive && !collision.transform.GetComponent<GoalController>())
        {
            breakAudio.Play();
            Destroy(collision.transform.parent.gameObject, 0.2f);
        }
        else
        {
            DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
            if (deathPart)
            {
                loseAudio.Play();
                GameManager.singleton.RestartLevel();
            }
        }

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

        ignoreNextcollision = true;

        Invoke("AllowNextCollision", 0.2F);

        perfectPass = 0;
        isSuperSpeedActive = false;
    }

    private void Update()
    {
        if (perfectPass >= perfectPassCount && !isSuperSpeedActive)
        {
            fastAudio.Play();
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * superSpeed, ForceMode.Impulse);
        }
        //Debug.Log("Perfec pass:" + perfectPass);
        //Debug.Log("Super sped active:" + isSuperSpeedActive);
        //Debug.Log("Super spass count:" + perfectPassCount);

    }

    private void AllowNextCollision()
    {
        ignoreNextcollision = false;
    }

    public void ResetBall()
    {
        transform.position = startPosition;
    }

    public void addSplash(Collision collision)
    {
        // Instancia el prefab del splash
        GameObject newSplash = Instantiate(splash);

        // Establece el splash como hijo de la plataforma donde ocurre la colisión
        newSplash.transform.SetParent(collision.transform);

        // Ajusta la posición del splash
        newSplash.transform.position = new Vector3(
            this.transform.position.x,
            this.transform.position.y - 0.11f,
            this.transform.position.z
        );

        // Cambia el color del splash para que coincida con el material de la bola
        Renderer ballRenderer = GetComponent<Renderer>(); // Obtiene el Renderer de la bola
        SpriteRenderer splashRenderer = newSplash.GetComponent<SpriteRenderer>(); // Obtiene el Sprite Renderer del splash

        if (ballRenderer != null && splashRenderer != null)
        {
            // Verifica si el material de la bola tiene una propiedad "_Color"
            if (ballRenderer.material.HasProperty("_Color"))
            {
                // Asigna el color del material de la bola al color del splash
                splashRenderer.color = ballRenderer.material.color;
            }
        }

        // Destruye el splash después de 3 segundos
        Destroy(newSplash, 3);
    }


}
