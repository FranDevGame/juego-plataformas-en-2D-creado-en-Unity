using UnityEngine;

public class KillZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*OnTriggerEnter2D es un método al que se invoca,
     * cuando hay un collaider dentro de otro.
     * En este caso nuestro Astronauta es un collaider,
     * dentro del collaider que posee el GameObject "Kill Zone".*/
    /* Como el objeto "collision" no es de la clase "Player Controller",
     * vamos a obtener el componente de la clase Player Controller. De manera que:
     * PlayerController controller = colission.GetComponent<PlayerController>();
     * Así al recuperar el controlador "PlayerController" y tenerlo en esta 
     * clase actual "KillZone" podemos indicarle que DEBE MORIR.
     * Además, ahora en la "Kill zone" invoca al método Die(); del controlador 
     * "Player Controller"*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")//Si el objeto contra el que colisiono, es el personaje, morírá.
        {
            PlayerControllerVF controller =
                collision.GetComponent<PlayerControllerVF>();
            controller.Die();
            
        }
    }
}
