using API.Extensions;
using JetBrains.Annotations;

namespace API_Tests.Extensions;

[TestClass]
[TestSubject(typeof(DictionaryExtensions))]
public class DictionaryExtensionsTest
{

    [TestMethod]
    public void AddWithQuantity_ShouldReturnWithNewItem()
    {
        // Arrange
        var dictionary = new Dictionary<string, int>()
        {
            { "Dairy", 1 },
            { "Electrical", 8 },
            { "Vegetables", 3 },
        };
        
        var expected = new Dictionary<string, int>()
        {
            { "Dairy", 1 },
            { "Electrical", 8 },
            { "Vegetables", 3 },
            { "Clothing", 1 },
        };

        var additionalItem = "Clothing";
        
        // Act
        dictionary.AddWithQuantity(additionalItem);
        
        // Assert
        Assert.IsTrue(Equals(dictionary, expected));
    }
    
    [TestMethod]
    public void AddWithQuantity_ShouldReturnWithNewValue()
    {
        // Arrange
        var dictionary = new Dictionary<string, int>()
        {
            { "Dairy", 1 },
            { "Electrical", 8 },
            { "Vegetables", 3 },
        };
        
        var expected = new Dictionary<string, int>()
        {
            { "Dairy", 2 },
            { "Electrical", 8 },
            { "Vegetables", 3 },
        };

        var additionalItem = "Dairy";
        
        // Act
        dictionary.AddWithQuantity(additionalItem);
        
        // Assert
        Assert.IsTrue(Equals(dictionary, expected));
    }
    
    [TestMethod]
    public void RemoveWithQuantity_ShouldReturnWithNewValue()
    {
        // Arrange
        var dictionary = new Dictionary<string, int>()
        {
            { "Dairy", 1 },
            { "Electrical", 8 },
            { "Vegetables", 3 },
        };
        
        var expected = new Dictionary<string, int>()
        {
            { "Dairy", 1 },
            { "Electrical", 7 },
            { "Vegetables", 3 },
        };

        var itemToRemove = "Electrical";
        
        // Act
        dictionary.RemoveWithQuantity(itemToRemove);
        
        // Assert
        Assert.IsTrue(Equals(dictionary, expected));
    }
    
    [TestMethod]
    public void RemoveWithQuantity_ShouldReturnWithNewItemRemoved()
    {
        // Arrange
        var dictionary = new Dictionary<string, int>()
        {
            { "Dairy", 1 },
            { "Electrical", 8 },
            { "Vegetables", 3 },
        };
        
        var expected = new Dictionary<string, int>()
        {
            { "Electrical", 8 },
            { "Vegetables", 3 },
        };

        var itemToRemove = "Dairy";
        
        // Act
        dictionary.RemoveWithQuantity(itemToRemove);
        
        // Assert
        Assert.IsTrue(Equals(dictionary, expected));
    }
    
    [TestMethod]
    public void RemoveWithQuantity_ShouldReturnUnchanged()
    {
        // Arrange
        var dictionary = new Dictionary<string, int>()
        {
            { "Dairy", 1 },
            { "Electrical", 8 },
            { "Vegetables", 3 },
        };
        
        var expected = new Dictionary<string, int>()
        {
            { "Dairy", 1 },
            { "Electrical", 8 },
            { "Vegetables", 3 },
        };

        var itemToRemove = "";
        
        // Act
        dictionary.RemoveWithQuantity(itemToRemove);
        
        // Assert
        Assert.IsTrue(Equals(dictionary, expected));
    }
    
    [TestMethod]
    public void RemoveWithQuantity_ShouldReturnUnchanged_1()
    {
        // Arrange
        var dictionary = new Dictionary<string, int>()
        {
            { "Dairy", 1 },
            { "Electrical", 8 },
            { "Vegetables", 3 },
        };
        
        var expected = new Dictionary<string, int>()
        {
            { "Dairy", 1 },
            { "Electrical", 8 },
            { "Vegetables", 3 },
        };

        var itemToRemove = "Random";
        
        // Act
        dictionary.RemoveWithQuantity(itemToRemove);
        
        // Assert
        Assert.IsTrue(Equals(dictionary, expected));
    }
    
    private bool Equals(IDictionary<string, int> x, IDictionary<string, int> y)
    {
        if (x.Count != y.Count)
            return false;

        HashSet<KeyValuePair<string, int>> set = new HashSet<KeyValuePair<string, int>>(x);
        set.SymmetricExceptWith(y);
        return set.Count == 0;
    }
}