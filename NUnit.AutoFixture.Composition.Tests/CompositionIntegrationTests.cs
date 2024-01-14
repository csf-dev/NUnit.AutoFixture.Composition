using AutoFixture.NUnit3;

namespace AutoFixture;

[TestFixture, Parallelizable]
public class CompositionIntegrationTests
{
    [Test,AutoData]
    public void SpecimenShouldBeCreatedWithAllThreeAttributes([SampleObjectConstruction, WithName("My name"), WithCity("My city")] SampleObject specimen)
    {
        Assert.Multiple(() =>
        {
            Assert.That(specimen.DateOfBirth, Is.EqualTo(new DateTime(2014, 3, 2)), "The DateOfBirth is specified in the SampleObjectConstructionCustomization");
            Assert.That(specimen.Name, Is.EqualTo("My name"), "The Name is specified in the WithNameAttribute");
            Assert.That(specimen.City, Is.EqualTo("My city"), "The City is specified in the WithCityAttribute");
        });
    }
}