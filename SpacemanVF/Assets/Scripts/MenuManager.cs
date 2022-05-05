using UnityEngine;
using System.Collections;

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

    public void QuitGame() /*Método que hemos creado para que el juego salga al pulsar el botón Quit, sea la plataforma que sea en la que juguemos. 
                            * Hemos modificado el código ya que daba error al construir el ejecutable. Este fragmento de código sí funciona.*/
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }



}
