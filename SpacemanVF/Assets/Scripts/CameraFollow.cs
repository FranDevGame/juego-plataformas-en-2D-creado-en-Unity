using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target; //variable para declarar al objetivo (target) que seguirá la cámara.

    public Vector3 offset = new Vector3(0.2f, 0f, -10f); /*Variable para declarar a la distancia 
                                                          * que la cámara debe seguir al personaje.*/

    public float dampingTime = 0.3f; /*Variable para amortiguar el avance de la cámara, 
                                      * para que sea relajado y no tan brusco a la vista.*/

    public Vector3 velocity = Vector3.zero;

    public void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera(true); //Aquí el método MoveCamera con parámetro booleano será true ya que queremos suavidad de la cámara durante la partida.
    }

    public void ResetCameraPosition() /*Método para resetear la cámara, para cuando el personaje muera,
                                       * no lo siga hasta los confines del mundo. Y vuelva a la LevelStartPosition*/
    {
        MoveCamera(false); // En el reseteo de la cámara el MoveCamera estará en false para que sea instantáneo.
    }

    void MoveCamera(bool smooth)/*Con el método MoveCamera añadimos una funcionalidad al DampingTime,
                                 * y es que cuando se vaya moviendo la cámara estará en "true"
                                 * y cuando el personaje muera, estará en false. De manera que, al morir
                                 * no hará un barrido hacia atrás, si no que será instantáneo. 
                                 * en un frame, estará en la pantalla de muerte, y en el siguiente,
                                 * estará en la posición inicial para comenzar de nuevo la partida.(false)*/

    {
        Vector3 destination = new Vector3 /*Declaramos una variable Vector3 con el nombre destination
                                           * que será el destino de la cámara, que en este caso será
                                           * la posición del objetivo (target.position.x) 
                                           * menos la variable de la distancia(offset.x)
                                           * Y luego declarar las distancias de los ejes Y y Z.*/
            (target.position.x - offset.x,
            offset.y,
            offset.z);

        /*No se puede asginar directamente "this.transform.position" a "MoveCamera"
        * es decir, "esta configuración de transform con esta posición" al método
        * MoveCamera; ya que tenemos un método con parámetros booleanos. 
        * Así que declaramos un If donde sentenciamos distinos parámetros para 
        * que se cumplan en condición "true".*/

        if (smooth)
        {
            this.transform.position = Vector3.SmoothDamp(
                this.transform.position,
                destination,
                ref velocity, /*La variable velocity la vamos a pasar por referencia (ref), 
                               * de modo que va desde nuestro Script en VS,
                               * al Script de Unity para que éste haga sus cálculos y
                               * nos la devuelva al Script de VS para ver la velocidad de la
                               * cámara sin tener que calcularlo yo. Lo hará en método SmoothDamp.*/
                dampingTime
                );
        }
        else
        {
            this.transform.position = destination; /*Aquí asignamos directamente this.transform.position
                                                    *a destination de la cámara ya que estará en false,
                                                    *para cuando muera el personaje y no necesitamos 
                                                    *el deslizar suave de la cámara.*/

        }



    }
}
