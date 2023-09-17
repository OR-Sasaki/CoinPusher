using UnityEngine;

namespace View
{
    public class CoinController : MonoBehaviour
    {
        [SerializeField] float destroyDistance;
        const float CheckDestroyInterval = 5f;
        float elapsed;
        
        void Update()
        {
            elapsed += Time.deltaTime;
            if (!(elapsed >= CheckDestroyInterval)) return;
            
            elapsed = 0;
            if (Vector3.Distance(transform.position, Vector3.zero) >= destroyDistance)
            {
                Destroy(gameObject);
            }
        }
    }
}