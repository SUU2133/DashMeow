using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 생선 가시
// 날아오는 오브젝트
// 데미지 : -10
// 스토리 모드(서브, 보스, 보스(광폭)), 무한 모드
// 파괴 안됨
public class FishThorn : Obstacle
{
    private void Start()
    {
		damage = player.FlyObstacleDamage(damage);
    }
}
