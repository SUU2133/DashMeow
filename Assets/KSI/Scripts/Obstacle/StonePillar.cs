using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 돌기둥
// 고정형 오브젝트
// 데미지 : -10
// 스토리 모드(서브, 보스, 보스(광폭)), 무한 모드
// 파괴 안됨
public class StonePillar : Obstacle
{
	private void Start()
	{
		damage = 10;
	}
}
