using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using FruitScapes.Extensions;
using FruitScapes.Data;
using FruitScapes.Components;

namespace FruitScapes.Tiles
{
    public class TilesController : MonoBehaviour
    {
        [SerializeField] private Tilemap _sandTileMap;
        [SerializeField] private ObjectsData _objectsData;
        [SerializeField] private GameObject _fruitPrefab;

        private GameObject _gemsContainer;
        private int _colMax;
        private int _rowMax;
        private int[,] _mapIDs;

        private void Start()
        {
            _colMax = _sandTileMap.size.y;
            _rowMax = _sandTileMap.size.x;
            _mapIDs = new int[_colMax, _rowMax];
            _gemsContainer = new GameObject("GemsContainer");
            GameObject[,] asd;
            GenerateInTiles(out asd);
        }

        public void GenerateInTiles(out GameObject[,] allObjects)
        {
            allObjects = new GameObject[_colMax, _rowMax];
            var allPositions = _sandTileMap.cellBounds.allPositionsWithin;

            for (int i = 0; i < _colMax; i++)
            {
                for (int j = 0; j < _rowMax; j++)
                {
                    allPositions.MoveNext();
                    if (_sandTileMap.GetTile(allPositions.Current) == null)
                    {
                        _mapIDs[i, j] = -1;
                        allObjects[i, j] = new GameObject(string.Format("Grass {0} {1}", i, j));
                        allObjects[i, j].transform.position = _sandTileMap.GetCellCenterWorld(allPositions.Current);
                        allObjects[i, j].transform.parent = _gemsContainer.transform;
                        continue;
                    }
                    allObjects[i, j] = CreateFruit(_sandTileMap.GetCellCenterWorld(allPositions.Current), i, j);
                }
            }

            Debug.Log("Frits Generated");
        }

        public GameObject CreateFruit(Vector3 pos, int col, int row)
        {
            GameObject newFruit = Instantiate(_fruitPrefab, pos, Quaternion.identity);
            newFruit.name = "Fruit";
            newFruit.GetComponent<MapObject>().SetColRow(col, row);
            SetAppearance(col, row, newFruit);
            newFruit.transform.parent = _gemsContainer.transform;
            return newFruit;
        }
        private void SetAppearance(int i, int j, GameObject fruit)
        {
            ObjectAppearance tempAppearance = _objectsData.GetRandomAppearance();
            Combine fruitCombine = fruit.GetComponent<Combine>();
            fruitCombine.Id = tempAppearance.Id;
            while (IMatch(i, j, fruitCombine.Id))
            {
                tempAppearance = _objectsData.GetRandomAppearance();
                fruitCombine.Id = tempAppearance.Id;
            }
            _mapIDs[i, j] = tempAppearance.Id;
            fruit.GetComponent<MapObject>().SetSprite(tempAppearance.Sprite);
        }

        private bool IMatch(int i, int j, int id)
        {
            if (i > 1 && j > 1)
            {
                if (_mapIDs[i - 1, j] == id && _mapIDs[i - 2, j] == id)
                {
                    return true;
                }
                if (_mapIDs[i, j - 1] == id && _mapIDs[i, j - 2] == id)
                {
                    return true;
                }
            }
            else if (i < 2 || j < 2)
            {
                if (j > 1)
                {
                    if (_mapIDs[i, j - 1] == id && _mapIDs[i, j - 2] == id)
                    {
                        return true;
                    }
                }
                if (i > 1)
                {
                    if (_mapIDs[i - 1, j] == id && _mapIDs[i - 2, j] == id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
