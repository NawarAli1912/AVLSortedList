namespace AVLSortedList;
public class AVLTreeNode<T>
    where T : IComparable<T>
{
    public T Value { get; set; }

    public AVLTreeNode<T> Left { get; set; }

    public AVLTreeNode<T> Right { get; set; }

    public int Height { get; set; }

    public int Count { get; set; }

    public AVLTreeNode(T value)
    {
        Value = value;
        Height = 1;
        Count = 1;
    }
}
