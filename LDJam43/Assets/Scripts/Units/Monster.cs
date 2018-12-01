﻿using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Units {
	public class Monster : MonoBehaviour {

		[SerializeField] private GameObject _startPoint;
		[SerializeField] private GameObject _sleepPoint;
		[SerializeField] private GameObject _eatPoint;

		[SerializeField] private float _startDuration = 5;
		[SerializeField] private float _sleepDuration = 5;
		[SerializeField] private float _eatDuration = 5;

		private List<Vector3> _eatPoints;

		private float _timer; // TODO
		private bool monsterAwake;
		
		private void Start() {
			_eatPoints = new List<Vector3>{_eatPoint.transform.position, _sleepPoint.transform.position};
		}

		public void Spawn() {
			MonsterAwake();
			_timer = 10;
			monsterAwake = true;
		}

		private void Update() {
			if (monsterAwake) {
				_timer -= Time.deltaTime;
				if (!(_timer <= 0.0f)) {
					return;
				}
				monsterAwake = false;
				EatPlayer();
			}
		}

		public void MonsterAwake() {
			transform.DOMove(_startPoint.transform.position, _startDuration);
		}

		public void GoSleep() {
			transform.DOMove(_sleepPoint.transform.position, _sleepDuration);
		}

		public void EatPlayer() {
			transform.DOPath(_eatPoints.ToArray(), _eatDuration, PathType.CatmullRom);
		}
	}
}