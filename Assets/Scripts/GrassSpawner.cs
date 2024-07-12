using UnityEngine;
using System.Collections;

public class GrassSpawner : MonoBehaviour
{
    public GameObject grassPrefab;
    public float regrowthTime = 10f;
    public GameObject currentGrass;
    public bool growing = false;

    void Start()
    {
        SpawnGrass();
    }
    
    void Update()
    {
        if (currentGrass == null && growing == false)
        {
            growing = true;
            StartRegrowthTimer();
        }
    }

    void SpawnGrass()
    {
        currentGrass = Instantiate(grassPrefab, transform.position, transform.rotation);
        // currentGrass.GetComponent<GrassCollectible>().spawner = this;
    }

    public void StartRegrowthTimer()
    {
        StartCoroutine(RegrowGrass());
    }

    private IEnumerator RegrowGrass()
    {
        yield return new WaitForSeconds(regrowthTime);
        SpawnGrass();
        growing = false;
    }
}