using MiniGame.CubesMoving.Cube;
using MushroomMadness.InputSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MiniGame.CubesMoving.Controller
{
    public class MoveCubesController : MonoBehaviour
    {
        [SerializeField] private ContenerCubs _contener;
        [SerializeField] private float _speed;

        private IInputMove _inputMove;

        private List<CubeMoving> _cubes;

        private Coroutine _moving;

        private void Start()
        {
            _cubes = _contener.GetCubes().ToList();
        }

        public void Init(IInputMove input)
        {
            _inputMove = input;
            _inputMove.ClickMove += OnClickMove;
        }
     
        private void OnDestroy()
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

                foreach (var cube in _cubes)
                {
                    cube.Move(direction, _speed);
                }

                yield return null;
            }
        }

    }
}