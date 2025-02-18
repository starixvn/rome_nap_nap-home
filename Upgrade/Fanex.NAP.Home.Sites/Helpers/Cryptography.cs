namespace Fanex.Common;

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class Cryptography
{
    private const string CfsKey = "&％#@?,:*!~^.";

    public static string CfsEnCrypt(string plainText)
    {
        var encryptedText = string.Empty;
        try
        {
            byte[] rgbIV = new byte[0];
            byte[] rgbKey = new byte[] { 0x21, 0x63, 0x54, 0x5a, 0x91, 0xac, 110, 0xcc };
            rgbIV = Encoding.UTF8.GetBytes(CfsKey);
            var provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write))
                {
                    stream2.Write(bytes, 0, bytes.Length);
                    stream2.FlushFinalBlock();
                    encryptedText = Convert.ToBase64String(stream.ToArray());
                }
            }
        }
        catch
        {
            throw;
        }

        return encryptedText;
    }
}
