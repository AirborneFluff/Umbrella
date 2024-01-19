using API.Extensions;
using JetBrains.Annotations;
using CollectionExtensions = API.Extensions.CollectionExtensions;

namespace API_Tests.Extensions;

[TestClass]
[TestSubject(typeof(CollectionExtensions))]
public class CollectionExtensionsTest
{
    [TestMethod]
    public void AddUnique_ShouldReturnUniqueList()
    {
        // Arrange
        var list = new List<string>()
        {
            "hello",
            "world",
            "it's",
            "me"
        };
        var clone = list.ToList();

        var itemToAdd = "hello";
        
        // Act
        list.AddUnique(itemToAdd);
        
        // Asset
        CollectionAssert.AreEqual(list, clone);
    }

    [TestMethod]
    public void AddUnique_ShouldReturnUniqueListWithExtraItem()
    {
        // Arrange
        var itemToAdd = "hello";
        var itemToAdd1 = "nice";
        
        var list = new List<string>()
        {
            "hello",
            "world",
            "it's",
            "me"
        };
        var clone = list.ToList();
        clone.Add(itemToAdd1);
        
        // Act
        list.AddUnique(itemToAdd);
        list.AddUnique(itemToAdd1);
        
        // Asset
        CollectionAssert.AreEqual(list, clone);
    }
}