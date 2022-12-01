namespace Bnnsoft.Sdk
{
    public interface IByteSigner
    {
        byte[] Sign(byte[] input);
    }
}