using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scripts/StoryNode")]
public class StoryNode : ScriptableObject {
	[TextArea]
	public string Story;
	public string[] Answers;
	public bool IsFinal;
	public StoryNode[] NextNode;
	public Action OnNodeVisited;
}
