using System;
using System.Timers;
using Core.Inputs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core {
    public class GameController : MonoBehaviour {

        private static GameController _instance; // Singleton instance

        public static GameController Instance {
            get { return _instance; }
        }

        public float movementPhaseDuration = 5.0f;
        public float shootingPhaseDuration = 1.0f;

        private bool      _testingMode = false; // Define if we are testing a scene alone or using the SceneController Loader
        private GamePhase _gamePhase   = GamePhase.Moving;

        private float _timer;

        /// References
        private GameObject _player1;
        private GameObject _player2;
        private GameObject _player3;
        private GameObject _player4;

        [SerializeField] private SceneController _sceneController;


        // ====================================
        // ====================================

        /// <summary>
        /// Accessors for different important Components
        /// </summary>

        public GameObject Player1 {
            get {
                if ( _player1 == null ) _player1 = GameObject.Find("Player1");
                return _player1;
            }
        }

        public GameObject Player2 {
            get {
                if ( _player2 == null ) _player2 = GameObject.Find("Player2");
                return _player2;
            }
        }

        public GameObject Player3 {
            get {
                if ( _player3 == null ) _player3 = GameObject.Find("Player3");
                return _player3;
            }
        }

        public GameObject Player4 {
            get {
                if ( _player4 == null ) _player4 = GameObject.Find("Player4");
                return _player4;
            }
        }

        public SceneController SceneController {
            get { return _sceneController; }
        }


        public ActionsMapsHelper actionsMapsHelper { get; protected set; }

        // ====================================
        // ====================================


        /// <summary>
        /// Brute constructor.
        /// Called even before Awake() calls.
        /// </summary>
        public GameController() {
            // Setup Singleton
            if ( _instance == null )
                _instance = this;
            else if ( _instance != this ) Destroy(gameObject);
        }


        private void Awake() {
            // Define if we are testing a scene
            if ( SceneManager.GetActiveScene().name != "Persistent" ) {
                _testingMode = true;
            }

            actionsMapsHelper = new ActionsMapsHelper();
        }


        private void Update() { gamePhaseManager(); }


        private void gamePhaseManager() {
            _timer += Time.deltaTime;
            if ( _gamePhase == GamePhase.Moving && _timer > movementPhaseDuration ) {
                setGamePhase(GamePhase.Shooting);
            }  else if ( _gamePhase == GamePhase.Shooting && _timer > shootingPhaseDuration ) {
                BroadcastToAllPlayers("Shoot");
                setGamePhase(GamePhase.Moving);
            }
        }


        private void setGamePhase(GamePhase gamePhase) {
            switch ( gamePhase ) {
                case GamePhase.Moving:
                    setMovingGamePhase();
                    break;
                case GamePhase.Shooting:
                    setShootingGamePhase();
                    break;
                default: throw new ArgumentOutOfRangeException("gamePhase", gamePhase, null);
            }

            broadcastGamePhase();
            resetTimer();
            if ( _testingMode ) {
                printPhase();
            }
        }

        // ========================================================
        // ========================================================
        // ========================================================


        public bool IsTesting() { return _testingMode; }
        
        /// <summary>
        /// Quit the entire application (Only in builds)
        /// </summary>
        public void KillGame() { Application.Quit(); }

        private void setMovingGamePhase() { _gamePhase = GamePhase.Moving; }

        private void setShootingGamePhase() { _gamePhase = GamePhase.Shooting; }

        private void printPhase() { Debug.Log("Game Phase is " + _gamePhase); }

        private void resetTimer() { _timer = 0f; }
        
        private void broadcastGamePhase() { BroadcastToAllPlayers("ChangeGamePhase", _gamePhase); }

        private void BroadcastToAllPlayers(string functionCall, object message = null) {
            if ( Player1 != null ) Player1.BroadcastMessage(functionCall, message);
            if ( Player2 != null ) Player2.BroadcastMessage(functionCall, message);
            if ( Player3 != null ) Player3.BroadcastMessage(functionCall, message);
            if ( Player4 != null ) Player4.BroadcastMessage(functionCall, message);
        }

    }

    public enum     GamePhase {
        Moving,
        Shooting
    }

// Class
}