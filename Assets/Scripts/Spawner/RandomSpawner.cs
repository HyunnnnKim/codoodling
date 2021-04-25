using UnityEngine;

namespace Collie
{
    public class RandomSpawner : MonoBehaviour
    {
        #region Serialize Field
        [Space, Header("Object to spawn")]
        [SerializeField]
        private string spawnObjectTag = null;
        [SerializeField]
        private int spawnLimit = 10;
        [SerializeField, Range(1, 10)]
        private float delayTime = 3f;
        #endregion

        #region Private Field
        private Vector3 _randomPoint = Vector3.zero;
        private float _elapsedTime = Mathf.Infinity;
        private float _waitTime = 0;
        private int _numberOfSpawnedObjects = 0;
        private Color _gizmosColor = new Color(0.3f, 0f, 0f, 0.6f);
        #endregion

        private void Update()
        {
            if (_numberOfSpawnedObjects < spawnLimit)
            {
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime < _waitTime) return;

                CalculateRandomPoint(ref _randomPoint);
                PointSpawn(spawnObjectTag, _randomPoint);

                _waitTime = Mathf.Clamp(Random.Range(delayTime - 1, delayTime + 1), 1, 10);
                _elapsedTime = 0f;
            }
        }

        #region Spawn
        private void CalculateRandomPoint(ref Vector3 randomPoint)
        {
            var origin = transform.position;
            var range = transform.localScale / 2.0f;
            var randomRange = new Vector3(
                Random.Range(-range.x, range.x),
                Random.Range(-range.y, range.y),
                Random.Range(-range.z, range.z));
            randomPoint = origin + randomRange;
        }

        private void PointSpawn(string tag, Vector3 point)
        {
            ObjectPool.Instance.Spawn(tag, point, Quaternion.identity);
        }
        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawCube(transform.position, transform.localScale);
        }
    }
}