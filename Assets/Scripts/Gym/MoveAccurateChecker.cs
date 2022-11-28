using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveAccurateChecker : MonoBehaviour
{
    private static MoveAccurateChecker _instance;
    public static MoveAccurateChecker Instance
    { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public List<Button> buttons;

    [SerializeField] private GymMoves gymMoves;
    [SerializeField] private float scoreIncrease;
    [SerializeField] private ScoreBarController scoreBarController;

    private moves currentMove;
    private moves nextMove;

    public Dictionary<moves, GameObject> movesAndImages;

    public static bool canMove;
    [SerializeField] private Animator gymPlayerAnimator;

    private void Start()
    {
        canMove = true;
        movesAndImages = gymMoves.movesAndImages;
        SetRandomMove();
    }

    public void SetRandomMove()
    {
        nextMove = (moves)Random.Range(1, 5);
        while (nextMove.Equals(currentMove))
            nextMove = (moves)Random.Range(1, 5);
        currentMove = nextMove;
        foreach (GameObject obj in movesAndImages.Values)
        {
            obj.SetActive(false);
        }

        movesAndImages[currentMove].SetActive(true);
    }

    public void CheckAccurate(int moveInput)
    {
        if (canMove)
        {
            if (moveInput.Equals((int)currentMove))
            {
                foreach (Button btn in buttons)
                    btn.interactable = false;
                scoreBarController.AddScore(scoreIncrease);
                gymPlayerAnimator.SetTrigger(((moves)moveInput).ToString());
                //SetRandomMove();
                canMove = false;
                // bool isFinished = scoreBarController.IsFinished();
                //if (isFinished)//     GameOver();
                // Debug.Log("Finished");
            }
            else
                scoreBarController.AddScore(-scoreIncrease);
        }
    }

    private void Update()
    {
        //    if (Input.GetMouseButtonDown(0))
        //        SetRandomMove();
    }
}