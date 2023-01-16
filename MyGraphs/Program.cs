var g = new MyGraph();
g.AddNode("A");
g.AddNode("C");
g.AddNode("B");
g.AddNode("D");
//g.AddEdgeDirectedGraph("D", "A");
g.AddEdgeDirectedGraph("A", "B");
g.AddEdgeDirectedGraph("A", "C");
g.AddEdgeDirectedGraph("B", "C");
Console.WriteLine(g.HasCycle());





Console.WriteLine("Hello, World!");