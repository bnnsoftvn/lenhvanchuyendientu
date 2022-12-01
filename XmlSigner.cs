using Bnnsoft.Sdk.Signers;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.IO;

namespace Bnnsoft.Sdk
{
    public class XmlSigner
    {
        private readonly string inputPDF = "";
        private readonly string outputPDF = "";
        private readonly string appid;
        private readonly string secret;
        private readonly string region;

        public XmlSigner(string appid, string secret, string region = "demo")
        {
            this.appid = appid;
            this.secret = secret;
            this.region = region;
        }


        public ApiResp Verify(byte[] input)
        {
            var signClient = new SignClient(appid, secret, "sign", region);
            string base64 = Convert.ToBase64String(input);
            var responseString = signClient.UploadString("/api/v2/xml/verify", "POST", base64);
            var verifyResult = JsonConvert.DeserializeObject<ApiResp>(responseString);
            if (verifyResult.status == 0)
            {
                return verifyResult;
            }

            return null;
        }


        public byte[] Sign(byte[] input)
        {
            var signClient = new SignClient(appid, secret, "sign", region);

            string base64xml = Convert.ToBase64String(input);

            var OriginalData = new
            {
                base64xml = base64xml,
                hashalg = "SHA1"
            };
            string StringJson = JsonConvert.SerializeObject(OriginalData);
            var responseString = signClient.UploadString("/api/v2/xml/sign/defaultdata", "POST", StringJson);
            var dataSigned = JsonConvert.DeserializeObject<ApiResp>(responseString);
            if (dataSigned.status == 0)
            {
                var bytesout = Convert.FromBase64String(dataSigned.obj.ToString());
                return bytesout;
            }

            return null;
        }

        public byte[] Sign78(byte[] input)
        {
            var signClient = new SignClient(appid, secret, "sign", region);

            string base64xml = Convert.ToBase64String(input);

            var OriginalData = new
            {
                base64xml = base64xml,
                datapath = "HDDT/ThongTinHoaDon",
                hashalg = "SHA1",
                signaturepath = "HDDT",
                signdate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
                standard = 78,
                typeID = 1
            };
            string StringJson = JsonConvert.SerializeObject(OriginalData);
            var responseString = signClient.UploadString("/api/v2/xml/sign/invoice/78", "POST", StringJson);
            var dataSigned = JsonConvert.DeserializeObject<ApiResp>(responseString);
            if (dataSigned.status == 0)
            {
                var bytesout = Convert.FromBase64String(dataSigned.obj.ToString());
                return bytesout;
            }

            return null;
        }

        internal byte[] SignLvcdt(byte[] bDataFile)
        {
            SignClient signClient = new SignClient(appid, secret, "sign", region);

            var OriginalData = new
            {
                base64xml = Convert.ToBase64String(bDataFile),
                datapath = new string[] { "LenhVanChuyen/DuLieu/ThongTinChung", "LenhVanChuyen/DuLieu/NoiDungLenhVanChuyen", "LenhVanChuyen/DuLieu/NoiDungBenDiXacNhan" },
                signaturepath = "LenhVanChuyen/DanhSachChuKySo/BenDi",
                hashalg = "SHA1",
                signatureid = "lenhvanchuyen1",
                signdate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
            };
            string StringJson = JsonConvert.SerializeObject(OriginalData);
            var responseString = signClient.UploadString("/api/v2/xml/refs", "POST", StringJson);
            var dataSigned = JsonConvert.DeserializeObject<ApiResp>(responseString);
            if (dataSigned.status == 0)
            {
                var bytesout = Convert.FromBase64String(dataSigned.obj.ToString());
                return bytesout;
            }

            return null;

        }
    }
}
