using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 불기둥
// 고정형 오브젝트
// 데미지 : -20
// 무한 모드
// 파괴 안됨
public class FirePillar : Obstacle
{
	private void Start()
	{
		damage = 10;
	}
}
