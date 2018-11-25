using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PlayerController = Entities.Player.PlayerController;
using Random = UnityEngine.Random;

namespace Core {
    public class RoundsController : MonoBehaviour {

        private bool roundRunning = false;

        public GameObject[] players; // The 4 players in the scene
        public GameObject[] spawningPositions;

        [Serializable]
        public class EndRoundEvent : UnityEvent<PlayerController> { }

        public UnityEvent    beginRound = new UnityEvent();
        public EndRoundEvent endRound   = new EndRoundEvent();


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
            foreach ( GameObject obj in players ) {
                PlayerController player = obj.GetComponent<PlayerController>();
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

            GameController.Instance.ResetState();
            // Spawn players to starting positions
            List<int> usedSpawn = new List<int>();
            int index = 0;
            foreach ( GameObject player in players ) {
                Vector3          pos              = spawningPositions[index].transform.position;
                PlayerController playerController = player.GetComponent<PlayerController>();
                playerController.ResetState();
                playerController.SpawnAt(pos);
                usedSpawn.Add(index++);
            }

            roundRunning = true;
            beginRound.Invoke();
        }


        public void EndRound(PlayerController winner) {
            if ( roundRunning ) {
                roundRunning = false;
                endRound.Invoke(winner);
            }
        }

    }
}