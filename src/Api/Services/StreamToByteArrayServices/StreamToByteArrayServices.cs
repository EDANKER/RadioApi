namespace Api.Services.StreamToByteArrayServices;

public interface IStreamToByteArrayServices
{
    Task<byte[]?> StreamToByte(Stream stream);
}

public class StreamToByteArrayServices : IStreamToByteArrayServices
{
    public async Task<byte[]?> StreamToByte(Stream stream)
    {
        try
        {
            byte[] buffer = new byte[stream.Length];
            int readAsync = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (readAsync > 0)
                return buffer;
            
            return null;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}