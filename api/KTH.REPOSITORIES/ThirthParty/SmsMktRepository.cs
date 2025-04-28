using System;
using System.Text;
using KTH.MODELS.Custom.Request.MasterCountry;
using KTH.MODELS.ThirdParty.SMSMKT;
using KTH.REPOSITORIES.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface ISmsMktRepository
    {
        public Task<OtpSendReturn> OTPSend(string phone);

        public Task<OtpSendReturn> OTPSendInter(string phone);

        public Task<OtpValidateReturn> OTPValidate(OtpValidateRequest param);
    }

    public class SmsMktRepository : ISmsMktRepository
    {
        private readonly IConfiguration _configuration;

        private readonly string _urlOtpSend;
        private readonly string _urlOtpSendInter;
        private readonly string _urlOtpValidate;
        private readonly string _projectKey;
        private readonly string _apiKey;
        private readonly string _secretKey;


        public SmsMktRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            _urlOtpSend = _configuration["SMS:URL_OTP_SEND"];
            _urlOtpSendInter = _configuration["SMS:URL_OTP_SEND_INTER"];
            _urlOtpValidate = _configuration["SMS:URL_OTP_VALIDATE"];
            _projectKey = _configuration["SMS:PROJECT_KEY"];
            _apiKey = _configuration["SMS:API_KEY"];
            _secretKey = _configuration["SMS:SECRET_KEY"];

        }

        public async Task<OtpSendReturn> OTPSend(string phone)
        {
            OtpSendRequest smsMktRequest = new OtpSendRequest();

            OtpSendReturn smsMktReturn = new OtpSendReturn();

            try
            {
                var client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, _urlOtpSend);

                request.Headers.Add("api_key", _apiKey);

                request.Headers.Add("secret_key", _secretKey);

                smsMktRequest.project_key = _projectKey;

                smsMktRequest.phone = phone;

                smsMktRequest.ref_code = Helper.GenerateShortGuid();

                var jsonTxt = JsonConvert.SerializeObject(smsMktRequest);

                request.Content = new StringContent(jsonTxt, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    using (HttpContent content = response.Content)
                    {
                        var json = content.ReadAsStringAsync().Result;

                        smsMktReturn = JsonConvert.DeserializeObject<OtpSendReturn>(json);

                        smsMktReturn.result.phone = phone;
                    }
                }
            }
            catch
            {
                smsMktReturn.code = "000";
                smsMktReturn.detail = "Error";
            }

            return smsMktReturn;
        }

        public async Task<OtpSendReturn> OTPSendInter(string phone)
        {
            OtpSendRequest smsMktRequest = new OtpSendRequest();

            OtpSendReturn smsMktReturn = new OtpSendReturn();

            try
            {
                var client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, _urlOtpSendInter);

                request.Headers.Add("api_key", _apiKey);

                request.Headers.Add("secret_key", _secretKey);

                smsMktRequest.project_key = _projectKey;

                smsMktRequest.phone = phone;

                smsMktRequest.ref_code = Helper.GenerateShortGuid();

                var jsonTxt = JsonConvert.SerializeObject(smsMktRequest);

                request.Content = new StringContent(jsonTxt, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    using (HttpContent content = response.Content)
                    {
                        var json = content.ReadAsStringAsync().Result;

                        smsMktReturn = JsonConvert.DeserializeObject<OtpSendReturn>(json);

                        smsMktReturn.result.phone = phone;
                    }
                }
            }
            catch
            {
                smsMktReturn.code = "000";
                smsMktReturn.detail = "Error";
            }

            return smsMktReturn;
        }

        public async Task<OtpValidateReturn> OTPValidate(OtpValidateRequest param)
        {
            OtpValidateRequest smsMktRequest = new OtpValidateRequest();

            OtpValidateReturn smsMktReturn = new OtpValidateReturn();

            try
            {
                var client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, _urlOtpValidate);

                request.Headers.Add("api_key", _apiKey);

                request.Headers.Add("secret_key", _secretKey);

                smsMktRequest.token = param.token;

                smsMktRequest.otp_code = param.otp_code;

                smsMktRequest.ref_code = param.ref_code;

                var jsonTxt = JsonConvert.SerializeObject(smsMktRequest);

                request.Content = new StringContent(jsonTxt, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = client.SendAsync(request).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        var json = content.ReadAsStringAsync().Result;

                        smsMktReturn = JsonConvert.DeserializeObject<OtpValidateReturn>(json);
                    }
                }
            }
            catch
            {
                smsMktReturn.code = "000";
                smsMktReturn.detail = "Error";
                smsMktReturn.result.status = false;
            }

            return smsMktReturn;

        }
    }
}

