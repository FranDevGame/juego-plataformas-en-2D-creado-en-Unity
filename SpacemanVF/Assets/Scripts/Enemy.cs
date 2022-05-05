using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float runningSpeed = 1.5f; //Velocidad a la que irá el enemigo

    private Rigidbody2D rigidBodyEnemy; //Declarar la variable RigidBody para configurarla luego en el Awake

    private int enemyDamage = 50;

    public bool facingRight = false; /* Variable Booleana para saber a donde está el enemigo, si a la izquierda o a la derecha. 
                                      * Será true si mira a la derecha, y false si mira a la izquierda. Colocamos por defecto en false, para que se encuentre de cara con el personaje que está mirando a la derecha.*/
    private Vector3 startPosition; // Variable privada de 3 vectores donde vamos a indicar la posición inicial del enemigo una vez pasemos de un escenario a otro. 

    private void Awake()
    {
        rigidBodyEnemy = GetComponent<Rigidbody2D>(); //Invocar al RigidBody2D de nuestro enemigo
        startPosition = this.transform.position; //Aquí indicamos donde se posicionará una vez despierte el juego por primera vez.
    }


    // Start is called before the first frame update
    void Start()
    {
        //this.transform.position = startPosition; 
    }

    private void FixedUpdate() //Hemos cambiado el Update por el FixedUpdate porque vamos a tener que hacer cambios a las físicas y evitaremos el lagueo o bajada de frames.
    {
        float currentRunningSpeed = runningSpeed; //Variable de la velocidad actual del enemigo, para utilizarla ahora en una función para girar al enemigo en dicho punto de velocidad.

        if (facingRight)
        {
            //Mirando a la derecha
            currentRunningSpeed = runningSpeed; //runningSpeed con el signo positivo es ir hacia la derecha
            this.transform.eulerAngles = new Vector3(0, 180, 0); /*Con transform.eulerAngles, rotamos al enemigo 0 grados en el eje X, 
                                                                  * 180 en el eje Y ya que por defecto estará mirando a la izquierda y queremos que mire a la derecha, y 0 en el eje Z.*/
        }
        else
        {
            //Mirando a la izquierda
            currentRunningSpeed = -runningSpeed; //runningSpeed con el signo menos, signo negativo, es ir hacia la izquierda
            this.transform.eulerAngles = Vector3.zero; /*Aquí utilizamos el mismo código, excepto que acortamos (new vector 3 (0,0,0)) por vector3.zero, 
                                                        * ya que por defecto mira a la izquierda y no necesitamos que gire a la derecha.*/
        }

        if (GameManager.sharedInstance.currentGameState == GameState.inGame) /* Si el estado del juego actual, en la instancia compartida del GameManager está en inGame (dentro de la partida),
                                                                             * la velocidad del rigidBody del enemigo será igual a un vector de 2 ejes (X e Y), donde en X será
                                                                             * la velocidad de carrera actual (currentrunningSpeed) y en el eje Y la velocidad propia del rigidBody en el eje Y.*/
        {
            rigidBodyEnemy.velocity = new Vector2(currentRunningSpeed,
                rigidBodyEnemy.velocity.y);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision) //Será llamado cuando el enemigo entre en colisión con otro collider.
    {
        if (collision.tag == "Coin") //Si colisiona con la moneda, devuelve el mismo valor; es decir, no ocurre nada.
        {
            return;
        }

        if (collision.tag == "Player") //Si colisiona con el personaje
        {
            
            collision.gameObject.GetComponent<PlayerControllerVF>().CollectHealth(-enemyDamage); /*Al colisionar con el gameObject cuyo componente es el PlayerControllerVF, esa colisión le va a hacer DAÑO.
                                                                                                       * por tanto el método CollectHealth sumará puntos (NEGATIVOS) con la variable float enemyDamage.
                                                                                                       * Podemos dejarla con valor float, de manera que arriba donde declaramos enemyDamage, en vez de float,
                                                                                                       * declaremos la variable en int y sea un nº entero (10).
                                                                                                       * o hacer un casting explícito para que los puntos de vida de CollectHealth,
                                                                                                       * que son de tipo de datos int, los convirtamos en float y así tenemos nºs decimales. (10.0f)*/
            return;
        }
        if(collision.tag == "Exit Zone") /*Hemos creado este If para que al colisionar nuestro enemigo con una ExitZone,
                                          * no se rompa el efecto de su trayectoria, ignore el collider de la ExitZone, 
                                          * y siga su recorrido entre los márgenes de las rocas situadas en el escenario.*/
        {
            return;
        }
        
        // Si llegamos aquí, no hemos chocado ni con monedas ni, ni con players, 
        //Lo más normal es que aquí haya otro enemigo o bien escenario
        //Vamos a hacer que el enemigo rote
        facingRight = !facingRight;
    }
}
