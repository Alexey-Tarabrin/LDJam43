﻿using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Units {
	public class Monster : MonoBehaviour {

		[SerializeField]
		GameObject _startPoint;
		[SerializeField]
		GameObject _sleepPoint;
		[SerializeField]
		GameObject _eatPoint;

		[SerializeField]
		float _startDuration = 5;
		[SerializeField]
		float _sleepDuration = 5;
		[SerializeField]
		float _eatDuration = 5;

		List<Vector3> _eatPoints;

		float _timer;
		bool monsterAwake;

		void Start() {
			_eatPoints = new List<Vector3>{_eatPoint.transform.position, _sleepPoint.transform.position};
			EventManager.OnMonsterAwake += MonsterAwake;
			DOTween.Init();
		}

		void Update() {
			if (monsterAwake) {
				_timer -= Time.deltaTime;
				if (!(_timer <= 0.0f)) {
					return;
				}
				monsterAwake = false;
				if (GameParameters.Instance.FuelInUsage > GameParameters.Instance.MinFuelToGetHit) {
					GoSleep();
				} else {
					EatPlayer();
				}
			}
		}

		public void MonsterAwake() {
			transform.DOMove(_startPoint.transform.position, _startDuration);
			_timer = 3;
			monsterAwake = true;
		}

		public void GoSleep() {
			transform.DOMove(_sleepPoint.transform.position, _sleepDuration);
			EventManager.MonsterSleep();
		}

		public void EatPlayer() {
			transform.DOPath(_eatPoints.ToArray(), _eatDuration, PathType.CatmullRom);
			EventManager.PlayerHit();
			EventManager.MonsterSleep();
		}
	}
}
