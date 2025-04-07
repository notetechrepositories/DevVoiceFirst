namespace DevVoiceFirst.DtoModel
{
    public class AuthDtoModel
    {
        public string Username { get; }
        public string Password { get; }

        public AuthDtoModel(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("User name is required.", nameof(username));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required.", nameof(password));

            Username = username;
            Password = password;
        }
    }

    public class VerificationOtpDtoModel
    {
        public string Otp { get; }
        public string EncryptedData { get; }

        public VerificationOtpDtoModel(string otp, string encryptedData)
        {
            if (string.IsNullOrWhiteSpace(otp))
                throw new ArgumentException("OTP is required.", nameof(otp));

            if (otp.Length != 6)
                throw new ArgumentException("OTP must be exactly 6 characters long.", nameof(otp));

            if (string.IsNullOrWhiteSpace(encryptedData))
                throw new ArgumentException("Encrypted OTP is required.", nameof(encryptedData));

            Otp = otp;
            EncryptedData = encryptedData;
        }
    }

    public class ResetPasswordDtoModel
    {
        public string UserId { get; }
        public string Password { get; }

        public ResetPasswordDtoModel(string userId, string password)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User id is required.", nameof(userId));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required.", nameof(password));

            if (password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters long.", nameof(password));

            UserId = userId;
            Password = password;
        }
    }

    public class ChangePasswordDtoModel
    {
        public string CurrentPassword { get; }
        public string Password { get; }

        public ChangePasswordDtoModel(string currentPassword, string password)
        {
            if (string.IsNullOrWhiteSpace(currentPassword))
                throw new ArgumentException("Current password is required.", nameof(currentPassword));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("New password is required.", nameof(password));

            if (password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters long.", nameof(password));

            CurrentPassword = currentPassword;
            Password = password;
        }
    }
}
