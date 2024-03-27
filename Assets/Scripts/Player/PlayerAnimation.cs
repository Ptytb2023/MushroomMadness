using MushroomMadness.Controllers;
using UnityEngine;

namespace MushroomMadness.Player.Animation
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private MovePlayerConroller _controller;

        private Animator _animator;

        private const string _nameRun = "Run";
        private const string _nameJump = "Jumping";

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _controller.Jump += SetJump;
            _controller.Run += SetRun;
        }

        private void OnDisable()
        {
            _controller.Jump -= SetJump;
            _controller.Run -= SetRun;
        }

        private void SetJump(bool isJump)
        {
            if (isJump)
                _animator.SetTrigger(_nameJump);
            else
                _animator.SetBool(_nameJump, false);
        }
        private void SetRun(bool isRun) => _animator.SetBool(_nameRun, isRun);


    }
}