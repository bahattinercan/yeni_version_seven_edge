using System.Collections.Generic;
using UnityEngine;

public class GymMoves : MonoBehaviour
{
    public Dictionary<moves, GameObject> movesAndImages = new Dictionary<moves, GameObject>();
    [SerializeField] private GameObject rPunchImage;
    [SerializeField] private GameObject lPunchImage;
    [SerializeField] private GameObject rKickImage;
    [SerializeField] private GameObject lKickImage;

    private void Awake()
    {
        movesAndImages.Add(moves.rPunch, rPunchImage);
        movesAndImages.Add(moves.lPunch, lPunchImage);
        movesAndImages.Add(moves.rKick, rKickImage);
        movesAndImages.Add(moves.lKick, lKickImage);
    }
}

public enum moves
{
    rPunch = 1,
    lPunch = 2,
    rKick = 3,
    lKick = 4
}