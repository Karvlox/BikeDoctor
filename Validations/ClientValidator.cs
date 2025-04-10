namespace BikeDoctor.Validations;

using BikeDoctor.Models;

public class ClientValidator
{
    private const string CI_ERROR = "El CI debe tener entre 7 y 8 dígitos numéricos.";
    private const string NAME_ERROR = "El nombre debe tener máximo 50 caracteres y solo letras.";
    private const string LASTNAME_ERROR = "El apellido debe tener máximo 50 caracteres y solo letras.";
    private const string AGE_ERROR = "La edad debe estar entre 16 y 70 años.";
    private const string PHONE_ERROR = "El número de teléfono debe tener exactamente 8 dígitos.";
    private const string GENDER_ERROR = "El género debe ser 'MASCULINO' o 'FEMENINO' en mayúsculas.";
    private const string CI_MISMATCH_ERROR = "El CI de la URL no coincide con el del cuerpo de la solicitud.";

    public static void ValidateClientForAddOrUpdate(Client client)
    {
        ValidateCI(client.CI);
        ValidateName(client.Name);
        ValidateLastName(client.LastName);
        ValidateAge(client.Age);
        ValidatePhoneNumber(client.NumberPhone);
        ValidateGender(client.Gender);
    }

    public static void ValidateCI(int ci)
    {
        string ciStr = ci.ToString();
        if (ciStr.Length < 7 || ciStr.Length > 8 || !ciStr.All(char.IsDigit))
            throw new ArgumentException(CI_ERROR);
    }

    public static void ValidateName(string name)
    {
        if (string.IsNullOrEmpty(name) || name.Length > 50 || !name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            throw new ArgumentException(NAME_ERROR);
    }

    public static void ValidateLastName(string lastName)
    {
        if (string.IsNullOrEmpty(lastName) || lastName.Length > 50 || !lastName.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            throw new ArgumentException(LASTNAME_ERROR);
    }

    public static void ValidateAge(int age)
    {
        if (age < 16 || age > 70 || age.ToString().Length > 2)
            throw new ArgumentException(AGE_ERROR);
    }

    public static void ValidatePhoneNumber(int phoneNumber)
    {
        string phoneStr = phoneNumber.ToString();
        if (phoneStr.Length != 8 || !phoneStr.All(char.IsDigit))
            throw new ArgumentException(PHONE_ERROR);
    }

    public static void ValidateGender(string gender)
    {
        if (gender != "MASCULINO" && gender != "FEMENINO")
            throw new ArgumentException(GENDER_ERROR);
    }

    public static void ValidateCiMatch(int urlCi, int bodyCi)
    {
        if (urlCi != bodyCi)
            throw new ArgumentException(CI_MISMATCH_ERROR);
    }
}