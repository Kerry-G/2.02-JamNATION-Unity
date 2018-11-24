using UnityEngine;
using Rewired;

namespace Entities.Player {
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour {

        [PlayerIdProperty(typeof(RewiredConsts.Player))]
        public int player;

        public float walkingSpeed          = 5f;
        public float movementRotationSpeed = 5f;
        public float shootingRotationSpeed = 5f;


        private Rigidbody                     _rb;
        private Animator                      _animator;
        private Core.GameController.GamePhase _phase;

        public Rewired.Player PlayerInputs { get; protected set; }

        // ========================================================
        // ========================================================


        protected void Awake() {
            PlayerInputs = ReInput.players.GetPlayer(player); // Get the MainPlayer's inputs
            _rb          = GetComponent<Rigidbody>();
        }


        private void Update() {
            MoveForward();
            CheckForRotation();
        }


        // ========================================================
        // ========================================================


        /// <summary>
        /// Move the gameobject forward in local space
        /// </summary>
        private void MoveForward() {
            Vector3 locVel = transform.InverseTransformDirection(_rb.velocity);
            Debug.Log(locVel);
            locVel.z     = walkingSpeed; // Move forward in local space
            _rb.velocity = transform.TransformDirection(locVel);
        }


        /// <summary>
        /// Receive message with the current gamePhase
        /// </summary>
        /// <param name="phase"></param>
        public void ChangeGamePhase(Core.GameController.GamePhase phase) { _phase = phase; }


        /// <summary>
        /// Check inputs for rotating the player
        /// </summary>
        private void CheckForRotation() {
            float horizontal = PlayerInputs.GetAxisRaw(RewiredConsts.Action.Horizontal);
            float vertical   = PlayerInputs.GetAxisRaw(RewiredConsts.Action.Vertical);

            // When moving
            if (_phase == Core.GameController.GamePhase) {

            }
        }

    }
}