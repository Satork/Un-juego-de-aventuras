using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using InsultNode;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Dialogues : MonoBehaviour {
    [Header("Json File")] public TextAsset textJson;
    public DialoguesData dialoguesData;

    [Header("Answers Layout")] public GameObject buttonLayoutGameObject;
    public GameObject buttonAnswerPrefab;

    [Header("Insults Text")] public Text textUI;

    [Header("Score")] public Text playerScore;
    public Text computerScore;

    [Header("Dialogues Images")] public GameObject playerDialogueBubble;
    public GameObject computerDialogBubble;

    // [Header("Animation")] public GameObject playerCharacter;
    // public GameObject computerCharacter;
    // public float speed = 10f;
    //
    // [Header("Idle Positions")] public GameObject playerIdleGameObject;
    // public GameObject computerIdleGameObject;
    //
    // [Header("Fight Positions")] public GameObject playerFightGameObject;
    // public GameObject computerFightGameObject;

    // [Header("Scripts")] public EndMenu endMenu;
    //
    // private Animator _playerAnimator;
    // private Animator _computerAnimator;
    //
    // private Vector3 _playerFightPos;
    // private Vector3 _computerFightPos;
    // private Vector3 _playerIdlePos;
    // private Vector3 _computerIdlePos;

    
    public bool IsAnswerSelected { get; set; }
    // private bool IsOnTimerFight { get; set; }
    // private bool FlagFightAnim { get; set; }
    // private bool IsWin { get; set; }
    // private bool IsEnded { get; set; }

    private RectTransform _buttonLayout;
    private Button[] childButtons;
    private int _actualInsultId;
    // private static readonly int IsWalking = Animator.StringToHash("isWalking");
    // private static readonly int IsFighting = Animator.StringToHash("isFighting");
    //
    // public float runOutFightAnimationTime = 2f * 100f;
    // private float _timer;
    // private static readonly int IsDead = Animator.StringToHash("isDead");

    private void Awake() {
        //Animator instance
        // _playerAnimator = playerCharacter.GetComponent<Animator>();
        // _computerAnimator = computerCharacter.GetComponent<Animator>();
       
        //Position instance
        // _playerFightPos = playerFightGameObject.transform.position;
        // _computerFightPos = computerFightGameObject.transform.position;
        // _playerIdlePos = playerIdleGameObject.transform.position;
        // _computerIdlePos = computerIdleGameObject.transform.position;

        //Bool set
        IsAnswerSelected = false;
        _buttonLayout = buttonLayoutGameObject.GetComponent<RectTransform>();
        // IsOnTimerFight = false;
        // FlagFightAnim = false;
        // IsWin = false;
        // IsEnded = false;
    }

    private void Start() {
        LoadDialogues();
        LoadAnswers();
        LoadNewInsult();
    }

    private void Update() {
        /*
        if (IsAnswerSelected) {
            if (playerCharacter.transform.position == _playerFightPos &&
                computerCharacter.transform.position == _computerFightPos) {
                IsAnswerSelected = false;
                IsOnTimerFight = true;
                FlagFightAnim = true;
            }

            if (IsWin) {
                
            }

            MoveTo(playerCharacter, _playerFightPos, _playerAnimator);
            MoveTo(computerCharacter, _computerFightPos, _computerAnimator);
            _timer = 0f;
        }
        */

        /*
        if (IsOnTimerFight) {
            _timer += Time.deltaTime;

            if (_timer >= runOutFightAnimationTime) {
                FlagFightAnim = false;
                if (playerCharacter.transform.position == _playerIdlePos &&
                    computerCharacter.transform.position == _computerIdlePos) {
                    IsOnTimerFight = false;
                    LoadNewInsult();
                }

                if (!IsEnded) {
                    MoveTo(playerCharacter, _playerIdlePos, _playerAnimator);
                    MoveTo(computerCharacter,_computerIdlePos, _computerAnimator);
                }
                else {
                    if (IsWin) {
                        _playerAnimator.SetBool(IsFighting,false);
                        _computerAnimator.SetBool(IsDead, true);
                    }
                    else {
                        _computerAnimator.SetBool(IsFighting,false);
                        _playerAnimator.SetBool(IsDead, true);
                    }
                    
                }
                
            }
        }
    */
    }

    private void LoadAnswers() {
        Insult[] answers = dialoguesData.conversations[1].insults;
        float height = buttonAnswerPrefab.GetComponent<RectTransform>().rect.height;
        height *= 105f / 100f;

        Transform parent = _buttonLayout.parent;

        _buttonLayout.sizeDelta = new Vector2(parent.GetComponent<RectTransform>().rect.width,
                                             height * answers.Length);
        _buttonLayout.localPosition = new Vector3(0, -parent.GetComponent<RectTransform>().rect.height, 0);
        
        Rect rect = _buttonLayout.rect;
        float imageHeight = rect.height;

        foreach (Insult insult in answers) {
            GameObject buttonAnswerCopy = Instantiate(buttonAnswerPrefab, _buttonLayout, true);
            buttonAnswerCopy.name = "AnswerButton" + insult.id;

            RectTransform bacRect = buttonAnswerCopy.GetComponent<RectTransform>(); //bacRect = buttonAnswerCopyRect

            bacRect.localScale = Vector3.one;
            bacRect.localPosition = new Vector3(0, (imageHeight / 2) - height / 2 * (1 + 2 * (insult.id - 1)), 0);
            buttonAnswerCopy.GetComponentInChildren<Text>().text = insult.dialogue;

            FillListener(buttonAnswerCopy.GetComponent<Button>(), insult.id);
        }
        childButtons = buttonLayoutGameObject.gameObject.GetComponentsInChildren<Button>();
    }

    private void LoadDialogues() {
        dialoguesData = new DialoguesData();
        dialoguesData = JsonUtility.FromJson<DialoguesData>(textJson.text);

        playerScore.text = InsultsRecord.Win.ToString();
        computerScore.text = InsultsRecord.Lose.ToString();
    }

    private void FillListener(Button button, int id) {
        button.onClick.AddListener(() => { AnswerSelected(id); });
    }

    public void LoadNewInsult() {
        Random random = new Random();
        int nextInsultId = random.Next(1, dialoguesData.conversations[0].insults.Length + 1);
        _actualInsultId = nextInsultId;
        BubbleFlip(true, GetInsult(nextInsultId));
    }

    private string GetInsult(int nextInsultId) {
        foreach (var insult in dialoguesData.conversations[0].insults) {
            if (insult.id.Equals(nextInsultId)) {
                return insult.dialogue;
            }
        }
        return "Insult not found";
    }

    private void AnswerSelected(int id) {
        if (InsultsRecord.Rounds < InsultsRecord.RoundsMax) {
            if (_actualInsultId.Equals(id)) {
                InsultsRecord.Win++;
                playerScore.text = InsultsRecord.Win.ToString();
            }
            else {
                InsultsRecord.Lose++;
                computerScore.text = InsultsRecord.Lose.ToString();
            }
            IsAnswerSelected = true;
            GameplayManager.IsAnswerSelectedStage1 = true;
            Enable(false);
            InsultsRecord.Rounds++;
            if (InsultsRecord.Rounds != InsultsRecord.RoundsMax) {
                BubbleFlip(false, dialoguesData.conversations[1].insults[id - 1].dialogue);
            } 
            /*else if (InsultsRecord.Rounds == InsultsRecord.RoundsMax) {
                if (InsultsRecord.Win > InsultsRecord.Lose) {
                    IsWin = true;
                } else if (InsultsRecord.Win < InsultsRecord.Lose) {
                    IsWin = false;
                }

                IsEnded = true;
            }*/
        }
    }
    private void BubbleFlip(bool set, string dialogueText) {
        if (set) {
            playerDialogueBubble.SetActive(false);
            computerDialogBubble.SetActive(true);
        }
        else {
            playerDialogueBubble.SetActive(true);
            computerDialogBubble.SetActive(false);
        }

        textUI.text = dialogueText;
    }

    public void Enable(bool set) {
        foreach (var button in childButtons) {
            button.enabled = set;
        }
    }
    /*
    private void MoveTo(GameObject character, Vector3 targetPosition, Animator charAnimator) {
        if (FlagFightAnim) {
            charAnimator.SetBool(IsFighting, true);
            charAnimator.SetBool(IsWalking, false);
        }
        else {
            charAnimator.SetBool(IsFighting,false);
            charAnimator.SetBool(IsWalking, character.transform.position != targetPosition);
        }
        float step = speed * Time.deltaTime;
        character.transform.position = Vector3.MoveTowards(character.transform.position, targetPosition, step);
    }
*/
}
