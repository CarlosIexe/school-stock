namespace SchoolStock.Data.Converters.Contract
{
    public interface IObjectConverter<Origin, Destination>
    {
        Destination Parse(Origin origin);

        List<Destination> Parse(List<Origin> origin);
    }
}
