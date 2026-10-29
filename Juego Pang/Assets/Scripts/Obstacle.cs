using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float fallspeed;
    public float minY = -15f;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GetComponent<GameManager>();
       Debug.Log("DIFICULTAD: " + gameManager.currentDifficulty);
    }

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector2.down * fallspeed * Time.deltaTime);
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }
}
