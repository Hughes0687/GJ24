using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public float power = 0.5f;
    public float mass = 1f;
    public float maxAngularVelocity = 5f;
    public float angularDrag = 0.00f;
    public float drag = 0.05f;
    public float maxBoostValue = 5.0f;
    public bool grounded = false;
    public float boost = 0f;
    public float carbon;
    public float fuel = 0;
    public float scale = 1f;
    public Rigidbody _rigidbody;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {

        Vector3 newScale = new Vector3(scale, scale, scale);
        _rigidbody.transform.localScale = newScale;
        fuel += Time.deltaTime * 0.1f;
    }
}