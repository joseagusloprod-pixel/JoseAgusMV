using UnityEngine;
using UnityEngine.InputSystem;

public class ControlJugador : MonoBehaviour
{
    public int vidas;
    public float velocidad;
    private float minX = -7.5f, maxX = 7.5f;


    void Start()
    {
        
    }

    
    void Update()
    {
        // Movimiento horizontal del jugador imput manager antiguo
        //float move = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;
        //transform.Translate(move, 0f, 0f);

        //Input manager nuevo. Movimiento horizontal del jugador
        float direccion = 0f;
        Keyboard teclado = Keyboard.current;
        if (teclado != null)
        {
            if (teclado.leftArrowKey.isPressed||teclado.aKey.isPressed)
            {
                direccion -= 1f;
            }
            else if (teclado.rightArrowKey.isPressed||teclado.dKey.isPressed)
            {
                direccion += 1f;
            }
        }
        
        //soporte opcional para mando.
        Gamepad mando = Gamepad.current;
        if (mando != null && mando.leftStick.x.ReadDefaultValue() != 0.1f)
        {
            direccion = mando.leftStick.x.ReadDefaultValue(); 
        }
        transform.Translate(direccion * velocidad * Time.deltaTime, 0f, 0f);


        // Limitar la posición del jugador dentro de los límites de la pantalla
        Vector2 position = transform.position;

        position.x = Mathf.Clamp(position.x, minX, maxX);
        transform.position = position;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                Destroy(other.gameObject);
                //vidas -= obstacle.damage;

                //Logica de Game Over
               // if()
            }
        }
    }

}
