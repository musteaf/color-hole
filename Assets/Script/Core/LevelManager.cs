using System.Collections.Generic;
using Script.Core.BoardNS;
using Script.Core.ShapeNS;
using Script.Generator.Model;
using UnityEngine;
using Shape = Script.Core.ShapeNS.Shape; 

namespace Script.Core
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;
        private Levels levels;

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
            levels = ResourceSaveAndLoad.DeSerializeLevels(Global.path);
        }

        public List<Board> NextLevel()
        {
            CurrentLevel += 1;
            return LoadLevel();
        }
        
        public List<Board> RefreshLevel()
        {
            return LoadLevel();
        }

        private List<Board> LoadLevel()
        {
            var level = levels.levelList[CurrentLevel - 1];
            var boards = new List<Board>();
            boards.Add(LoadBoard(level.board1,0, BoardType.Start));
            boards.Add(LoadBoard(level.board2,1, BoardType.End));
            return boards;
        }

        private Board LoadBoard(List<Generator.Model.Shape> shapes, int id, BoardType boardType)
        {
            var shapeSet = new HashSet<Shape>();
            var boardGameObject = BoardPool.instance.GetBoardGameObject(boardType);
            var board = boardGameObject.GetComponent<Board>();
            var boardBounds = board.MeshRenderer.bounds;
            var boardDistance = boardBounds.max.z - boardBounds.min.z;
            board.transform.position = new Vector3(0, 0, boardDistance * id);
            var acceptableShapeCount = 0;
            for (var i = 0; i < shapes.Count; i++)
            {
                var shapeGameObject = ShapePool.instance.GetShapeGameObject(shapes[i].shapeType,
                    shapes[i].acceptanceType);
                var producedShape = shapeGameObject.GetComponent<Shape>();
                producedShape.ActiveShape(shapes[i], i, board.GetMidPosition());
                shapeSet.Add(producedShape);
                if (shapes[i].acceptanceType == AcceptanceType.Acceptable)
                    acceptableShapeCount++;
                producedShape.Board = board;
            }
            board.Initialize(acceptableShapeCount, shapeSet, id);
            return board;
        }

        public int CurrentLevel { get; private set; }
    }
}