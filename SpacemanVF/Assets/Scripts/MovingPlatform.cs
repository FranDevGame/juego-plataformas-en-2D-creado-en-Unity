using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //Método onTriggerEnter2D para que cuando se pose el personaje en la plataforma, ésta automaticamente arranque a moverse.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Animator animator = GetComponent<Animator>(); /*Invocamos la clase Animator de Unity aquí en VS con el nombre animator para que VS la reconozca como una variable, 
                                                       * y a su vez nos traemos la propia clase Animator de Unity con el método GetComponente para tenerla aquí en VS.*/
        animator.enabled = true; //el animator estará activo por defecto.
    }
}
