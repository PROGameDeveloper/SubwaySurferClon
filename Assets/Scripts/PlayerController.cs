using System;
using UnityEngine;

public enum Side { Left = -2, Middle = 0, Right= 2}


public class PlayerController : MonoBehaviour
{
    private Transform myTransform;
    private Animator _animator;
    public Animator Animator { get => _animator; set => _animator = value; }
    private CharacterController _myCharacterController;
    public CharacterController MyCharacterController { get => _myCharacterController; set => _myCharacterController = value; }
    private PlayerCollision playerCollision;
   

    private Side position;
    private Vector3 motionVector;

    [Header("Player Controller")]
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float dodgeSpeed;

    private float rollTimer;
    private float newXPosition;
    private float yPosition;
    private float xPosition;
    private int IdDodgeLeft = Animator.StringToHash("DodgeLeft");
    private int IdDodgeRight = Animator.StringToHash("DodgeRight");
    private int IdJump = Animator.StringToHash("Jump");
    private int IdFall = Animator.StringToHash("Fall");
    private int IdLanding = Animator.StringToHash("Landing");
    private int IdRoll = Animator.StringToHash("Roll");
    private int _IdStumbleLow = Animator.StringToHash("StumbleLow");
    public  int IdStumbleLow { get => _IdStumbleLow; set => _IdStumbleLow = value; }
    private int _IdDeathLower = Animator.StringToHash("DeathLower");
    public int IdDeathLower { get => _IdDeathLower; set => _IdDeathLower = value; }
    private int _IdDeathMovingTrain = Animator.StringToHash("DeathMovingTrain");
    public int IdDeathMovingTrain { get => _IdDeathMovingTrain; set => _IdDeathMovingTrain = value; }
    private int _IdDeathBounce = Animator.StringToHash("DeathBounce");
    public int IdDeathBounce { get => _IdDeathBounce; set => _IdDeathBounce = value; }
    private int _IdDeathUpper = Animator.StringToHash("DeathUpper");
    public int IdDeathUpper { get => _IdDeathUpper; set => _IdDeathUpper = value; }
    private int _IdStumbleCornerRight = Animator.StringToHash("StumbleCornerRight");
    public int IdStumbleCornerRight { get => _IdStumbleCornerRight; set => _IdStumbleCornerRight = value; }
    private int _IdStumbleCornerLeft = Animator.StringToHash("StumbleCornerLeft");
    public int IdStumbleCornerLeft { get => _IdStumbleCornerLeft; set => _IdStumbleCornerLeft = value; }
    private int _IdStumbleSideLeft = Animator.StringToHash("StumbleSideLeft");
    public int IdStumbleSideLeft { get => _IdStumbleSideLeft; set => _IdStumbleSideLeft = value; }
    private int _IdStumbleSideRight = Animator.StringToHash("StumbleSideRight");
    public int IdStumbleSideRight { get => _IdStumbleSideRight; set => _IdStumbleSideRight = value; }

    private int IdStumbleFall = Animator.StringToHash("StumbleFall");
    private int IdStumbleOffLeft = Animator.StringToHash("StumbleOffLeft");
    private int IdStumbleOffRight = Animator.StringToHash("StumbleOffRight");
   
  
 
    private bool swipeLeft, swipeRight, swipeUp, swipeDown;
    [Header("Player States")]
    [SerializeField] private bool isJumping;
    [SerializeField] private bool _isRolling;
    public bool IsRolling { get => _isRolling; set => _isRolling = value; }


    [SerializeField] private bool isGrounded;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _myCharacterController = GetComponent<CharacterController>();
        playerCollision = GetComponent<PlayerCollision>();
        position = Side.Middle;
        yPosition = -7;
    }

    void Update()
    {
        GetSwipe();
        SetPlayerPosition();
        MovePlayer();
        Jump();
        Roll();
        isGrounded = _myCharacterController.isGrounded;
    }

    private void GetSwipe()
    {
        swipeLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        swipeRight = Input.GetKeyDown(KeyCode.RightArrow);
        swipeUp = Input.GetKeyDown(KeyCode.UpArrow);
        swipeDown = Input.GetKeyDown(KeyCode.DownArrow);
    }

    private void SetPlayerPosition()
    {
        if (swipeLeft && !_isRolling)
        {
            if (position == Side.Middle)
            {
                UpdatePlayerXPosition(Side.Left);
                SetPlayerAnimator(IdDodgeLeft, false);
            }
            else if (position == Side.Right)
            {
                UpdatePlayerXPosition(Side.Middle);
                SetPlayerAnimator(IdDodgeLeft, false);
            }
        }
        else if (swipeRight && !_isRolling)
        {
            if (position == Side.Middle)
            {
                UpdatePlayerXPosition(Side.Right);
                SetPlayerAnimator(IdDodgeRight,false);
            }
            else if (position == Side.Left)
            {
                UpdatePlayerXPosition(Side.Middle);
                SetPlayerAnimator(IdDodgeRight, false);
            }
        }
    }

    private void UpdatePlayerXPosition(Side xPostion)
    {
        newXPosition = (int)xPostion;
        position = xPostion;
    }

    public void SetPlayerAnimator(int id, bool isCrossFade, float fadeTime = 0.1f)
    {
        _animator.SetLayerWeight(0, 1);
        if (isCrossFade)
        {
            _animator.CrossFadeInFixedTime(id, fadeTime);
        }
        else
        {
            _animator.Play(id);
        }
        ResetCollision();
    }

    public void SetPlayerAnimatorWithLayer(int id)
    {
        _animator.SetLayerWeight(1, 1);
        _animator.Play(id);

        ResetCollision();
    }

    private void ResetCollision()
    {
        Debug.Log(playerCollision.CollisionX.ToString() + " " + playerCollision.CollisionY.ToString() + " " + playerCollision.CollisionZ.ToString());
        playerCollision.CollisionX = CollisionX.None;
        playerCollision.CollisionY = CollisionY.None;
        playerCollision.CollisionZ = CollisionZ.None;
    }

    private void MovePlayer()
    {
        motionVector = new Vector3(xPosition - myTransform.position.x, yPosition * Time.deltaTime, forwardSpeed * Time.deltaTime);
        xPosition = Mathf.Lerp(xPosition, newXPosition, Time.deltaTime * dodgeSpeed);
        _myCharacterController.Move(motionVector);
    }

    private void Jump()
    {
        if (_myCharacterController.isGrounded)
        {
            isJumping = false;
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
            {
                SetPlayerAnimator(IdLanding, false);
            }
            if (swipeUp && !_isRolling)
            {
                isJumping = true;
                yPosition = jumpPower;
                SetPlayerAnimator(IdJump, true);
            }
        }
        else
        {
            yPosition -= jumpPower * 2 * Time.deltaTime;
            if (_myCharacterController.velocity.y <= 0)
                SetPlayerAnimator(IdFall, false);
        }
    }

    private void Roll()
    {
        rollTimer -= Time.deltaTime;
        if (rollTimer <= 0)
        {
            _isRolling = false;
            rollTimer = 0;
            _myCharacterController.center = new Vector3(0, .45f, 0);
            _myCharacterController.height = .9f;
        }

        if (swipeDown && !isJumping)
        {
            _isRolling = true;
            rollTimer = .5f;
            SetPlayerAnimator(IdRoll, true);
            _myCharacterController.center = new Vector3(0, .2f, 0);
            _myCharacterController.height = .4f;
        }
    }
}
