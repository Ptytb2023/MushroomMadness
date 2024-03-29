using MushroomMadness.Controllers;
using System.Collections;
using UnityEngine;
using Zenject;

namespace MushroomMadness.Player
{
    [RequireComponent(typeof(MovePlayerConroller))]
    public class Player : MonoBehaviour
    {
        private MovePlayerConroller _conroller;
        private Vector3 _startPosition;
        private const float _timeOffMove = 1f;

        [Inject]
        private IinputInterface _input;

        private void Start()
        {
            _conroller = GetComponent<MovePlayerConroller>();
            _startPosition = transform.position;
        }

        public void ResetPlayer()
        {
            StartCoroutine(TurnOffMoveOnSecond());
        }

        private IEnumerator TurnOffMoveOnSecond()
        {
            float elapsedTime = _timeOffMove;
            SetActiveMove(false);

            transform.position = _startPosition;

            while (elapsedTime > 0 && enabled)
            {
                elapsedTime -= Time.deltaTime;
                yield return null;
            }

            SetActiveMove(true);
        }


        public void SetActiveMove(bool active)
        {
            if (active)
                _conroller.enabled = true;
            else if (!active)
            {
                _conroller.ResetMove();
                _conroller.enabled = false;
            }
        }
       
    }
}