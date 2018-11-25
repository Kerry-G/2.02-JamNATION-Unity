using System.Collections;
using Core;
using Rewired;
using RewiredConsts;
using UnityEngine;

namespace Entities.Player {
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour {

        [PlayerIdProperty(typeof(RewiredConsts.Player))]
        public int player;

        public int   numberOfLives         = 1;
        public float walkingSpeed          = 5f;
        public int   movementRotationSpeed = 100;
        public int   shootingRotationSpeed = 200;

        private Rigidbody      _rb;
        private Animator       _animator;
        private GamePhase _phase;

        public Rewired.Player PlayerInputs { get; protected set; }

        public bool HasLives {
            get { return numberOfLives > 0; }
        }

        // ========================================================
        // ========================================================


        protected void Awake() {
            GameController.Instance.onChangePhase.AddListener(ChangeGamePhase);

            PlayerInputs = ReInput.players.GetPlayer(player); // Get the MainPlayer's inputs
            _rb          = GetComponent<Rigidbody>();
        }


        private void Update() {
            MoveForward();
            CheckForRotation();
//            StartCoroutine(KillEveryone());
        }


        IEnumerator KillEveryone() {
            yield return new WaitForSeconds(5f);
            if (gameObject.name != "Player2") {Kill();}
        }
        

        // ========================================================
        // ========================================================


        /// <summary>
        /// Move the gameobject forward in local space
        /// </summary>
        private void MoveForward() {
            Vector3 locVel = transform.InverseTransformDirection(_rb.velocity);
            if ( _phase == GamePhase.Moving )
                locVel = new Vector3(0, 0, walkingSpeed); // Move forward in local space
            else
                locVel = new Vector3(0, 0, 0);

            _rb.velocity = transform.TransformDirection(locVel);

            // Reset Angular Velocity (Manual movement only)
            _rb.angularVelocity = Vector3.zero;
        }


        /// <summary>
        /// Spawn Player at given position
        /// </summary>
        /// <param name="pos"></param>
        public void SpawnAt(Vector3 pos) {
            transform.position = pos;
            gameObject.SetActive(true);
        }


        /// <summary>
        /// Kill Event
        /// External call
        /// </summary>
        public void Kill() {
            // Spawn particles ???
//            StartCoroutine(dd());
            gameObject.SetActive(false);
            numberOfLives--;

            AkSoundEngine.PostEvent("Play_PlayerKilled", gameObject);
        }


//        IEnumerator dd() {
//            yield return new WaitForSeconds(0.1f);
//            numberOfLives--;
//            gameObject.SetActive(false);
//        }


        /// <summary>
        /// Receive message with the current gamePhase
        /// </summary>
        /// <param name="phase"></param>
        public void ChangeGamePhase(GamePhase phase) { _phase = phase; }


        /// <summary>
        /// Check inputs for rotating the player
        /// </summary>
        private void CheckForRotation() {
            float horizontal = PlayerInputs.GetAxisRaw(Action.Horizontal);
            float vertical   = PlayerInputs.GetAxisRaw(Action.Vertical);

            // When moving (Type 1)
            if ( _phase == GamePhase.Moving ) {
                if ( !Mathf.Approximately(horizontal, 0f) )
                    transform.Rotate(Vector3.up * movementRotationSpeed * Mathf.Sign(horizontal) * Time.deltaTime);
            } else if ( _phase == GamePhase.Shooting ) {
                if ( !Mathf.Approximately(horizontal, 0f) )
                    transform.Rotate(Vector3.up * shootingRotationSpeed * Mathf.Sign(horizontal) * Time.deltaTime);
            }
        }


        public void ResetState() {
            numberOfLives = 1;
            
        }

    }
}