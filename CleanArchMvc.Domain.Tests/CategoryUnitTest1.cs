using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace CleanArchMvc.Domain.Tests;

public class CategoryUnitTest1
{
    [Fact(DisplayName = "Create Category With Valid State")]
    public void CreateCategory_WhitValidParam_ResultObjetcValidState()
    {
        Action action = () => new Category(1, "Category Name");
        action.Should()
            .NotThrow<DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create Category Nagative Id Value")]
    public void CreateCategory_NegativeIdValue_DomainExeceptionInvalidId()
    {
        Action action = () => new Category(-1, "Category Name");
        action.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid Id value");
    }

    [Fact(DisplayName = "Create Category Short Name Value")]
    public void CreateCategory_ShortNameValue_DomainExeceptionName()
    {
        Action action = () => new Category(1, "Ca");
        action.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid name, too short, min 3 char");
    }

    [Fact(DisplayName = "Create Category Missing Name Value")]
    public void CreateCategory_MissingNameValue_DomainExeceptionRequiredName()
    {
        Action action = () => new Category(1, "");
        action.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid name.Name is required");
    }
}
