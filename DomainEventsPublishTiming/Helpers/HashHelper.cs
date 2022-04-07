using System.Security.Cryptography;
using System.Text;

namespace DomainEventsPublishTiming.Helpers
{
  public static class HashHelper
  {
    public static string GetMd5Hash(string input)
    {
      using (MD5 md5Hash = MD5.Create())
      {
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder sb = new();
        for (int i = 0; i < data.Length; i++)
        {
          sb.Append(data[i].ToString("x2"));
        }
        return sb.ToString();
      }
    }
  }
}
