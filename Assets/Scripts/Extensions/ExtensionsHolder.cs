using UnityEngine;


namespace FruitScapes.Extensions
{
    [System.Serializable]
    public class ObjectAppearance
    {
        public Sprite Sprite;
        public int Id;
    }

    [System.Serializable]
    public class FruitWithDir
    {
        public GameObject Fruit;
        public Vector2Int Dir;

        public FruitWithDir(GameObject currentFruit, Vector2Int direction)
        {
            Fruit = currentFruit;
            Dir = direction;
        }

        public FruitWithDir() { }
    }

    public enum GameState
    {
        Move = 0,
        Wait
    }
}
