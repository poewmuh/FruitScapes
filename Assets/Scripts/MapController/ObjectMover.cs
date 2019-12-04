using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FruitScapes.Components;
using FruitScapes.Tiles;
using FruitScapes.Events;
using FruitScapes.Extensions;
using UnityEngine.SceneManagement;

namespace FruitScapes.MapController
{
    public class ObjectMover : MonoBehaviour
    {
        [SerializeField] private TilesController _tilesHolder;

        private GameObject[,] _allObjects;
        private ObjectAnalys objAnalys;
        private ObjectFinder finder;
        private GameState gameState;
        private void Start()
        {
            _tilesHolder.GenerateInTiles(out _allObjects);
            EventHolder.moveEvent.AddListener(TryToChange);
            EventHolder.destroyEvent.AddListener(MoveDown);
            objAnalys = new ObjectAnalys();
            finder = new ObjectFinder();
            gameState = GameState.Move;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(0);
        }

        public GameObject[,] GetAllObjects()
        {
            return _allObjects;
        }

        public void TryToChange(FruitWithDir data)
        {
            if (gameState == GameState.Wait)
                return;
            Movable mainFruit = data.Fruit.GetComponent<Movable>();
            Movable otherFruit = mainFruit.GetNeighborMovable(data.Dir, _allObjects);

            if (CanChange(mainFruit, data.Dir))
            {
                gameState = GameState.Wait;
                ChangeFruits(mainFruit, otherFruit);
                EventHolder.startAnimation.Invoke();
                List<Combine> matchesList = finder.GetCombinedObjects(_allObjects);
                if (matchesList.Count == 0)
                {
                    StartCoroutine(RevertGems(mainFruit, otherFruit));
                }
                else
                {
                    objAnalys.MakeDamage(matchesList);
                    StartCoroutine(objAnalys.GemsDestroy(_allObjects, 0.4f));
                }
            }

        }

        private IEnumerator RevertGems(Movable fruit1, Movable fruit2)
        {
            yield return new WaitForSeconds(0.4f);
            ChangeFruits(fruit1, fruit2);
            EventHolder.startAnimation.Invoke();
            gameState = GameState.Move;
            yield break;
        }

        public void ChangeFruits(Movable fruit1, Movable fruit2, bool secondInstant = false, bool onlyLogic = false)
        {
            int oldCol = fruit1.GetCol;
            int oldRow = fruit1.GetRow;
            Vector2 oldPos = _tilesHolder.GetPosWithCoordinate(oldCol, oldRow);

            fruit1.SetColRow(fruit2.GetCol, fruit2.GetRow, _allObjects);
            fruit2.SetColRow(oldCol, oldRow, _allObjects);
            if (onlyLogic)
                return;
            fruit1.LerpMeTo(_tilesHolder.GetPosWithCoordinate(fruit1.GetCol, fruit1.GetRow));
            if (!secondInstant)
                fruit2.LerpMeTo(oldPos);
            else
                fruit2.transform.position = oldPos;
        }

        private bool CanChange(Movable movable, Vector2Int dir)
        {
            if (movable.CanFit(dir, _allObjects) == false)
                return false;
            if (movable.GetNeighborMovable(dir, _allObjects) == null)
                return false;
            return true;
        }

        private void MoveDown()
        {
            FallingDown();
            StartCoroutine(SpawnNewGems());

        }
        private IEnumerator SpawnNewGems()
        {
            yield return new WaitForSeconds(0.3f);
            bool iCreate = false;
            for (int j = 0; j < _allObjects.GetLength(1); j++)
            {
                if (_allObjects[_allObjects.GetLength(0) - 1, j].GetComponent<Empty>() != null)
                {
                    iCreate = true;
                    GameObject trash = _allObjects[_allObjects.GetLength(0) - 1, j];
                    _allObjects[_allObjects.GetLength(0) - 1, j] = _tilesHolder.CreateFruit(new Vector3(_allObjects[_allObjects.GetLength(0) - 1, j].transform.position.x, _allObjects[_allObjects.GetLength(0) - 1, j].transform.position.y + 0.82f), _allObjects.GetLength(0) - 1, j);
                    Destroy(trash);
                    _allObjects[_allObjects.GetLength(0) - 1, j].GetComponent<Movable>().SmoothCreate();
                }
                
            }
            List<Combine> matchesList = finder.GetCombinedObjects(_allObjects);
            if (iCreate)
            {
                MoveDown();
            }
            else if (matchesList.Count > 0)
            {
                objAnalys.MakeDamage(matchesList);
                StartCoroutine(objAnalys.GemsDestroy(_allObjects, 0.3f));
            }
            else
            {
                gameState = GameState.Move;
            }
            yield break;
        }

        private void FallingDown()
        {
            for (int j = 0; j < _allObjects.GetLength(1); j++)
            {
                Empty lastEmpty = finder.GetLastEmpty(j, _allObjects);
                if (lastEmpty == null)
                {
                    List<Movable> pairs = finder.GetDiogonalsPair(_allObjects);
                    if (pairs != null)
                    {
                        ChangeFruits(pairs[0], pairs[1], true);
                        j = -1;
                    }
                    continue;
                }
                Movable lastMovable = finder.GetLastMovable(j, _allObjects);
                if (lastMovable != null)
                {
                    ChangeFruits(lastMovable, lastEmpty.GetComponent<Movable>(), true);
                    j = -1;
                }
            }
            EventHolder.startAnimation.Invoke();
        }

    }
}
