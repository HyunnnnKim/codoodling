using System.Collections.Generic;
using UnityEngine;

namespace Collie
{
    public class ObjectPool : MonoBehaviour
    {
        #region Singleton
        public static ObjectPool Instance = null;
        #endregion

        #region Serialize Field
        [Space, Header("Pools")]
        [SerializeField]
        private List<Pool> referencePools = null;
        #endregion

        #region Private Field
        private Dictionary<string, Queue<GameObject>> _objPools = null;
        #endregion

        private void Awake()
        {
            CreateInstance();
            InitVariables();
        }

        #region Initialize
        private void CreateInstance()
        {
            if (Instance == null) Instance = this;
        }

        private void InitVariables()
        {
            _objPools = new Dictionary<string, Queue<GameObject>>();
        }
        #endregion

        private void Start()
        {
            CreatePools();
        }

        #region Create Pools
        private void CreatePools()
        {
            foreach (Pool pool in referencePools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.Size; i++)
                {
                    // Pick random prefab from the prefab list.
                    int randomIndex = Random.Range(0, pool.Prefabs.Count - 1);
                    GameObject obj = Instantiate(pool.Prefabs[randomIndex]);

                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }
                _objPools.Add(pool.Tag.ToString(), objectPool);
            }
        }
        #endregion

        #region Controls
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public GameObject Spawn(string tag, Vector3 position, Quaternion rotation)
        {
            if (!_objPools.ContainsKey(tag) || _objPools[tag].Count <= 0) return null;
            
            GameObject obj2Spawn = _objPools[tag].Dequeue();
            obj2Spawn.SetActive(true);
            obj2Spawn.transform.position = position;
            obj2Spawn.transform.rotation = rotation;

            return obj2Spawn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="gameObject"></param>
        public void Terminate(string tag, GameObject gameObject)
        {
            gameObject.SetActive(false);
            _objPools[tag].Enqueue(gameObject);
        }
        #endregion
    }

    [System.Serializable]
    public class Pool
    {
        #region Serialize Field
        [Space, Header("Pool")]
        [SerializeField]
        private string tag = null;
        [SerializeField]
        private List<GameObject> prefabs = null;
        [SerializeField]
        private int size = 0;
        #endregion

        #region Properties
        public string Tag { get => tag; set => tag = value; }
        public List<GameObject> Prefabs { get => prefabs; set => prefabs = value; }
        public int Size { get => size; set => size = value; }
        #endregion
    }
}