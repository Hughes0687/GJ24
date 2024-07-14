using UnityEngine;

public class CarbonSpawner : MonoBehaviour
{
    public GameObject carbonAllyPrefab;
    public GameObject player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (player != null && Player.instance.carbon > 5)
            {
                Debug.Log(player.transform.position);
                SpawnCarbonAlly(player);
            }
        }
    }

    void SpawnCarbonAlly(GameObject player)
    {

        if (Player.instance.fuel > 15)
        {
            Vector3 spawnPosition = player.transform.position + new Vector3(0, 0, -1);
            Instantiate(carbonAllyPrefab, spawnPosition, Quaternion.identity);
            Player.instance.fuel -= 15;
            Debug.Log("Carbon Ally spawned. Fuel remaining: " + Player.instance.fuel);
        }

        Debug.Log("Fyue Ally not spawned. Fuel remaining: " + Player.instance.fuel);
    }
}