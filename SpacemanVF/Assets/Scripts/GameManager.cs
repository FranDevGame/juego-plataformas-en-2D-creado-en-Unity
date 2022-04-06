using UnityEngine;

/*Creamos un Enumerado (Enum), y ésto es un tipo de variable que 
 * contiene varios estados. P.Ej; en una brújula, la estrella o punto cardinal
 * sería el Enumerado, y Norte, Sur, Este u Oeste serían los posibles estados del Enumerado.
 * Vamos a declarar un Enum con los 3 posibles estados del videojuego, y además
 * declararemos una variable para saber en cual de los 3 estados se encuentra el videojuego.*/
public enum GameState
{
    menu,
    inGame,
    gameOver
}
public class GameManager : MonoBehaviour
{

    /*Declaramos una variable publica de tipo GameState a la que llamaremos currentGameState
 * (Estado actual del juego) el cual iniciaremos con GameState.menu, ya que
 * el jugador al comenzar el juego tendrá el menú para comenzar una partida nueva.*/

    public GameState currentGameState = GameState.menu;

    /*Creamos una variable static de la clase GameManager, cuyo nombre es sharedInstance (Instancia compartida).
     * Este será el nombre del singleton y será el unico
     *que se instancie  como GameManager propiamente dicho,
     * para que no haya otros GameManager 
     * que entren en conflicto.*/

    public static GameManager sharedInstance;

    /*Declaramos la variable "PlayerController" en esta actual clase "GameManager", 
   * y la declaramos de manera privada y con el nombre "controller", de tal modo 
   * que sólo se puede modificar por código aquí en Visual Studio.*/
    private PlayerControllerVF controller;

    public int collectedObject = 0; //Contador de cuantos objetos llevamos recolectados.

    //Declaramos el método Awake, y dentro de él vamos a instanciar el Singleton llamado "SharedInstance"
    /*Si (if) el sharedInstance es igual, igual a null (si la instancia compartida no ha sido asignada previamente,
     * la asginas al script actual (GameManager). De modo que la primera vez que se llega al SharedInstance,
     * como no ha sido inicializado, es nulo, por lo tanto esa variable ejecuta que se le asgine el SharedInstance
     * al GameManager. El 2º que llegue al SharedInstance, ya no obtendrá el nivel nulo, porque ya está asignada.*/
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        /*Controller es igual a que la clase GameObject localice (Find) un objeto.
        *Como no se puede convertir implicitamente la clase GameObject en "Player"
        *(el nombre de nuestro GameObject de personaje en Unity, definido en Hierarchy, panel Jerarquía)
        * tenemos que obtener el componente PlayerController del GameObject para que el casting sea posible.
        * De modo que invocamos al método "GetComponent" con el nombre de la clase que ya está declarada
        * arriba del Awake, "PlayerController", por lo que ahora sí es posible el casting implícito.
        * Además, de esta manera el Game Manager tiene localizado a nuestro PlayerController, enlazado con Unity*/


        controller = GameObject.Find("Player").
            GetComponent<PlayerControllerVF>();
        SetGameState(GameState.menu);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && currentGameState !=
             GameState.inGame)/*Con el && (Ampersand) solucionamos el BUG
                              *que iniciábamos la partida infinitamente después de morir
                              *Ahora sólo podremos iniciar la partida, si (if) pulsamos 
                              *"Submit" que está asociado al botón "Enter" y a la misma vez,
                              *si se cumple la segunda condición booleana que es:
                              *Que el GameManager tenga un estado(currentGameState)
                              *distinto(!=) al de "Partida"*/
        {
            StartGame();
        }
       

    }

    public void StartGame() //Método para iniciar el juego
    {
        SetGameState(GameState.inGame);
    }

    public void GameOver() //Método para finalizar una partida
    {
        SetGameState(GameState.gameOver);
        
    }

    public void BackToMenu() //Método para devolvernos al menú principal
    {
        SetGameState(GameState.menu);
    }

    /* Declaramos un nuevo método, con el parámetro GameState, el cual éste
  * identificará el estado actual del juego, y newGameState será 
  * el nuevo estado al que pasará el juego según pasen las cosas (menu, inGame, gameOver)*/
    private void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu) //Si el nuevo estado del juego es menu, colocar el menu
        {
            //TODO: colocar la lógica del menú
            
            MenuManager.sharedInstance.HideGameMenu();
            MenuManager.sharedInstance.HideGameOverMenu();
            MenuManager.sharedInstance.ShowMainMenu(); /*Invocamos al singleton del script MenuManager,
                                                       *para declarar aquí en el script GameManager,
                                                       *el método ShowMainmenu mientras el juego está
                                                       *en el estado menu.*/

        }
        else if (newGameState == GameState.inGame) // si el nuevo estado es inGame, colocar el escenario de juego
        {
            LevelManager.SharedInstance.RemoveAllLevelBlocks(); //Limpizamos la escena de bloques antiguos antes de crear los nuevos.
            LevelManager.SharedInstance.GenerateInitialBlocks(); //Genera los bloques iniciales.

            controller.StartGame(); /* El PlayerControllerVF(Player en Unity) comienza una partida.
                                     * También comienza una partida después de haber limpiado la escena de bloques antiguos, 
                                     * y haber generado los iniciales.*/

            
            MenuManager.sharedInstance.HideGameOverMenu();
            MenuManager.sharedInstance.HideMainMenu();/*Invocamos al singleton del script MenuManager,
                                                       * para declarar aquí en el script GameManager,
                                                       * el método HideMainmenu mientras el juego está
                                                       * en el estado inGame.*/
            MenuManager.sharedInstance.ShowGameMenu();


        }
        else if (newGameState == GameState.gameOver) // si el nuevo estado es gameOver, preparar el juego para gameOver
        {
            //TODO: preparar el juego para el Game Over
            MenuManager.sharedInstance.HideMainMenu();
            MenuManager.sharedInstance.HideGameMenu();
            MenuManager.sharedInstance.ShowGameOverMenu();
            

        }

        /* con esta linea de codigo, decimos lo siguiente:
         * la variable currentGameState, después de todos los cambios con los "If"
         * será actualizada por newGameState. De manera que currentGameState es la variable
         * del GameManager y que newGameState es el parámetro le pasamos a currentGameState y modifica el GameManager.*/

        this.currentGameState = newGameState;
    }

    public void CollectObject(Collectable collectable)/*Método CollectObject, que va a incrementar el contador 
                                                       * que hemos declarado arriba como "CollectedObject"*/
    {
        collectedObject += collectable.value; /* //Con esta función vamos a incrementar (+=) en el valor
                                               * que hayamos definido en la variable public int value. definida
                                               * en el script Collectable. (en este caso 1), el contador de objetos recolectados,
                                               * (collectedObject).*/
    }


}
