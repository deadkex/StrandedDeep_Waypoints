using UnityEngine;

namespace SDWaypoints
{
    public class Loader
    {
        public static GameObject myobj;
        public static void Load()
        {
            Loader.myobj = new GameObject();
            Loader.myobj.AddComponent<Menu>();
            UnityEngine.Object.DontDestroyOnLoad(Loader.myobj);
        }
        public static void Unload()
        {
            GameObject.Destroy(Loader.myobj);
        }
    }
}