using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    public Vector3 direction;
    public GameObject miniAsteroidPrefab;

    void Update()
    {
        var pos = transform.position;
        if (pos.x > Player.xBorderLimit || pos.x < -Player.xBorderLimit ||
            pos.y > Player.yBorderLimit || pos.y < -Player.yBorderLimit)
        {
            BulletPool.ReturnBullet(gameObject);                
        }
            
        transform.Translate(speed * Time.deltaTime * direction);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            IncreaseScore();
            Destroy(collision.gameObject);
            BulletPool.ReturnBullet(gameObject);

            Vector3 baseDir = direction.normalized;
            Vector3 leftDir = new Vector3(-baseDir.y, baseDir.x, 0);
            Vector3 rightDir = new Vector3(baseDir.y, -baseDir.x, 0);

            GameObject miniAsteroid1 = Instantiate(miniAsteroidPrefab, collision.transform.position, Quaternion.identity);
            miniAsteroid1.GetComponent<MiniAsteroid>().direction = leftDir;

            GameObject miniAsteroid2 = Instantiate(miniAsteroidPrefab, collision.transform.position, Quaternion.identity);
            miniAsteroid2.GetComponent<MiniAsteroid>().direction = rightDir;
        }
        else if (collision.gameObject.CompareTag("MiniAsteroid"))
        {
            IncreaseScore();
            Destroy(collision.gameObject);
            BulletPool.ReturnBullet(gameObject);
        }
    }

    void IncreaseScore()
    {
        Player.SCORE++;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("ScoreText");
        go.GetComponent<TextMeshProUGUI>().text = "Puntuacion: " + Player.SCORE;
    }
}
