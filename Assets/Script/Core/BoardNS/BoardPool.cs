using System.Collections.Generic;
using UnityEngine;

namespace Script.Core.BoardNS
{
    public class BoardPool : MonoBehaviour
    {
        // may we can create base pool class or interface for shapepool and boardpool
        public static BoardPool instance;
        [SerializeField] private GameObject[] boardPrefabs;
        public Dictionary<BoardType, List<GameObject>> boards;
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
            boards = new Dictionary<BoardType, List<GameObject>>();
        }
        
        public void AddToPool(Board board)
        {
            board.transform.position = Global.farPosition;
            BoardType boardType = board.BoardType;
            if (boards.ContainsKey(boardType))
            {
                boards[boardType].Add(board.gameObject);
            }
            else
            {
                var newTypeList = new List<GameObject> {board.gameObject};
                boards.Add(boardType, newTypeList);
            }
            board.gameObject.SetActive(false);
        }

        public GameObject GetBoardGameObject(BoardType boardType)
        {
            if (boards.ContainsKey(boardType))
            {
                List<GameObject> boardList = boards[boardType];
                if (boardList.Count == 0)
                {
                    return InstantiateBoard(boardType);
                }
                else
                {
                    var willReturn = boardList[boardList.Count - 1];
                    boardList.RemoveAt(boardList.Count - 1);
                    willReturn.gameObject.SetActive(true);
                    return willReturn;
                }
            }
            else
            {
                return InstantiateBoard(boardType);
            }
        }
        
        private GameObject InstantiateBoard(BoardType boardType)
        {
            var newGameObject = 
                Instantiate(boardPrefabs[(int) boardType],
                    Global.farPosition, Quaternion.identity);
            return newGameObject;
        }
    }
}