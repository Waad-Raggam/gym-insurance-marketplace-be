using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class PasswordComplexityAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var password = value as string;
        if (password == null) return false;


        if (password.Length < 8) return false;

        bool hasUpperCase = password.Any(char.IsUpper);
        bool hasLowerCase = password.Any(char.IsLower);
        bool hasDigit = password.Any(char.IsDigit);
        bool hasSpecialChar = Regex.IsMatch(password, @"[!@#$%^&*(),.?""\:{}|<>]");
        return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
    }

    public override string FormatErrorMessage(string name){
        return "Password must be at least 8 characters long and contains an uppercase character, a digit, and a special character";
    }
}