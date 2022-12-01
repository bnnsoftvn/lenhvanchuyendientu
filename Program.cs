
using Bnnsoft.Sdk.Signers;
using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;

namespace Bnnsoft.Sdk
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            byte[] bDataFile = File.ReadAllBytes("sample.xml");
            bool valid = false;
            try
            {
                string key = "4288161d272f4622b23e52697346ad8d";
                string secret = "eHi3x+c4nQZYuVQRZV3d1WcB46aCzMQhNR4KcSpY";
                string server = "hn1";
                var signer = new XmlSigner(key,secret, server);
                var xpathdata = new string[] { "", "" };
                var signed = signer.SignLvcdt(bDataFile);
                File.WriteAllBytes("signed.xml", signed);
                if (signed == null)
                {
                    return;
                }
                
                File.WriteAllBytes(key + ".signed.xml", signed);
            }
            catch (Exception ex)
            {
                ex.GetHashCode();
            }
            finally
            {

            }
        }       
    }
}
