using UnityEngine;

namespace View
{
    public class FallCoinController : SingletonMonoBehaviour<FallCoinController>
    {
        [SerializeField] Transform fallPositionMaxX;
        [SerializeField] Transform fallPositionMinX;
        [SerializeField] Transform fallPositionYZ;

        [SerializeField] GameObject coinPrefab;
        
        public void FallCoin(Vector3 clickPosition)
        {
            var x = Mathf.Clamp(clickPosition.x, fallPositionMinX.position.x, fallPositionMaxX.position.x);
            var position = fallPositionYZ.position;
            var y = position.y;
            var z = position.z;
            
            Debug.Log(new Vector3(x, y, z));
            Instantiate(coinPrefab, new Vector3(x, y, z), Random.rotation);
        }
    }
}