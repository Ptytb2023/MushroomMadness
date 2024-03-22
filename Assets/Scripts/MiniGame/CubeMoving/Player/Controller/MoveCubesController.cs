using MushroomMadness.InputSystem;
using System.Collections;
using UnityEngine;
using Zenject;

namespace MiniGame.MovingCubes.Controller
{
    public class MoveCubesController : MonoBehaviour
    {
         private AssistanCubes _assistan;

        [Inject]
        private IInputMove _inputMove;

        private Coroutine _moving;

        public void Init(AssistanCubes assistan)
        {
            _assistan = assistan;
        }

        public void OnEnable()
        {
            _inputMove.ClickMove += OnClickMove;
        }

        private void OnDisable()
        {
            _inputMove.ClickMove -= OnClickMove;
        }

        private void OnClickMove(bool isPress)
        {
            if (_moving != null)
                StopCoroutine(_moving);

            if (!isPress)
                return;

            _moving = StartCoroutine(SetDirectionAndMove());
        }

        private IEnumerator SetDirectionAndMove()
        {
            while (enabled)
            {
                var direction = _inputMove.GetDirectionMove();

                foreach (var cube in _assistan._cubes)
                {
                    cube.Move(direction);
                }

                yield return null;
            }
        }

    }
}