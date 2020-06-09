using System.Collections;
using System.Collections.Generic;
using Scripts.UI;
using UnityEngine;

namespace Core.BoardNS
{
    public class BoardManager : MonoBehaviour
    {
        //Obviously missing GameManager but we don't have many components like ADS, SOUND, VIBRATION, ANALYZER so that can be done without it for now.
        public static BoardManager instance;
        private List<Board> boards;
        private int currentBoardIndex;
        [SerializeField] private HoleMovement holeMovement;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private LevelView levelView;
        [SerializeField] private RoadFiller roadFiller;
        [SerializeField] private ColorChanger colorChanger;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            StartBoard();
        }

        private void StartBoard()
        {
            boards = LevelManager.instance.NextLevel();
            SetCurrentBoard(0);
            holeMovement.MoveToTheTargetPosition(boards[currentBoardIndex].GetHolePosition());
            cameraController.SetCameraPosition(boards[currentBoardIndex].GetCameraPosition());
            roadFiller.Fill(boards[currentBoardIndex].GetFillerPosition(), boards[currentBoardIndex + 1].GetFillerPosition());
            StartCoroutine(WaitAndOpenController(0.5f));
        }
        
        private void MoveToNextBoard()
        {
            StartCoroutine(WaitAndOpenController(1.5f));
            boards[currentBoardIndex].CollectRemains();
            holeMovement.MoveToTheNextBoard(boards[currentBoardIndex + 1].GetHolePosition());
            StartCoroutine(cameraController.MoveToNextBoardEnumerator(boards[currentBoardIndex].GetCameraPosition(),
                boards[currentBoardIndex + 1].GetCameraPosition()));
            SetCurrentBoard(currentBoardIndex + 1);
        }
        
        private void SetCurrentBoard(int board)
        {
            currentBoardIndex = board;
            boards[currentBoardIndex] = boards[board];
            boards[currentBoardIndex].BroadCastLevel = levelView.SetPercentage;
            levelView.SetLevel(LevelManager.instance.CurrentLevel);
        }
        
        public void BoardCollected(Board board)
        {
            if (board.Id == 0)
            {
                Controller.instance.TurnOffController();
                MoveToNextBoard();
            }
            else
            {
                Controller.instance.TurnOffController();
                NextLevel();
            }
        }
        
        private void NextLevel()
        {
            colorChanger.ChangeColor();
            boards[currentBoardIndex].CollectRemains();
            boards.ForEach(board => BoardPool.instance.AddToPool(board));
            boards = LevelManager.instance.NextLevel();
            PrepareBoard();
            levelView.ResetPercentage();
        }

        public void RefreshLevel()
        {
            Controller.instance.TurnOffController();
            boards.ForEach(board => board.CollectRemains());
            boards.ForEach(board => BoardPool.instance.AddToPool(board));
            boards = LevelManager.instance.RefreshLevel();
            PrepareBoard();
            levelView.ResetPercentage();
        }

        private void PrepareBoard()
        {
            SetCurrentBoard(0);
            holeMovement.MoveToTheTargetPosition(boards[currentBoardIndex].GetHolePosition());
            cameraController.SetCameraPosition(boards[currentBoardIndex].GetCameraPosition());
            roadFiller.Fill(boards[currentBoardIndex].GetFillerPosition(), boards[currentBoardIndex + 1].GetFillerPosition());
            StartCoroutine(WaitAndOpenController(0.5f));
        }

        private IEnumerator WaitAndOpenController(float time)
        {
            yield return new WaitForSeconds(time);
            Controller.instance.TurnOnController();
            Controller.instance.Limist = boards[currentBoardIndex].GetLimits();
        }

        public Board GetCurrentBoard()
        {
            return boards[currentBoardIndex];
        }
    }
}
