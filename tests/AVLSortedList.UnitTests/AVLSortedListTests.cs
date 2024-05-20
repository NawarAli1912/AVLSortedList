namespace AVLSortedList.UnitTests;

public class AVLSortedListTests
{
    private readonly AVLSortedList<int> _sut;

    public AVLSortedListTests()
    {
        _sut = [];
    }

    [Fact]
    public void Add_SingleElement_ElementAdded()
    {

        // Act
        _sut.Add(10);

        // Assert
        Assert.Equal(1, _sut.Count);
        Assert.Equal(10, _sut[0]);
    }

    [Fact]
    public void Add_MultipleElements_ElementsAddedInOrder()
    {
        // Arrange

        _sut.Add(5);
        _sut.Add(5);
        _sut.Add(20);
        _sut.Add(10);

        Assert.Equal(4, _sut.Count);
        Assert.Equal(5, _sut[0]);
        Assert.Equal(5, _sut[1]);
        Assert.Equal(10, _sut[2]);
        Assert.Equal(20, _sut[3]);
    }

    [Fact]
    public void Remove_ExistingElement_ElementRemoved()
    {
        _sut.Add(10);
        _sut.Add(5);
        _sut.Add(20);

        bool removed = _sut.Remove(10);

        Assert.True(removed);
        Assert.Equal(2, _sut.Count);
        Assert.Equal(5, _sut[0]);
        Assert.Equal(20, _sut[1]);
    }

    [Fact]
    public void Remove_NonExistingElement_NoEffect()
    {
        _sut.Add(10);
        _sut.Add(5);
        _sut.Add(20);

        bool removed = _sut.Remove(15);

        Assert.False(removed);
        Assert.Equal(3, _sut.Count);
    }

    [Fact]
    public void IndexOf_ExistingElement_ReturnsIndex()
    {
        _sut.Add(10);
        _sut.Add(5);
        _sut.Add(20);

        int index = _sut.IndexOf(10);

        Assert.Equal(1, index);
    }

    [Fact]
    public void IndexOf_NonExistingElement_ReturnsNegativeOne()
    {
        _sut.Add(10);
        _sut.Add(5);
        _sut.Add(20);

        int index = _sut.IndexOf(15);

        Assert.Equal(-1, index);
    }


    [Fact]
    public void Indexer_InvalidIndex_ThrowsException()
    {
        Assert.Throws<IndexOutOfRangeException>(() => _sut[0]);
    }

    [Fact]
    public void ComplexOperations_MaintainsCorrectOrder()
    {
        _sut.Add(10);
        _sut.Add(5);
        _sut.Add(15);
        _sut.Add(20);
        _sut.Add(25);
        _sut.Remove(15);
        _sut.Add(17);

        int[] expected = { 5, 10, 17, 20, 25 };

        Assert.Equal(expected.Length, _sut.Count);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], _sut[i]);
        }
    }

    [Fact]
    public void IndexOf_MultipleOccurrences_ReturnsFirstIndex()
    {
        _sut.Add(10);
        _sut.Add(5);
        _sut.Add(20);
        _sut.Add(10);
        _sut.Add(10);

        int index = _sut.IndexOf(10);

        Assert.Equal(1, index); // Index of the first occurrence of 10
    }

    [Fact]
    public void AddRemove_MixOperations_MaintainsOrderAndCount()
    {
        _sut.Add(10);
        _sut.Add(20);
        _sut.Add(5);
        _sut.Add(15);
        _sut.Remove(20);
        _sut.Add(25);
        _sut.Remove(10);
        _sut.Add(30);

        int[] expected = { 5, 15, 25, 30 };

        Assert.Equal(expected.Length, _sut.Count);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], _sut[i]);
        }
    }

    [Fact]
    public void Add_LargeNumberOfElements_MaintainsOrder()
    {
        var random = new Random();

        for (int i = 0; i < 10000; i++)
        {
            _sut.Add(random.Next(0, 10000));
        }

        int prev = int.MinValue;
        foreach (var value in _sut)
        {
            Assert.True(value >= prev);
            prev = value;
        }
    }

    [Fact]
    public void Remove_LargeNumberOfElements_MaintainsOrderAndCount()
    {
        var random = new Random();
        var elements = new List<int>();

        for (int i = 0; i < 1000; i++)
        {
            int value = random.Next(0, 10000);
            _sut.Add(value);
            elements.Add(value);
        }

        foreach (var value in elements)
        {
            _sut.Remove(value);
        }

        Assert.Equal(0, _sut.Count);
    }

    [Fact]
    public void Enumerator_IteratesInOrder()
    {
        _sut.Add(10);
        _sut.Add(5);
        _sut.Add(20);

        int[] expected = { 5, 10, 20 };
        int index = 0;

        foreach (var element in _sut)
        {
            Assert.Equal(expected[index++], element);
        }
    }

    [Fact]
    public void Add_LargeNumberOfDuplicates_MaintainsOrderAndCount()
    {
        for (int i = 0; i < 1000; i++)
        {
            _sut.Add(42);
        }

        Assert.Equal(1000, _sut.Count);
        for (int i = 0; i < 1000; i++)
        {
            Assert.Equal(42, _sut[i]);
        }
    }

    [Fact]
    public void EdgeCase_EmptyListOperations()
    {
        Assert.Throws<IndexOutOfRangeException>(() => _sut[0]);
        Assert.False(_sut.Remove(10));
        Assert.Equal(-1, _sut.IndexOf(10));
        Assert.Empty(_sut);
    }

    [Fact]
    public void Add_ElementsInDescendingOrder_MaintainsSortedOrder()
    {
        for (int i = 10; i >= 1; i--)
        {
            _sut.Add(i);
        }

        for (int i = 0; i < 10; i++)
        {
            Assert.Equal(i + 1, _sut[i]);
        }
    }

    [Fact]
    public void Add_ElementsInAscendingOrder_MaintainsSortedOrder()
    {
        for (int i = 1; i <= 10; i++)
        {
            _sut.Add(i);
        }

        for (int i = 0; i < 10; i++)
        {
            Assert.Equal(i + 1, _sut[i]);
        }
    }

    [Fact]
    public void ComplexMix_AdditionsRemovals_MaintainsOrder()
    {
        var random = new Random();

        for (int i = 0; i < 1000; i++)
        {
            _sut.Add(random.Next(0, 10000));
        }

        for (int i = 0; i < 500; i++)
        {
            _sut.Remove(random.Next(0, 10000));
        }

        int prev = int.MinValue;
        foreach (var value in _sut)
        {
            Assert.True(value >= prev);
            prev = value;
        }
    }

    [Fact]
    public void IndexOf_DuplicateValues_ComplexScenario()
    {
        _sut.Add(10);
        _sut.Add(5);
        _sut.Add(10);
        _sut.Add(20);
        _sut.Add(10);
        _sut.Add(5);
        _sut.Add(15);

        int[] expectedInitial = [5, 5, 10, 10, 10, 15, 20];
        Assert.Equal(expectedInitial.Length, _sut.Count);
        for (int i = 0; i < expectedInitial.Length; i++)
        {
            Assert.Equal(expectedInitial[i], _sut[i]);
        }

        Assert.Equal(0, _sut.IndexOf(5));
        Assert.Equal(2, _sut.IndexOf(10));


        _sut.Remove(5);
        _sut.Remove(10);


        int[] expectedAfterFirstRemoval = { 5, 10, 10, 15, 20 };
        Assert.Equal(expectedAfterFirstRemoval.Length, _sut.Count);
        for (int i = 0; i < expectedAfterFirstRemoval.Length; i++)
        {
            Assert.Equal(expectedAfterFirstRemoval[i], _sut[i]);
        }

        Assert.Equal(0, _sut.IndexOf(5));
        Assert.Equal(1, _sut.IndexOf(10));

        _sut.Remove(10);

        int[] expectedAfterSecondRemoval = [5, 10, 15, 20];
        Assert.Equal(expectedAfterSecondRemoval.Length, _sut.Count);
        for (int i = 0; i < expectedAfterSecondRemoval.Length; i++)
        {
            Assert.Equal(expectedAfterSecondRemoval[i], _sut[i]);
        }

        Assert.Equal(0, _sut.IndexOf(5));
        Assert.Equal(1, _sut.IndexOf(10));
    }

    [Fact]
    public void RealWorldUsageScenario()
    {
        var random = new Random();
        var elements = new List<int>();

        for (int i = 0; i < 100; i++)
        {
            int value = random.Next(0, 200);
            _sut.Add(value);
            elements.Add(value);
        }

        int prev = int.MinValue;
        foreach (var value in _sut)
        {
            Assert.True(value >= prev, $"Value {value} is not greater than or equal to {prev}");
            prev = value;
        }

        var elementCounts = new Dictionary<int, int>();
        foreach (var value in elements)
        {
            if (!elementCounts.ContainsKey(value))
            {
                elementCounts[value] = 0;
            }
            elementCounts[value]++;
        }

        for (int i = 0; i < 50; i++)
        {
            int indexToRemove = random.Next(0, elements.Count);
            int valueToRemove = elements[indexToRemove];
            bool removed = _sut.Remove(valueToRemove);
            if (removed)
            {
                elementCounts[valueToRemove]--;
                elements.RemoveAt(indexToRemove);

                if (elementCounts[valueToRemove] == 0)
                {
                    Assert.Equal(-1, _sut.IndexOf(valueToRemove));
                }
            }
        }

        prev = int.MinValue;
        foreach (var value in _sut)
        {
            Assert.True(value >= prev, $"Value {value} is not greater than or equal to {prev}");
            prev = value;
        }

        Assert.Equal(elements.Count, _sut.Count);

        elements.Sort();
        for (int i = 0; i < elements.Count; i++)
        {
            Assert.Equal(elements[i], _sut[i]);
        }
    }
}
