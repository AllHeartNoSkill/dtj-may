using System;
using Godot;

public static class NodeExtension
{
    public static bool SearchParent<T>(this Node self, out T result, int depth = 5) where T : class
    {
        Node parent = self.GetParent();
        Node root = self.GetTree().Root;
         
        for (int i = 0; i < depth - 1; i++)
        {
            if (parent is T parent1)
            {
                result = parent1;
                return true;
                 
            }
            if (parent == root)
            {
                break;
            }

            parent = parent.GetParent();
        }

        result = null;
        return false;
    }
}