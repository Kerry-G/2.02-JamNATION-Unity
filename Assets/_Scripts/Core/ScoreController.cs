using Core;
using UnityEngine;

public class ScoreController : MonoBehaviour {

    private int[] _playerScore;
    public RoundsController roundsController;
    public int scoreLimit = 5;
    
    private void Start() {
        _playerScore = new[] {0, 0, 0, 0};
        roundsController.endRound.AddListener(onEndRound);
    }


    public void onEndRound(Entities.Player.PlayerController winner) {
        if ( winner ) {
            _playerScore[winner.player]++;
        } else {
            //draw
        }

        bool isGameFinish = false;
            
        foreach ( int score in _playerScore ) {
            if ( score == scoreLimit ) {
                isGameFinish = true;
            }
        }

        if ( isGameFinish ) {
            //Game Finish 
        } else {
            roundsController.StartRound();
        }
    }

    private void resetScore() { _playerScore = new[] {0, 0, 0, 0}; }
    
    
}
