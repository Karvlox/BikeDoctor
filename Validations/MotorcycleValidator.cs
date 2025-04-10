namespace BikeDoctor.Validations;

using BikeDoctor.Models;
using System.Text.RegularExpressions;

public class MotorcycleValidator
{
    private const string CLIENT_CI_ERROR = "El CI del cliente debe tener entre 7 y 8 dígitos numéricos.";
    private const string BRAND_ERROR = "La marca debe tener un máximo de 50 caracteres.";
    private const string MODEL_ERROR = "El modelo debe tener un máximo de 50 caracteres.";
    private const string LICENSE_PLATE_ERROR = "El número de matrícula debe tener exactamente 7 caracteres: 4 números seguidos de 3 letras mayúsculas (ej. 1234ABC).";
    private const string COLOR_ERROR = "El color debe tener un máximo de 30 caracteres.";

    public static void ValidateMotorcycle(Motorcycle motorcycle)
    {
        ValidateClientCI(motorcycle.ClientCI);
        ValidateBrand(motorcycle.Brand);
        ValidateModel(motorcycle.Model);
        ValidateLicensePlateNumber(motorcycle.LicensePlateNumber);
        ValidateColor(motorcycle.Color);
    }

    public static void ValidateClientCI(int clientCI)
    {
        string ciStr = clientCI.ToString();
        if (ciStr.Length < 7 || ciStr.Length > 8 || !ciStr.All(char.IsDigit))
            throw new ArgumentException(CLIENT_CI_ERROR);
    }

    public static void ValidateBrand(string brand)
    {
        if (string.IsNullOrEmpty(brand) || brand.Length > 50)
            throw new ArgumentException(BRAND_ERROR);
    }

    public static void ValidateModel(string model)
    {
        if (string.IsNullOrEmpty(model) || model.Length > 50)
            throw new ArgumentException(MODEL_ERROR);
    }

    public static void ValidateLicensePlateNumber(string licensePlate)
    {
        if (string.IsNullOrEmpty(licensePlate) || licensePlate.Length != 7 || 
            !Regex.IsMatch(licensePlate, @"^\d{4}[A-Z]{3}$"))
            throw new ArgumentException(LICENSE_PLATE_ERROR);
    }

    public static void ValidateColor(string color)
    {
        if (string.IsNullOrEmpty(color) || color.Length > 30)
            throw new ArgumentException(COLOR_ERROR);
    }
}