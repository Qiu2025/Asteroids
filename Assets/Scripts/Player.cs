using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float thrustForce = 100f;
    public float rotationSpeed = 150f;
    public static float SCORE = 0;
    public static float xBorderLimit = 9;
    public static float yBorderLimit = 6;
    private bool isPaused = false;

    public GameObject gun, bulletPrefab;
    public ParticleSystem ps;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public BulletPool bulletPool;

    private AudioSource sound;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float thrust = Input.GetAxis("Thrust") * Time.deltaTime;
        Vector3 thrustDirection = transform.right;
        rb.AddForce(thrustDirection * thrust * thrustForce);

        float rotation = Input.GetAxis("Rotation") * Time.deltaTime;
        rotation *= (-1);
        transform.Rotate(new Vector3(0, 0, rotation * rotationSpeed));
    }

    void Update()
    {
        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit;
        else if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit;
        else if (newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit;
        else if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit;

        transform.position = newPos;

        if (Input.GetKeyDown(KeyCode.Space) && !isPaused)
        {
            GameObject bullet = bulletPool.GetBullet();
            Bullet scriptBullet = bullet.GetComponent<Bullet>();
            bullet.transform.position = gun.transform.position;
            scriptBullet.direction = transform.right;

            sound.Play();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ps.Play();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("MiniAsteroid"))
        {
            SCORE = 0;
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
            BulletPool.pool.Clear();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }
    }

    public void OnButton()
    {
        TogglePause();
    }

    public void OnBack()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRetry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Juego");
    }
}
