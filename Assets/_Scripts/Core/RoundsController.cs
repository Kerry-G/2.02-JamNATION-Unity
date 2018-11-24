using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;

namespace Core {
    public class RoundsController : MonoBehaviour {

        private PlayerController[] _playingPlayers = new PlayerController[4];
        private bool               roundRunning    = false;

        public GameObject[] players; // The 4 players prefabs for 4 players
        public GameObject[] spawningPositions;


        private void Start() {
            //
            StartRound();
        }


        private void Update() {
            //
            DetectEndRound();
        }


        private void DetectEndRound() {
            int              hasLivesPlayerCount = 0;
            PlayerController winningPlayer       = null;
            foreach ( PlayerController player in _playingPlayers ) {
                if ( player.HasLives ) {
                    hasLivesPlayerCount++;
                    winningPlayer = player;
                }
            }

            if ( hasLivesPlayerCount <= 1 ) EndRound(winningPlayer);
        }


        /// <summary>
        ///
        /// </summary>
        public void StartRound() {
            if ( spawningPositions.Length < 4 ) {
                Debug.LogWarning("Need at least 4 spawning positions !!");
                return;
            }

            // Spawn players to starting positions
            List<int> usedSpawn = new List<int>();
            foreach ( GameObject player in players ) {
                int index;
                do {
                    index = Random.Range(0, spawningPositions.Length);
                } while ( usedSpawn.Contains(index) );

                Debug.Log("Spawn at pos index: "+index);
                Vector3 pos = spawningPositions[index].transform.position;
                // player.SpawnAt(pos);
                usedSpawn.Add(index);
            }

            roundRunning = true;
        }


        public void EndRound(PlayerController winner) {
            // Find winning player
            Debug.Log("End Of Round!");
            if ( winner ) {
                Debug.Log("Winner is player # : " + winner.player);
            } else {
                Debug.Log("Winner is player # : DRAW");
            }

            roundRunning = false;
        }

    }
}