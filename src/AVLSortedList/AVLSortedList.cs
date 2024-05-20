using System.Collections;

namespace AVLSortedList;

public class AVLSortedList<T> : IEnumerable<T> where T : IComparable<T>
{
    private AVLTree<T> avlTree = new AVLTree<T>();

    public int Count => avlTree.Count;

    public void Add(T value)
    {
        avlTree.Insert(value);
    }

    public bool Remove(T value)
    {
        return avlTree.Remove(value);
    }

    public int IndexOf(T value)
    {
        return avlTree.IndexOf(value);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return avlTree.InOrderTraversal().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public T this[int index]
    {
        get
        {
            return avlTree[index];
        }
    }
}
