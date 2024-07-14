using System.Collections;
using UnityEngine;
public class GrassCollectible : MonoBehaviour
{
    public GrassSpawner spawner;
    public float shrinkSpeed = 1f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ally"))
        {
            StartCoroutine(ShrinkChunk());
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ally"))
        {
            StartCoroutine(ShrinkChunk());
            // spawner.StartRegrowthTimer();
        }
    }
    
    IEnumerator ShrinkChunk()
    {
        while (transform.localScale.x > 0)
        {
            transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, shrinkSpeed) * Time.deltaTime;
            yield return null;
        }

        Player.instance.carbon += 1f;
        Player.instance.fuel += 1f;
        Debug.Log("Carbon: " +  + Player.instance.carbon);
        Debug.Log("Fuel: " +  + Player.instance.fuel);
        // Ball.instance.overBoost += 2f;
        // Destroy(gameObject);
        Destroy(transform.parent.gameObject);
    }
}
