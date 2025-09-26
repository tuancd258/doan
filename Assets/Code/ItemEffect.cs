// File: ItemEffect.cs
using UnityEngine;

// Lớp "cha" cho tất cả các hiệu ứng đặc biệt
public abstract class ItemEffect : ScriptableObject
{
    // Mỗi hiệu ứng con sẽ phải định nghĩa logic của riêng nó cho các hàm này

    // Được gọi khi item được trang bị
    public abstract void OnEquip(GameObject player);

    // Được gọi khi item bị gỡ bỏ
    public abstract void OnUnEquip(GameObject player);

    // Được gọi khi người chơi tấn công (bạn sẽ cần tạo Event cho việc này)
    public virtual void OnAttack(GameObject target) { }

    // Được gọi khi người chơi hạ gục kẻ địch
    public virtual void OnKill(GameObject enemy) { }

    // Được gọi mỗi frame (nếu cần hiệu ứng theo thời gian)
    public virtual void OnUpdate() { }
}