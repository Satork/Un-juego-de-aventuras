using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour {
    [Header("Scripts")] public EndMenu endMenu;
    public Dialogues dialogues;

    [Header("Animation Speed & Times")] public float speed = 1f;
    public float fightTime = 3f;

    [Header("Animation")] public GameObject playerCharacterGameObject;
    public GameObject computerCharacterGameObject;

    [Header("Audio")] public AudioSource swordClash;
    public AudioSource grassWalkL;
    public AudioSource grassWalkR;
    public AudioSource death;

    private CharactersMovements _playerCharacter;
    private CharactersMovements _computerCharacter;

    private float _timer = 0f;

    public static bool IsAnswerSelectedStage1 { get; set; }
    private bool IsAnswerSelectedStage2 { get; set; }
    private bool IsAnswerSelectedStage3 { get; set; }

    public void Awake() {
        InsultsRecord.IsDeathAnimationEnded = false;
        IsAnswerSelectedStage1 = false;
        IsAnswerSelectedStage2 = false;
        IsAnswerSelectedStage3 = false;
    }

    private void Start() {
        _playerCharacter = playerCharacterGameObject.GetComponent<CharactersMovements>();
        _computerCharacter = computerCharacterGameObject.GetComponent<CharactersMovements>();
    }

    private void Update() {
        if (!dialogues.IsAnswerSelected) return;
        if (IsAnswerSelectedStage1) {
            if (playerCharacterGameObject.transform.position == _playerCharacter.fightPosition.position &&
                computerCharacterGameObject.transform.position == _computerCharacter.fightPosition.position) {
                IsAnswerSelectedStage1 = false;
                IsAnswerSelectedStage2 = true;
            }

            _playerCharacter.Move(_playerCharacter.fightPosition.position);
            _computerCharacter.Move(_computerCharacter.fightPosition.position);
        }

        if (IsAnswerSelectedStage2) {
            _timer += Time.deltaTime;
            // Debug.Log(_timer + "/" + fightTime);
            if (!(_timer > fightTime)) return;
            IsAnswerSelectedStage2 = false;
            IsAnswerSelectedStage3 = true;
            _timer = 0f;
        }

        if (!IsAnswerSelectedStage3) return;
        if (!InsultsRecord.IsFinish()) {
            if (playerCharacterGameObject.transform.position == _playerCharacter.idlePosition.position &&
                computerCharacterGameObject.transform.position == _computerCharacter.idlePosition.position) {
                IsAnswerSelectedStage3 = false;
                dialogues.IsAnswerSelected = false;
                dialogues.LoadNewInsult();
                dialogues.Enable(true);
            }

            _playerCharacter.Move(_playerCharacter.idlePosition.position);
            _computerCharacter.Move(_computerCharacter.idlePosition.position);
        }
        else {
            if (InsultsRecord.IsWin()) {
                _playerCharacter.PlayWin();
                _computerCharacter.PlayLost();
                // endMenu.ToggleEndMenu(true);
                // IsAnswerSelectedStage3 = false;
            }
            else {
                _playerCharacter.PlayLost();
                _computerCharacter.PlayWin();
                // if (!_playerCharacter.AnimationEnded()) return;
                // endMenu.ToggleEndMenu(true);
                // IsAnswerSelectedStage3 = false;
            }
            if (!(_computerCharacter.AnimationEnded() || _playerCharacter.AnimationEnded()) ) return;
            IsAnswerSelectedStage3 = false;
            endMenu.ToggleEndMenu(true);
        }
    }
}
