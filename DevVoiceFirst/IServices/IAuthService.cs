using DevVoiceFirst.DtoModel;

namespace DevVoiceFirst.IServices
{
    public interface IAuthService
    {
        Task<(Dictionary<string, object>, string, int)> AuthLogin(AuthDtoModel authDtoModel);
        Task<(Dictionary<string, object>, string, int)> ForgotPassword(string userName);
        string GetCurrentUserId();
        Task<(Dictionary<string, object>, string, int)> VerificationOTP(VerificationOtpDtoModel verificationOtpDtoModel);
        Task<(Dictionary<string, object>, string, int)> ResetPassword(ResetPasswordDtoModel resetPasswordDtoModel);
        Task<(Dictionary<string, object>, string, int)> ChangePassword(ChangePasswordDtoModel changePasswordDtoModel);
        Task<(Dictionary<string, object>, string, int)> ForgotPasswordLink(string userName);
    }
}
