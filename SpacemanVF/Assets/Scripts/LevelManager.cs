using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager SharedInstance; //Declaramos un Singleton de la misma forma que en GameManager

    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>(); //Una lista de todos los bloques del nivel

    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>(); /* Esta lista es para visualizar el contenido
                                                                          * de la escena. Tener la visión de los bloques de nivel
                                                                          * actuales en la escena.*/
   

    public Transform levelStartPosition; //Variable donde se va a crear el primer bloque de nivel.


    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this; /*Cuando llegamos aquí, si el SharedInstance no ha sido previamente asignada,
                                    * se la asignas al Script actual (LevelManager). Recordamos que cada clase,
                                    * puede tener una SharedInstance.*/
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialBlocks();
    }


    public void AddLevelBlock()//Metodo para añadir bloque de nivel
    {
        int randomIdx = Random.Range(0, allTheLevelBlocks.Count); /*Hemos declarado una variable de tipo de dato int llamada "randomIdx" 
                                                                   * la cual utiliza la clase Random(aleatorio) con el método Range (Rango) 
                                                                   * de manera que se lee así: Instanciamos una variable int "randomIdx" con la clase Random y el método Range
                                                                   * donde añadiremos un bloque de nivel de manera aleatoria elegido entre el "0" y cualquiera de todos 
                                                                   * los bloques disponibles que tenemos (allTheLevelBlocks).*/
        LevelBlock block; //Variable del Bloque de nivel, al que le afectará el siguiente IF de abajo.


        Vector3 spawnPosition = Vector3.zero; /*Una variable Vector3 para indicar en qué posición queremos que se genere el bloque (spawnPosition).
                                               * De momento colocamos el spwanPosition en "zero".*/


        if (currentLevelBlocks.Count == 0) //Si en el nº de bloques actuales(CurrentLevelBlocs) hay 0, instancia el bloque

        {
            block = Instantiate(allTheLevelBlocks[0]);//que se encuentra en el array "AllTheLevelBlocks" en la posición 0.
            spawnPosition = levelStartPosition.position; /*La posición donde se generará el bloque de la posición 0 será 
                                                          * en la posición del LevelStartPosition de Unity*/
        }
        else
        {
            block = Instantiate(allTheLevelBlocks[randomIdx]);/* Y si no(else), instancia el bloque que se encuentra
                                                               * en la posición "randomIdx" que nos dará un bloque
                                                               * aleatorio dentro del array "AllLevelBlocks".*/
            spawnPosition = currentLevelBlocks
                [currentLevelBlocks.Count - 1].exitPoint.position; /*Y ese bloque "randomIdx" se generará en el
                                                                    * actual bloque de niveles (currentLevelBlocks)
                                                                    * en la posición "exitPoint" del bloque justamente 
                                                                    * anterior (-1). De manera que situándose en el final(exitPoint)
                                                                    * del bloque anterior (-1) hará que justo enlace el final
                                                                    * de ese bloque anterior con el comienzo del nuevo que se acaba
                                                                    * de generar*/
        }

        block.transform.SetParent(this.transform, false); /*El padre de todos los bloques (SetParent), será el LiveManager, 
                                                          * el script actual (this).
                                                          * El método Transform.SetParent trae un parámetro booleano por defecto
                                                          * que nosotros vamos a dejar en false, ya que si colocamos 
                                                          * "worldPositionStays", por defecto será "true" 
                                                          * y va a perjudicar a todos los bloques hijos del padre.*/
        Vector3 correction = new Vector3(
            spawnPosition.x - block.startPoint.position.x, /*Restamos al Block.Start.Point.position en el eje "X", 
                                                          * que es el bloque ACTUAL, la posición del bloque anterior en el eje "X".
                                                          * De manera que el vector lleva el final del bloque anterior, al comienzo del nuevo,
                                                          * para que se engarcen entre sí.*/
            spawnPosition.y - block.startPoint.position.y, /*Restamos al Block.Start.Point.position en el eje "Y", 
                                                          * que es el bloque ACTUAL, la posición del bloque anterior en el eje "Y".
                                                          * De manera que el vector lleva el final del bloque anterior, al comienzo del nuevo,
                                                          * para que se engarcen entre sí.*/
            0 //Y el eje "Z" será 0 porque al ser el eje de la profunidad, no tiene interferencias ni tiene protagonismo en nuestro juego 2D.
            );
        block.transform.position = correction; /*la posición del transform del Bloque de niveles es igual a "correction" 
                                                * que es la corrección del método Set.Parent*/
        currentLevelBlocks.Add(block); //Añadimos la configuración del bloque para incluirlo en el bloque actual de niveles de la escena del juego.

    }

        public void RemoveLevelBlock() //Metodo para eliminar bloque de nivel
    {
        /*Dentro del método RemoveBlock hemos instanciado la clase LevelBlock con la variable oldBlock
         * en el elemento 0 del array CurrentLevelBlocks.
         * Invocamos el método Remove para que elimine el OldBlock en la posición 0 del Array,
         * de manera que cuando se elimine el bloque 0, el bloque siguiente pasará a ser el elemento 0 
         * del Array (Lista de bloques en Unity) y así sucesivamente con todos los bloques. 
         * Por último hemos invocado el método Destroy para que destruya de la pantalla de juego
         * el GameObject instanciado y llamado OldBlock.*/

        LevelBlock oldBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldBlock);
        Destroy(oldBlock.gameObject);
    }

    public void RemoveAllLevelBlocks() //Metodo para eliminar todos los bloques de nivel cuando muera el personaje.
    {
        /*Mientras el Array CurrentLevelBlocks.Count sea positivo, mayor que 0; 
         * se ejecutará el método RemoveLevelBlock. De manera que siempre que haya un bloque
         * en pantalla, se irá eliminando, para que así se eliminen todos los bloques de una partida,
         * hasta que concluya la primera partida por ejemplo. 
         * Cuando no haya más bloques(<0, menor que 0), 
         * dejará de ejecutarse RemoveLevelBlock*/

        while (currentLevelBlocks.Count > 0)
        {
            RemoveLevelBlock();
        }
    }

    public void GenerateInitialBlocks()//Metodo para arrancar el videojuego con los bloques iniciales que saldrán en pantalla.
    {
        for (int i = 0; i < 7; i++)
        {
            AddLevelBlock();
        }

    }

}
