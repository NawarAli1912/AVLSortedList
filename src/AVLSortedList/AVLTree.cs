namespace AVLSortedList;

using System;


public class AVLTree<T> where T : IComparable<T>
{
    private AVLTreeNode<T> root;

    public int Count => root?.Count ?? 0;

    public void Insert(T value)
    {
        root = Insert(root, value);
    }

    public bool Remove(T value)
    {
        int initialCount = Count;
        root = Remove(root, value);
        return Count < initialCount;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
            return FindByIndex(root, index);
        }
    }

    public int IndexOf(T value)
    {
        return IndexOf(root, value, 0, true);
    }

    public IEnumerable<T> InOrderTraversal()
    {
        return InOrderTraversal(root);
    }

    private IEnumerable<T> InOrderTraversal(AVLTreeNode<T> node)
    {
        if (node != null)
        {
            foreach (var value in InOrderTraversal(node.Left))
            {
                yield return value;
            }
            yield return node.Value;
            foreach (var value in InOrderTraversal(node.Right))
            {
                yield return value;
            }
        }
    }

    private AVLTreeNode<T> Insert(AVLTreeNode<T> node, T value)
    {
        if (node == null)
            return new AVLTreeNode<T>(value);

        int compare = value.CompareTo(node.Value);
        if (compare <= 0)
            node.Left = Insert(node.Left, value);
        else
            node.Right = Insert(node.Right, value);

        Update(node);
        return Balance(node);
    }

    private AVLTreeNode<T> Remove(AVLTreeNode<T> node, T value)
    {
        if (node == null)
            return null;

        int compare = value.CompareTo(node.Value);
        if (compare < 0)
            node.Left = Remove(node.Left, value);
        else if (compare > 0)
            node.Right = Remove(node.Right, value);
        else
        {
            if (node.Left == null)
                return node.Right;
            if (node.Right == null)
                return node.Left;

            AVLTreeNode<T> minLargerNode = GetMin(node.Right);
            node.Value = minLargerNode.Value;
            node.Right = Remove(node.Right, minLargerNode.Value);
        }

        Update(node);
        return Balance(node);
    }

    private AVLTreeNode<T> GetMin(AVLTreeNode<T> node)
    {
        while (node.Left != null)
            node = node.Left;
        return node;
    }

    private void Update(AVLTreeNode<T> node)
    {
        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);
    }

    private AVLTreeNode<T> Balance(AVLTreeNode<T> node)
    {
        int balanceFactor = BalanceFactor(node);
        if (balanceFactor > 1)
        {
            if (BalanceFactor(node.Left) < 0)
                node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }
        if (balanceFactor < -1)
        {
            if (BalanceFactor(node.Right) > 0)
                node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }
        return node;
    }

    private int BalanceFactor(AVLTreeNode<T> node)
    {
        return node == null ? 0 : Height(node.Left) - Height(node.Right);
    }

    private AVLTreeNode<T> RotateRight(AVLTreeNode<T> node)
    {
        AVLTreeNode<T> newRoot = node.Left;
        node.Left = newRoot.Right;
        newRoot.Right = node;
        Update(node);
        Update(newRoot);
        return newRoot;
    }

    private AVLTreeNode<T> RotateLeft(AVLTreeNode<T> node)
    {
        AVLTreeNode<T> newRoot = node.Right;
        node.Right = newRoot.Left;
        newRoot.Left = node;
        Update(node);
        Update(newRoot);
        return newRoot;
    }

    private int Height(AVLTreeNode<T> node)
    {
        return node?.Height ?? 0;
    }

    private int GetCount(AVLTreeNode<T> node)
    {
        return node?.Count ?? 0;
    }

    private T FindByIndex(AVLTreeNode<T> node, int index)
    {
        int leftCount = GetCount(node.Left);
        if (index < leftCount)
            return FindByIndex(node.Left, index);
        if (index == leftCount)
            return node.Value;
        return FindByIndex(node.Right, index - leftCount - 1);
    }

    private int IndexOf(AVLTreeNode<T> node, T value, int currentIndex, bool findFirst)
    {
        if (node == null)
            return -1;

        int compare = value.CompareTo(node.Value);

        if (compare < 0)
        {
            return IndexOf(node.Left, value, currentIndex, findFirst);
        }
        else if (compare == 0)
        {
            int leftIndex = IndexOf(node.Left, value, currentIndex, findFirst);

            if (findFirst && leftIndex != -1)
            {
                return leftIndex;
            }

            return currentIndex + GetCount(node.Left);
        }
        else
        {
            return IndexOf(node.Right, value, currentIndex + GetCount(node.Left) + 1, findFirst);
        }
    }
}