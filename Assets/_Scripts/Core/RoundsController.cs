using System;
using System.Collections;
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

        public string[] audioEventsNames = new string[3];

        private void Start() {
            //
            GameController.Instance.onChangePhase.AddListener(ListenChangePhase);

            StartRound();
            StartCoroutine(DetectEndRound());
        }


        /// <summary>
        /// Play audios
        /// </summary>
        /// <param name="phase"></param>
        private void ListenChangePhase(GamePhase phase) {
            int rtpcValue = 25;

            if (phase == GamePhase.Shooting) {
                // Play Audios
                int alive = CountAlivePlayers();
                int index = 0;
                if ( alive == 3 ) {
                    index = 1;
                    rtpcValue = 50;
                } else if ( alive < 3 ) {
                    index = 2;
                    rtpcValue = 100;
                }

                AkSoundEngine.SetRTPCValue("Intensity", rtpcValue);
                AkSoundEngine.PostEvent(audioEventsNames[index], gameObject);
            }
        }


        private void Update() {
//            DetectEndRound();
        }


        public int CountAlivePlayers() {
            int hasLivesPlayerCount = 0;
            foreach ( GameObject obj in players ) {
                PlayerController player = obj.GetComponent<PlayerController>();
                if ( player.HasLives ) {
                    hasLivesPlayerCount++;
                }
            }

            return hasLivesPlayerCount;
        }


        IEnumerator DetectEndRound() {
            while ( true ) {
                yield return new WaitForSeconds(.2f);

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
        }


//        private void DetectEndRound() {
//                int              hasLivesPlayerCount = 0;
//                PlayerController winningPlayer       = null;
//                foreach ( GameObject obj in players ) {
//                    PlayerController player = obj.GetComponent<PlayerController>();
//                    if ( player.HasLives ) {
//                        hasLivesPlayerCount++;
//                        winningPlayer = player;
//                    }
//                }
//
//                if ( hasLivesPlayerCount <= 1 ) EndRound(winningPlayer);
//        }


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
            int       index     = 0;
            foreach ( GameObject player in players ) {
                Vector3          pos              = spawningPositions[index].transform.position;
                PlayerController playerController = player.GetComponent<PlayerController>();
                playerController.ResetState();
                playerController.SpawnAt(pos);
                playerController.transform.LookAt(Vector3.zero);
                usedSpawn.Add(index++);
            }

            StartCoroutine(CountDownWithoutTimeScale(1, 2));
            Time.timeScale = 0;

            roundRunning = true;
            beginRound.Invoke();
        }


        public void EndRound(PlayerController winner) {
            if ( roundRunning ) {
                roundRunning = false;
                endRound.Invoke(winner);
            }
        }


        public static IEnumerator CountDownWithoutTimeScale(int interval, int duration) {
            while ( true ) {
                float pauseEndTime = Time.realtimeSinceStartup + duration;
                while (Time.realtimeSinceStartup < pauseEndTime)
                {
                    yield return 0;
                }

                Time.timeScale = 1;
                break;
            }
        }

    }
}