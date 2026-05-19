using System;
using System.Collections.Generic;

namespace TreeExample
{
    public class TreeNode
    {
        public string Value { get; }
        public List<TreeNode> Children { get; }

        public TreeNode(string value)
        {
            Value = value;
            Children = new List<TreeNode>();
        }

        public void AddChild(TreeNode child)
        {
            Children.Add(child);
        }

        
        public void PrintTree(string indent = "", bool isLast = true)
        {
            Console.Write(indent);

            if (indent != "")
            {
                Console.Write(isLast ? "└── " : "├── ");
            }

            Console.WriteLine(Value);

            indent += isLast ? "    " : "│   ";

            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].PrintTree(indent, i == Children.Count - 1);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode("Root");

            TreeNode child1 = new TreeNode("Child 1");
            TreeNode child2 = new TreeNode("Child 2");

            TreeNode child11 = new TreeNode("Child 1.1");
            TreeNode child12 = new TreeNode("Child 1.2");

            TreeNode child21 = new TreeNode("Child 2.1");

            root.AddChild(child1);
            root.AddChild(child2);

            child1.AddChild(child11);
            child1.AddChild(child12);

            child2.AddChild(child21);

            Console.WriteLine("Дерево:");
            root.PrintTree();
        }
    }
}