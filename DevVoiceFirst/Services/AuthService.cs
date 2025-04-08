using DevVoiceFirst.DtoModel;
using DevVoiceFirst.IRepository;
using DevVoiceFirst.IServices;
using DevVoiceFirst.Model;
using DevVoiceFirst.Repository;
using DevVoiceFirst.Utilities;
using DevVoiceFirst.Utilits;

namespace DevVoiceFirst.Services
{
    public class AuthService: IAuthService
    {
        public readonly IUserRepo _UserRepo;
        public readonly IRoleRepo _RoleRepo;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IConfiguration _Configuration;
        public AuthService(
            IUserRepo UserRepo,
            IRoleRepo RoleRepo,
            IConfiguration Configuration,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _UserRepo = UserRepo;
            _RoleRepo = RoleRepo;
            _Configuration = Configuration;
            _HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public string GetCurrentUserId()
        {
            if (_HttpContextAccessor.HttpContext?.User == null || !_HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var userIdClaim = _HttpContextAccessor.HttpContext.User.FindFirst("user_id");
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User ID not found in the token.");
            }

            var decryptedUserId = SecurityUtilities.Decryption(userIdClaim.Value);
            if (decryptedUserId == null)
            {
                throw new UnauthorizedAccessException("User ID decryption failed.");
            }

            return decryptedUserId;
        }
        public async Task<(Dictionary<string, object>, string, int)> AuthLogin(AuthDtoModel authDtoModel)
        {
            var data = new Dictionary<string, object>();
            var userDetails = await _UserRepo.GetUserDetailsByEmailOrPhone(authDtoModel.Username);
            if (userDetails != null && userDetails.t5_password != null && userDetails.t5_salt_key != null)
            {
                bool status = SecurityUtilities.VerifyPassword(authDtoModel.Password, userDetails.t5_password, userDetails.t5_salt_key);
                if (status)
                {
                    var encryptUserId = SecurityUtilities.Encryption(userDetails.id_t5_users);
                    var claimDetails = new Dictionary<string, string>();
                    claimDetails["user_id"] = encryptUserId;
                    var tokenUtilities = new SecurityUtilities(_Configuration);
                    var token = tokenUtilities.GetToken(claimDetails);

                    var users = new
                    {
                        t5_first_name = userDetails.t5_first_name,
                        t5_last_name = userDetails.t5_last_name,
                        t5_sex = userDetails.t5_sex,
                        t5_email = userDetails.t5_email,
                        t5_mobile_no = userDetails.t5_mobile_no,
                    };

                    data["token"] = token;
                    data["userDetails"] = users;

                    return (data, StatusUtilities.SUCCESS, StatusUtilities.SUCCESS_CODE);


                    //var filter = new Dictionary<string, string>
                    //{
                    //        { "id_t5_1_m_user_roles", userDetails.id_t5_1_m_user_roles }
                    //};
                    //var roleDeatils = _RoleRepo.GetAllSysRoleAsync(filter).Result.FirstOrDefault();


                    //if (roleDeatils != null)
                    //{
                    //    data["token"] = token;
                    //    data["userDetails"] = users;

                    //    return (data, StatusUtilities.SUCCESS, StatusUtilities.SUCCESS_CODE);

                    //}
                    //else
                    //{
                    //    return (data, StatusUtilities.FAILED, StatusUtilities.FAILED_CODE);
                    //}


                }
                else
                {
                    return (data, StatusUtilities.INVALID_PASSWORD, StatusUtilities.UNAUTHORIZED_ACCESS_CODE);
                }
            }
            else
            {
                return (data, StatusUtilities.USER_NOT_FOUND, StatusUtilities.NOT_FOUND_CODE);
            }


        }

        public async Task<(Dictionary<string, object>, string, int)> ChangePassword(ChangePasswordDtoModel changePasswordDtoModel)
        {
            var data = new Dictionary<string, object>();
            var userId = GetCurrentUserId();

            var userDeatils = _UserRepo.GetAllUserDetailsByUserId(userId).Result;
            if (userDeatils == null)
            {
                return (data, StatusUtilities.USER_NOT_FOUND, StatusUtilities.NOT_FOUND_CODE);
            }

            bool passwordStatus = SecurityUtilities.VerifyPassword(changePasswordDtoModel.Password, userDeatils.t5_password, userDeatils.t5_salt_key);
            if (!passwordStatus)
            {
                return (data, StatusUtilities.INVALID_PASSWORD, StatusUtilities.FAILED_CODE);
            }
            string salt = SecurityUtilities.GenerateSalt();
            string hashPassword = SecurityUtilities.HashPassword(changePasswordDtoModel.Password, salt);
            var parameters = new
            {
                Id = userId.Trim(),
                Password = hashPassword,
                saltKey = salt,
                UpdatedBy = userId,
                UpdatedDate = DateTime.UtcNow,
            };
            var status = await _UserRepo.UpdatePasswordAsync(parameters);

            if (status > 0)
            {

                return (data, StatusUtilities.SUCCESS, StatusUtilities.SUCCESS_CODE);
            }
            else
            {
                return (data, StatusUtilities.FAILED, StatusUtilities.FAILED_CODE);
            }
        }

        public async Task<(Dictionary<string, object>, string, int)> ForgotPassword(string userName)
        {
            var data = new Dictionary<string, object>();
            var userDetails = await _UserRepo.GetUserDetailsByEmailOrPhone(userName);
            if (userDetails != null)
            {
                var otp = StringUtilities.GenerateRandomOTP(6);
                var encrycryptedOtp = SecurityUtilities.Encryption(otp);
                var encrycryptedUserId = SecurityUtilities.Encryption(userDetails.id_t5_users);
                DateTime currentData = DateTime.Now;
                var encryptModel = SecurityUtilities.EncryptModel(encrycryptedOtp, encrycryptedUserId.ToString());
                var emailService = new CommunicationUtilities();

                EmailModel mail = new EmailModel();
                mail.from_email_password = "frsj ucpw vaww xzmv";
                mail.from_email = "anil.p@notetech.com";
                mail.to_email = userDetails.t5_email;
                mail.email_html_body =
                    "<html>" +
                        "<body>" +
                            "<p>Hi " + userDetails.t5_first_name + ",</p>" +
                            "<p>Your OTP is: <strong>" + otp + "</strong></p>" +
                            "<p>Please use this code to verify your account. This code will expire in 1 minutes.</p>" +
                            "<p><strong>Thanks & Regards,</strong><br>" +
                            "<em>Notetech Team</em></p>" +
                            "<p><em>Powered by Notetech Software</em></p>" +
                        "</body>" +
                    "</html>";
                mail.subject = "Your OTP Code to Verify Your Account";
                emailService.SendMail(mail);
                data["encryptedOtp"] = encryptModel;
                data["otpGeneratedTime"] = currentData;
                return (data, StatusUtilities.SUCCESS, StatusUtilities.SUCCESS_CODE);
            }
            else
            {
                return (data, StatusUtilities.USER_NOT_FOUND, StatusUtilities.NOT_FOUND_CODE);
            }

        }

        public async Task<(Dictionary<string, object>, string, int)> ResetPassword(ResetPasswordDtoModel resetPasswordDtoModel)
        {
            var data = new Dictionary<string, object>();
            var decryptedUserId = SecurityUtilities.Decryption(resetPasswordDtoModel.UserId);
            if (decryptedUserId == null)
            {
                return (data, StatusUtilities.UNAUTHORIZED_ACCESS, StatusUtilities.UNAUTHORIZED_ACCESS_CODE);
            }
            var filter = new Dictionary<string, string>
            {
                    { "id_t5_users", decryptedUserId }
            };
            var userDetails = _UserRepo.GetAllAsync(filter).Result.FirstOrDefault();
            if (userDetails == null)
            {
                return (data, StatusUtilities.USER_NOT_FOUND, StatusUtilities.NOT_FOUND_CODE);
            }
            string salt = SecurityUtilities.GenerateSalt();
            string hashPassword = SecurityUtilities.HashPassword(resetPasswordDtoModel.Password, salt);
            var parameters = new
            {
                Id = decryptedUserId.Trim(),
                Password = hashPassword,
                saltKey = salt,
                UpdatedBy = decryptedUserId,
                UpdatedDate = DateTime.UtcNow,
            };
            var status = await _UserRepo.UpdatePasswordAsync(parameters);

            if (status > 0)
            {

                return (data, StatusUtilities.SUCCESS, StatusUtilities.SUCCESS_CODE);
            }
            else
            {
                return (data, StatusUtilities.FAILED, StatusUtilities.FAILED_CODE);
            }
        }

        public async Task<(Dictionary<string, object>, string, int)> VerificationOTP(VerificationOtpDtoModel verificationOtpDtoModel)
        {
            var data = new Dictionary<string, object>();
            var decryptedData = SecurityUtilities.DecryptModel(verificationOtpDtoModel.EncryptedData);


            if (decryptedData == null)
            {
                return (data, StatusUtilities.UNAUTHORIZED_ACCESS, StatusUtilities.UNAUTHORIZED_ACCESS_CODE);
            }
            else
            {

                var currentDate = DateTime.Now;
                var diffTime = currentDate - decryptedData.CurrentDate;

                if (diffTime.TotalMinutes > 2)
                {

                    return (data, StatusUtilities.OTP_EXPIRED, StatusUtilities.FAILED_CODE);
                }

                var decryptedOtp = SecurityUtilities.Decryption(decryptedData.value);
                if (decryptedOtp == verificationOtpDtoModel.Otp)
                {
                    data["userData"] = decryptedData.userDeatils;
                    return (data, StatusUtilities.SUCCESS, StatusUtilities.SUCCESS_CODE);
                }
                else
                {
                    return (data, StatusUtilities.INVALID_OTP, StatusUtilities.INVALID_OTP_CODE);
                }
            }
        }

        public async Task<(Dictionary<string, object>, string, int)> ForgotPasswordLink(string userName)
        {

            var data = new Dictionary<string, object>();
            var userDetails = await _UserRepo.GetUserDetailsByEmailOrPhone(userName);
            if (userDetails != null)
            {

                string encryptedUserId = SecurityUtilities.Encryption(userDetails.id_t5_users);
                string encryptedDate = SecurityUtilities.Encryption(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"));
                string link = "http://localhost:4200/authentication/auth-test/" + encryptedUserId + "/" + encryptedDate;
                var emailService = new CommunicationUtilities();

                EmailModel mail = new EmailModel();
                mail.from_email_password = "frsj ucpw vaww xzmv";
                mail.from_email = "anil.p@notetech.com";
                mail.to_email = userDetails.t5_email;
                mail.email_html_body =
                    "<html>" +
                        "<body>" +
                            "<p>Hi " + userDetails.t5_first_name + ",</p>" +
                            "<p>You requested to reset your password. Please click the link below to reset your password:</p>" +
                            "<p><strong><a href='" + link + "' target='_blank'>" + link + "</a></strong></p>" +
                            "<p>This link will expire in 1 minute.</p>" +
                            "<p>If you did not request a password reset, please ignore this email.</p>" +
                            "<p><strong>Thanks & Regards,</strong><br>" +
                            "<em>Notetech Team</em></p>" +
                            "<p><em>Powered by Notetech Software</em></p>" +
                "</body>" +
                    "</html>";
                mail.subject = "Your OTP Code to Verify Your Account";
                emailService.SendMail(mail);
                return (data, StatusUtilities.SUCCESS, StatusUtilities.SUCCESS_CODE);
            }
            else
            {
                return (data, StatusUtilities.USER_NOT_FOUND, StatusUtilities.NOT_FOUND_CODE);
            }
        }
    }
}
