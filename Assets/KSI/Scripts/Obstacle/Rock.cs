using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 바위
// 고정형 오브젝트(1단 점프)
// 생성 위치 : 발판
// 데미지 : -10
// 스토리 모드(서브, 보스, 광폭 보스), 무한 모드
// 파괴 안됨
public class Rock : Obstacle
{
	private void Start()
	{
		damage = 10;
	}
}
