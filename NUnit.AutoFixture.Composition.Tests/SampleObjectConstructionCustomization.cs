namespace AutoFixture;

public class SampleObjectConstructionCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<SampleObject>(c => c.FromFactory(() => new SampleObject { DateOfBirth = new(2014, 3, 2) }).OmitAutoProperties());
    }
}