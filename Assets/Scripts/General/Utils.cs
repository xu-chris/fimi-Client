using UnityEngine;

namespace General
{
    public static class Utils
    {
        public static GameObject GetOrInstantiate(string nameOfGameObject, GameObject prefab)
        {
            var gameObjectToBeFound = GameObject.Find(nameOfGameObject);
            
            if (gameObjectToBeFound != null) return gameObjectToBeFound;
            GameObject.Instantiate(prefab);
            gameObjectToBeFound  = GameObject.Find(nameOfGameObject);

            return gameObjectToBeFound;
        }
        
        public static GameObject GetOrInstantiate(Tag tag, GameObject prefab)
        {
            var gameObjectToBeFound = GameObject.FindGameObjectWithTag(tag.ToString());
            
            if (gameObjectToBeFound != null) return gameObjectToBeFound;
            GameObject.Instantiate(prefab);
            gameObjectToBeFound  = GameObject.FindGameObjectWithTag(tag.ToString());

            return gameObjectToBeFound;
        }
    }
}