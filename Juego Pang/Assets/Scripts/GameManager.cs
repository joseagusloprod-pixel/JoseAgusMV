using UnityEngine;



 public enum difficulty
{
    Easy,
    Medium,
    Hard
}
public class GameManager : MonoBehaviour
{

    // VARIABLES DE PREFABS
    public GameObject[] obstaculoPrefabs;
    public float minIntervaloPrefabs = 0.5f;
    public float maxIntervaloPrefabs = 2f;
    private float timer = 0f;
    private float nextIntervaloPrefabs;

    // VARIABLES DE POSICION DE GENERACION DE PREFABS
    public float minX = -7.5f;
    public float maxX = 7.5f;
    public float spawnY = 10f;

    public difficulty currentDifficulty = difficulty.Easy;

    void Start()
    {
       Debug.Log("DIFICULTAD: " + currentDifficulty);
        //Cambio de velocidad de generacion de prefabs segun la dificultad seleccionada
        switch (currentDifficulty) 
        {
            case difficulty.Easy:
                minIntervaloPrefabs = 1.5f;
                maxIntervaloPrefabs = 3f;
                break;
            case difficulty.Medium:
                minIntervaloPrefabs = 1f;
                maxIntervaloPrefabs = 2f;
                break;
            case difficulty.Hard:
                minIntervaloPrefabs = 0.2f;
                maxIntervaloPrefabs = 0.5f;
                break;
        }
        //Inicializar el primer intervalo de generación
        nextIntervaloPrefabs = Random.Range(minIntervaloPrefabs, maxIntervaloPrefabs);
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= nextIntervaloPrefabs)
        {
            // Generar un nuevo prefab en una posición aleatoria
            float randomX = Random.Range(minX, maxX);
            Vector2 spawnPosition = new Vector2(randomX, spawnY);
            int randomIndex = Random.Range(0, obstaculoPrefabs.Length);
            Instantiate(obstaculoPrefabs[randomIndex], spawnPosition, Quaternion.identity);

            //RESETAR EL TIMER Y CALCULAR EL SIGUIENTE INTERVALO DE GENERACION
            timer = 0f;
            nextIntervaloPrefabs = Random.Range(minIntervaloPrefabs, maxIntervaloPrefabs);
        }
    }
}
