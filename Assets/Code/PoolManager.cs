using System.Collections.Generic;
using UnityEngine;

namespace Goldmetal.UndeadSurvivor
{
    public class PoolManager : MonoBehaviour
    {
        public GameObject[] prefabs; // Các prefab đăng ký sẵn
        private Dictionary<GameObject, List<GameObject>> pools;
        public static PoolManager Instance;

        void Awake()
        {
            Instance = this;

            pools = new Dictionary<GameObject, List<GameObject>>();
            foreach (var prefab in prefabs)
            {
                pools[prefab] = new List<GameObject>();
            }
        }

        // Lấy object theo prefab trực tiếp
        public GameObject Get(GameObject prefab)
        {
            if (!pools.ContainsKey(prefab))
            {
                pools[prefab] = new List<GameObject>();
            }

            GameObject select = null;

            // Tìm object inactive trong pool
            foreach (var item in pools[prefab])
            {
                if (!item.activeSelf)
                {
                    select = item;
                    select.SetActive(true);
                    break;
                }
            }

            // Nếu không có, tạo mới
            if (!select)
            {
                select = Instantiate(prefab, transform);
                pools[prefab].Add(select);
            }

            return select;
        }

        public void Despawn(GameObject obj)
        {
            obj.SetActive(false); // Trả về pool
        }
    }
}
