using UnityEngine;

namespace Tools
{
    /// <summary>
    /// Use to implement Singleton Pattern with Unity MonoBehaviour
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        protected static T _instance;

        /// <summary>
        /// Access to the instance.
        /// This may create the object if there is none.
        /// Use <see cref="internalInstance"/> if you want
        /// to access to the instance whithout modify the value
        /// </summary>
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<T>();
                }

                return _instance;
            }
        }

        /// <summary>
        /// If you want to check instance without modify it
        /// </summary>
        public static T internalInstance
        {
            get
            {
                return _instance;
            }
        }

        protected virtual void OnDestroy()
        {
            _instance = null;
        }
    }
}