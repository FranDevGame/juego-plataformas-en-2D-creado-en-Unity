using UnityEngine;
using UnityEngine.UI; //Es la librería donde están los paquetes de assets para las UI (Interfaces de ususario)

public class GameView : MonoBehaviour
{
    public Text coinText, scoreText, maxScoreText, ScoreTotalText, CoinsTotalText;

    private PlayerControllerVF controller;

    // Use this for initialization
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerControllerVF>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame) /*Si el estado de juego del GameManager es ingame, 
         estos son los "playholders" a rellenar con sus respectivas cantidades numéricas.*/
        {
            int coins = GameManager.sharedInstance.collectedObject; /*las monedas son int porque serán siempre nºs enteros, y no habrá ocasión de que sean decimales.
                                                                     El nº de coins es igual al contador (collectedObject) que está en la instancia compartida del GameManager.*/


            float score = controller.GetTravelledDistance(); /*el score es valor float porque pueden ser nº enteros y decimales, ya que son metros recorridos durante la partida.
                                                               Ahora el Score es igual a la distancia que ha recorrido nuestro personaje. 
                                                              * (controller.GetTravelledDistance(); Lo ponemos en el Update para que vaya actualizándose
                                                              * constantemente. Para ahorrarnos el GetComponent, ya tenemos referenciado arriba al PlayerController, 
                                                              * Y así sólo tenemos que invocar a "Controller" en el update.*/


            float maxScore = PlayerPrefs.GetFloat("maxscore", 0); /*Nos traemos la clase Playerprefs con el método GetFloat, 
                                                                  * donde el valor de maxscore por defecto será 0 para la primera partida.*/


            coinText.text = coins.ToString(); /*Para que aparezca el nº de coins (valor nºmerico) en la variable coinText (valor string) de Unity en la pantalla de juego,
                                               * hay que convertir ese valor nºmerico a string con ToString.*/

            scoreText.text = "Score: " + score.ToString("f1"); /*Aquí el valor score(vanor nºmerico)
                                                              *lo convertimos a string también precediendo
                                                              * el texto Score que aparecerá en la pantalla de juego.
                                                              * Además dentro del método Tostring hay un parámetro que
                                                              *permite personalizar cuantos decimales quieres que aparezcan;
                                                              *en nuestro caso queremos que sea uno(f1), si fueran 2(f2, etx) */

            maxScoreText.text = "MaxScore: " + maxScore.ToString("f1"); // Igual que Score
        }
        if (GameManager.sharedInstance.currentGameState == GameState.gameOver) /*En este caso hemos declarado las variables int coins y float score iguales que en el inGame.
                                                                                * Pero con la diferencia de que hemos declarado dos variables nuevas para la pantalla de gameOver;
                                                                                * una variable ScoreTotalText que nos mostrará la puntuación total obtenida en la partida,
                                                                                * y una condición Coinstotaltext que nos mostrará el máximo de monedas obtenidas en la partida.
                                                                                * ATENCIÓN, para que Unity no nos de error de referencia de objetos, hay que asignar los objetos de 
                                                                                * la jerarquía de Unity, a las casillas de este mismo Script, aunque no se utilicen como por ejemplo,
                                                                                * aquí en el gameOver el maxScoreText , y al revés en el gameCanvas, con las dos variables declaradas aquí
                                                                                * en el gameOver.*/
        {
            int coins = GameManager.sharedInstance.collectedObject;
            float score = controller.GetTravelledDistance();
            
            ScoreTotalText.text = "Score " + score.ToString("f1");
            CoinsTotalText.text = coins.ToString();
        }
    }

}
