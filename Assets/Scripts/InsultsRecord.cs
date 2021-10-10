using System.Collections;
using System.Collections.Generic;
using InsultNode;
using UnityEditor;
using UnityEngine;

public static class InsultsRecord {
	public static int Win = 0;
	public static int Lose = 0;
	public static int Rounds = 0;

	public static bool IsDeathAnimationEnded { get; set; }
	
	public const int RoundsMax = 3;

	public static bool IsWin() => Win > Lose;
	public static bool IsFinish() => Rounds == RoundsMax;

	public static void ResetStats() {
		Win = 0;
		Lose = 0;
		Rounds = 0;
	}
}
