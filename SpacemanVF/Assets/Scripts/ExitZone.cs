using UnityEngine;

public class ExitZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //No lo vamos a necesitar.
    }

    // Update is called once per frame
    void Update()
    {
        //No lo vamos a necesitar.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*si el personaje entra dentro de la zona de colisión,
         * La instancia compartida(Singleton) de la clase LevelManager,
         * eliminará un bloque (RemoveLevelBlock) y añadirá un bloque nuevo (AddLevelBlock)
         * de modo que irá reciclando, con uno eliminado, añadirá uno nuevo en pantalla.*/

        if (collision.tag == "Player")
        {
            LevelManager.SharedInstance.AddLevelBlock();
            LevelManager.SharedInstance.RemoveLevelBlock();
        }
        
    }
}
