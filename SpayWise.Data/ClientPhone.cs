namespace SpayWise.Data;

/// <summary>
/// client's phone digits with formatting removed, for searching
/// </summary>
public class ClientPhone
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string Digits { get; set; } = default!;

    public Client? Client { get; set; }
}
