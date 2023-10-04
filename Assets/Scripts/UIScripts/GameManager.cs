using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Text player1ScoreText;
	public Text player2ScoreText;
    public GameObject catNipBall;
	private int player1Score;
    private int player2Score;
    public GameObject paddle1;
    public GameObject paddle2;
    public AbilitiesSpawner abilitySpawner;
    public static int abilityIndex;
    public static string playerWinner;
    public AudioSource[] miaoSound;
    public AudioSource backgroundSound;

    public void Player1Scores() {
        player1Score++;
        player1ScoreText.text = player1Score.ToString();
        ball.ResetPosition();

	}

	public void Player2Scores() {
        player2Score++;
		player2ScoreText.text = player2Score.ToString();
		ball.ResetPosition();

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) || (Input.GetKeyDown(KeyCode.RightShift)))
        {
            int miaoSoundIndex = UnityEngine.Random.Range(0, miaoSound.Length);
            miaoSound[miaoSoundIndex].Play();
        }
        EndScreenUi();
    }
    public void EndScreenUi()
    {
        if(player1Score == 3 )
        {
            playerWinner = "player 1 \nwins";
            SceneManager.LoadScene(2);
        }
        else if ( player2Score == 3)
        {
            playerWinner = "player 2 \nwins";
            SceneManager.LoadScene(2);
        }
       
    }
}
