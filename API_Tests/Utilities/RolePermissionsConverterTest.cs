using API.Authentication;
using API.Entities;
using API.Utilities;
using JetBrains.Annotations;

namespace API_Tests.Utilities;

[TestClass]
[TestSubject(typeof(RolePermissionsConverter))]
public class RolePermissionsConverterTest
{

    [TestMethod]
    public void ConvertToPermissionsValue_ShouldReturnMaxValue()
    {
        // Arrange
        var roles = Enum.GetValues<UserPermissions>()
            .Select(role => new AppRole(role.ToString(), (ulong)role))
            .AsEnumerable();

        var expect = (ulong)(0x01 | 0x02 | 0x04 | 0x08);
        
        // Act
        var result = RolePermissionsConverter.ConvertToPermissionsValue(roles);
        
        // Assert
        Assert.AreEqual(expect, result);
    }
    
    [TestMethod]
    public void ConvertToPermissionsValue_ShouldReturnNine()
    {
        // Arrange
        var roles = new List<AppRole>()
        {
            new AppRole(nameof(UserPermissions.ManageStockItems), (ulong)UserPermissions.ManageStockItems),
            new AppRole(nameof(UserPermissions.ManageUsers), (ulong)UserPermissions.ManageUsers)
        };

        var expect = (ulong)(UserPermissions.ManageStockItems | UserPermissions.ManageUsers);
        
        // Act
        var result = RolePermissionsConverter.ConvertToPermissionsValue(roles);
        
        // Assert
        Assert.AreEqual(expect, result);
    }
    
    [TestMethod]
    public void ConvertToPermissionsValue_ShouldReturnZero()
    {
        // Arrange
        var roles = new List<AppRole>();

        ulong expect = 0;
        
        // Act
        var result = RolePermissionsConverter.ConvertToPermissionsValue(roles);
        
        // Assert
        Assert.AreEqual(expect, result);
    }

    [TestMethod]
    public void ConvertToRoles_ShouldReturnCorrectList()
    {
        // Arrange
        var permissions = (ulong)(UserPermissions.ManageUsers | UserPermissions.ManageStockItems);

        var expect = new List<UserPermissions>()
        {
            UserPermissions.ManageUsers,
            UserPermissions.ManageStockItems
        };
        
        // Act
        var result = RolePermissionsConverter
            .ConvertToRoles(permissions)
            .ToList();
        
        // Assert
        CollectionAssert.AreEqual(expect, result);
    }

    [TestMethod]
    public void ConvertToRoles_ShouldReturnEmptyList()
    {
        // Arrange
        ulong permissions = 0;

        var expect = new List<UserPermissions>();
        
        // Act
        var result = RolePermissionsConverter
            .ConvertToRoles(permissions)
            .ToList();
        
        // Assert
        CollectionAssert.AreEqual(expect, result);
    }

    [TestMethod]
    public void ConvertToRoles_ShouldReturnAllPermissions()
    {
        // Arrange
        ulong permissions = ulong.MaxValue;

        var expect = Enum.GetValues<UserPermissions>().ToList();
        
        // Act
        var result = RolePermissionsConverter
            .ConvertToRoles(permissions)
            .ToList();
        
        // Assert
        CollectionAssert.AreEqual(expect, result);
    }
}