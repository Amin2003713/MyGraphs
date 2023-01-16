public class MyGraph
{
	private class Node
	{
		private string Lable;

		public Node(string lable) => Lable = lable;
		public override string ToString() => Lable;
	}

	private IDictionary<string, Node> Nodes = new Dictionary<string, Node>();
	private IDictionary<Node, List<Node>> NodesList = new Dictionary<Node, List<Node>>();


	public void AddNode(string label)
	{
		var node = new Node(label);
		if (!Nodes.ContainsKey(label)) Nodes.Add(label, node);
		if (!NodesList.ContainsKey(node)) NodesList.Add(node, new List<Node>());
	}
	public void AddEdgeDirectedGraph(string from, string to)
	{
		var fromNode = Nodes.FirstOrDefault(f => f.Key == from).Value;
		var toNode = Nodes.FirstOrDefault(t => t.Key == to).Value;
		if (fromNode == null) throw new InvalidOperationException();
		if (toNode == null) throw new InvalidOperationException();
		NodesList.FirstOrDefault(n => n.Key == fromNode).Value.Add(toNode);
	}
	public void AddEdgeUnDirectedGraph(string from, string to)
	{
		var fromNode = Nodes.FirstOrDefault(f => f.Key == from).Value;
		var toNode = Nodes.FirstOrDefault(t => t.Key == to).Value;
		if (fromNode == null) throw new InvalidOperationException();
		if (toNode == null) throw new InvalidOperationException();
		NodesList.FirstOrDefault(n => n.Key == fromNode).Value.Add(toNode);
		NodesList.FirstOrDefault(n => n.Key == toNode).Value.Add(fromNode);
	}
	public void RemoveNode(string label)
	{
		Nodes.Remove(label, out var node);
		NodesList.Remove(node);
		if (node == null) return;

		foreach (var key in NodesList.Keys)
		{
			NodesList.FirstOrDefault(a => a.Key == key).Value.Remove(node);
		}

	}
	public void RemoveEdge(string from, string to)
	{
		var fromNode = Nodes.FirstOrDefault(f => f.Key == from).Value;
		var toNode = Nodes.FirstOrDefault(t => t.Key == to).Value;
		if (fromNode == null) return;
		if (toNode == null) return;
		NodesList.FirstOrDefault(n => n.Key == fromNode).Value.Remove(toNode);
		NodesList.FirstOrDefault(a => a.Key == toNode).Value.Remove(fromNode);
	}
	public void TraversDeapthFirst(string root)
	{
		var node = Nodes.FirstOrDefault(n => n.Key == root).Value;
		if (node == null) return;
		TraversDeapthFirst(node, new HashSet<Node>());
	}
	public void TraversBreathFirst(string root)
	{
		var node = Nodes.FirstOrDefault(a => a.Key == root).Value;
		if (node == null) return;

		var visited = new HashSet<Node>();
		var queue = new Queue<Node>();
		queue.Enqueue(node);
		while (queue.Count != 0)
		{
			var current = queue.Dequeue();
			if (visited.Contains(current)) continue;
			Console.WriteLine(current.ToString());
			visited.Add(current);

			foreach (var neighbour in NodesList.FirstOrDefault(a => a.Key == current).Value)
			{
				if (!visited.Contains(neighbour))
					queue.Enqueue(neighbour);
			}
		}
	}
	public List<string> TopologicalSort()
	{
		var stack = new Stack<Node>();
		var visited = new HashSet<Node>();
		var sortrd = new List<string>();
		foreach (var nodesValue in Nodes.Values)
		{
			TopologicalSort(nodesValue, visited, stack);
			while (stack.Count != 0)
				sortrd.Add(stack.Pop().ToString());
		}
		return sortrd;
	}
	public bool HasCycle()
	{
		var all = new HashSet<Node>();

		foreach (var node in Nodes)
		{
			all.Add(node.Value);
		}

		var visiting = new HashSet<Node>();
		var visited = new HashSet<Node>();

		while (all.Count != 0)
		{
			var current = all.ToArray()[0];
			if (HasCycle(current, all, visiting, visited)) return true;
		}
		return false;
	}




	private Boolean HasCycle(Node node, ISet<Node> all, ISet<Node> visiting, ISet<Node> visited)
	{
		all.Remove(node);
		visiting.Add(node);

		foreach (var sub in NodesList.FirstOrDefault(a => a.Key == node).Value)
		{
			if (visited.Contains(sub)) continue;
			if (visiting.Contains(sub)) return true;
			if (HasCycle(sub, all, visiting, visited)) return true;
		}

		visiting.Remove(node);
		visited.Add(node);
		return false;
	}
	private void TopologicalSort(Node node, ISet<Node> visited, Stack<Node> stack)
	{
		if (visited.Contains(node)) return;
		visited.Add(node);
		foreach (var nodes in NodesList.FirstOrDefault(a => a.Key == node).Value)
			TopologicalSort(nodes, visited, stack);
		stack.Push(node);

	}
	private void TraversDeapthFirst(Node root, ISet<Node> visitedNodes)
	{
		Console.WriteLine(root.ToString());
		visitedNodes.Add(root);
		foreach (var node in NodesList.FirstOrDefault(n => n.Key == root).Value)
		{
			if (!visitedNodes.Contains(node))
				TraversDeapthFirst(node, visitedNodes);
		}
	}
	public void PrintGraf()
	{
		foreach (var NodeKye in NodesList.Keys)
		{
			var targets = NodesList.FirstOrDefault(t => t.Key == NodeKye).Value;
			if (targets.Count != 0)
			{
				Console.Write($"{NodeKye} is connected to ");
				foreach (var target in targets)
				{
					Console.Write($"{target.ToString()} ");
				}
				Console.WriteLine();
			}
			else
				Console.WriteLine($"The {NodeKye} is not connected to nay other node ");
		}
	}
}