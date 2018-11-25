using System.Collections;
using Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    private int[] _playerScore;
    public Canvas roundCanvas;
    public RoundsController roundsController;
    public int scoreLimit = 5;
    
    private void Start() {
        if(roundCanvas == null) {Debug.Log("Please put a canvas in roundcanvas");}
        roundCanvas.enabled = false;
        _playerScore = new[] {0, 0, 0, 0};
        roundsController.endRound.AddListener(onEndRound);
    }


    public void onEndRound([CanBeNull] Entities.Player.PlayerController winner) {

        TextWin textCanvas = roundCanvas.transform.Find("Image").Find("t").GetComponent<TextWin>();

        
        if ( winner ) {
            _playerScore[winner.player]++;
            roundCanvas.transform.Find("Image").Find("t").GetComponent<TextWin>().setMenu(winner.player);
        } else {
            //draw
        }
        
        textCanvas.bearWins  = _playerScore[0];
        textCanvas.cowWins   = _playerScore[1];
        textCanvas.sharkWins = _playerScore[2];
        textCanvas.lionWins  = _playerScore[3];
        roundCanvas.enabled = true;
        bool isGameFinish = false;
            
        foreach ( int score in _playerScore ) {
            if ( score == scoreLimit ) {
                isGameFinish = true;
            }
        }

        if ( isGameFinish ) {
            //Game Finish 
        } else {
            StartCoroutine(startNewRound());
        }
    }

    private void resetScore() { _playerScore = new[] {0, 0, 0, 0}; }


    IEnumerator startNewRound() {
        yield return new WaitForSeconds(2f);
        roundCanvas.enabled = false;
        roundsController.StartRound();
    }
    

}
