namespace AggregatorMicroService.Dto.Photos;

public class PhotoCreatedDto
{
    public string FileName { get; set; }
    public byte[] Value { get; set; }
}