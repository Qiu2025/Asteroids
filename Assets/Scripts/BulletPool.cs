using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject prefab;
    public static Queue<GameObject> pool = new Queue<GameObject>();

    // Para obtener una instancia del pool
    public GameObject GetBullet()
    {
        // Si el pool tiene elementos, devolverlo
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        // Si el pool esta vacio, instanciar nuevos
        return Instantiate(prefab);
    }

    // Para devolver instancias al pool
    public static void ReturnBullet(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
