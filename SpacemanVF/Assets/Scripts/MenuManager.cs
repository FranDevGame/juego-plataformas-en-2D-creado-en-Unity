using UnityEngine;

public class MenuManager : MonoBehaviour
{
    /*Invocamos una clase pública Canvas para crear una variable menuCanvas 
     * dentro de nuestro Script MenuManager para que gestione 
     * todos los objetos del Canvas en Unity.*/

    //Creamos un Singleton que será el gobernador de este Script MenuManager
    public static MenuManager sharedInstance;
    public Canvas menuCanvas;
    public Canvas gameCanvas;
    public Canvas gameOverCanvas;
    

    private void Awake()
    {
        /*Declaramos el sharedInstance como en los demás Script, de manera 
         * que si llegamos aquí y no está asignado el sharedInstance, se asigna a este Script.*/

        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    public void ShowMainMenu() //Método para mostrar el menú en el juego
    {
        menuCanvas.enabled = true;
    }

    public void HideMainMenu()//Método para ocultar el menú en el juego.
    {
        menuCanvas.enabled = false;
    }

    public void ShowGameMenu()
    {
        gameCanvas.enabled = true;
    }

    public void HideGameMenu()
    {
        gameCanvas.enabled = false;
    }

    public void ShowGameOverMenu()
    {
        gameOverCanvas.enabled = true;
        
    }

    public void HideGameOverMenu()
    {
        gameOverCanvas.enabled = false;
    }

    public void ExitGame()
    {
        /*Aquí hemos declarado unos "if" "regionales" por así decirlo, para forzar la salida en cualquier plataforma que se esté jugando el juego.
         * La sintaxis es la siguiente: Si estamos en el EDITOR DE UNITY, para forzar la salida del juego (darle al botón STOP del emulador de Unity)
         * el reproductor del editor estará en false, es decir, no correrá. Y si no, si nos encontramos en cualquier plataforma que no sea el editor de Unity,
         * utilizamos el método Aplication.Quit();. Y finalizamos la sentencia "if" con un #endif.*/

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//Este código está más intenso que Aplication.quit porque es el que se va a ejecutar en el editor de Unity.
#else
Aplication.Quit(); //Si compilasemos para playstation, PC, o moviles el código que resaltaría sería este porque es el que se ejecutaría.
#endif

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
}
