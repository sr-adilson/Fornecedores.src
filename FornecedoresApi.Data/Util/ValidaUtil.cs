using System.Text.RegularExpressions;

namespace FornecedoresApi.Domain.Util;

public static class ValidaUtil
{
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }

    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        string phonePattern = @"^\(?\d{2}\)?\s?\d{4,5}-\d{4}$";
        return Regex.IsMatch(phoneNumber, phonePattern);
    }

    public static bool IsValidAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            return false;

        string addressPattern = @"^[\w\s.,'-/]+$";
        return Regex.IsMatch(address, addressPattern);
    }
}
