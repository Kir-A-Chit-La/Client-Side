using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    public int HorizontalVelocityHash { get; private set; }
    public int VerticalVelocityHash { get; private set; }
    public int CrouchingHash { get; private set; }
    public int JumpPreparationHash { get; private set; }
    public int LeapOffHash { get; private set; }
    public int FallHash { get; private set; }
    public int LandHash { get; private set; }
    public int PerformHash { get; private set; }
    public int TreeSlashHash { get; private set; }

    public void SetInteger(int hash, int value) => _animator.SetInteger(hash, value);
    public void SetFloat(int hash, float value) => _animator.SetFloat(hash, value);
    public void SetFloat(int hash, float value, float dampTime, float deltaTime) => _animator.SetFloat(hash, value, dampTime, deltaTime);
    public void SetBool(int hash, bool value) => _animator.SetBool(hash, value);
    public void SetTrigger(int hash) => _animator.SetTrigger(hash);
    public AnimatorStateInfo GetCurrentState(int layerIndex) => _animator.GetCurrentAnimatorStateInfo(layerIndex);
    public void ResetVelocity()
    {
        _animator.SetFloat(HorizontalVelocityHash, 0f);
        _animator.SetFloat(VerticalVelocityHash, 0f);
    }
    public void Init()
    {
        _animator = GetComponent<Animator>();
        
        HorizontalVelocityHash = Animator.StringToHash("Horizontal Velocity");
        VerticalVelocityHash = Animator.StringToHash("Vertical Velocity");
        CrouchingHash = Animator.StringToHash("Crouch");
        JumpPreparationHash = Animator.StringToHash("Prepare Jump");
        LeapOffHash = Animator.StringToHash("Leap Off");
        FallHash = Animator.StringToHash("Fall");
        LandHash = Animator.StringToHash("Land");
        PerformHash = Animator.StringToHash("Perform");
        TreeSlashHash = Animator.StringToHash("Tree Slash");
    }
}
