using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersMovements : MonoBehaviour {

    [Header("Positions")] public Transform idlePosition;
    public Transform fightPosition;

    [Header("Scripts")] public GameplayManager gameplayManager;
    
    
    
    
    private Animator _characterAnimator;

    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsFighting = Animator.StringToHash("isFighting");
    private static readonly int IsDead = Animator.StringToHash("isDead");

    // Start is called before the first frame update
    void Start() {
        _characterAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLost() {
        _characterAnimator.SetBool(IsDead, true);
    }

    public void PlayWin() {
        _characterAnimator.SetBool(IsFighting, false);
    }

    public void Move(Vector3 target) {
        float step = gameplayManager.speed * Time.deltaTime;
        Vector3 position = transform.position;
        position = Vector3.MoveTowards(position, target, step);
        transform.position = position;
        _characterAnimator.SetBool(IsWalking, position != target);
        _characterAnimator.SetBool(IsFighting, position == fightPosition.position);
        // Debug.Log(gameObject.name + ": (State: " +
        //           _characterAnimator.GetAnimatorTransitionInfo(0) +
        //           ", IsWalking: " + _characterAnimator.GetBool(IsWalking));
    }

    public bool AnimationEnded() {
        // Debug.Log(_characterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime + " / " + 1);
        return _characterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }

    public void PlaySwordSound() {
        gameplayManager.swordClash.Play();
    }

    public void PlaySoundWalkL() {
        gameplayManager.grassWalkL.Play();
    }

    public void PlaySoundWalkR() {
        gameplayManager.grassWalkR.Play();
    }

    public void PlaySoundDeath() {
        gameplayManager.death.Play();
    }
}
