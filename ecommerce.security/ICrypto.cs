using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.security
{
    public interface ICrypto : IDisposable
    {
        string Encrypt(string TextToEncrypt);

        string Decrypt(string TextToDecrypt);

        string EncryptPassword(string TextToEncrypt);

        string DecryptPassword(string TextToEncrypt);
    }
}
