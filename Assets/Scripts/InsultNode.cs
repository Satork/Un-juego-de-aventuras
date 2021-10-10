using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace InsultNode {

	[Serializable]
	public class Actors {
		public int id;
		public string name;
	}
	
	[Serializable]
	public class Insult {
		public int id;
		public String dialogue;
	}
	
	[Serializable]
	public class Conversations {
		public int id;
		public int actorId;
		public Insult[] insults;
	}

	[Serializable]
	public class DialoguesData {
		public Actors[] actors;
		public Conversations[] conversations;
	}
}