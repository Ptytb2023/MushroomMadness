using MushroomMadness.Controllers;
using System.Collections;
using UnityEngine;

namespace MushroomMadness.Player
{
    [RequireComponent(typeof(MovePlayerConroller))]
    public class Player : MonoBehaviour
    {
        private MovePlayerConroller _conroller;
        private Vector3 _startPosition;
        private const float _timeOffMove = 1f;

        private void Start()
        {
            _conroller = GetComponent<MovePlayerConroller>();
            _startPosition = transform.position;
        }

        public void ResetPlayer()
        {
            StartCoroutine(TurnOffMove());
        }

        private IEnumerator TurnOffMove()
        {
            float elapsedTime = _timeOffMove;
            OffMoveController();

            transform.position = _startPosition;

            while (elapsedTime > 0 && enabled)
            {
                elapsedTime -= Time.deltaTime;
                yield return null;
            }

            OnMoveController();
        }

        public void OffMoveController()
        {
            _conroller.ResetMove();
            _conroller.enabled = false;
        }

        public void OnMoveController()
        {
            _conroller.enabled = true;
        }
    }
}