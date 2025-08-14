namespace BikeDoctor.Service;

using System.Collections.Generic;
using System.Threading.Tasks;
using BikeDoctor.Models;
using BikeDoctor.Repository;
using System.Text.RegularExpressions;


public class ReceptionService : GenericService<Reception, Guid>, IReceptionService
{
    public ReceptionService(IReceptionRepository repository)
        : base(repository) { }

    public async Task<IEnumerable<Reception>> GetAllByEmployeeCIAsync(
        int employeeCI,
        int pageNumber = 1,
        int pageSize = 10
    )
    {
        return await ((IReceptionRepository)_repository).GetAllByEmployeeCIAsync(
            employeeCI,
            pageNumber,
            pageSize
        );
    }

    public override async Task AddAsync(Reception reception)
    {
        ValidateReception(reception);
        await base.AddAsync(reception);
    }

    public override async Task UpdateAsync(Guid id, Reception reception)
    {
        ValidateReception(reception);
        await base.UpdateAsync(id, reception);
    }

    private void ValidateReception(Reception reception)
    {
        if (reception.ClientCI <= 0)
            throw new ArgumentException("El CI del cliente es obligatorio.");
        if (string.IsNullOrWhiteSpace(reception.MotorcycleLicensePlate))
            throw new ArgumentException("La placa de la motocicleta es obligatoria.");
        if (reception.EmployeeCI <= 0)
            throw new ArgumentException("El CI del empleado es obligatorio.");
    }

    public async Task<object> GetReasonsMetricsAsync(bool contarFrases = true)
    {
        var receptions = await _repository.GetAllAsync(); // obtiene todas las recepciones
        var razones = receptions
            .Where(r => r.Reasons != null)
            .SelectMany(r => r.Reasons!) // aplana la lista de listas
            .Where(m => !string.IsNullOrWhiteSpace(m));

        var resultado = CalcularFrecuencias(razones, contarFrases);

        return new { totalRegistros = receptions.Count(), motivosMasRepetidos = resultado };
    }

    private List<object> CalcularFrecuencias(IEnumerable<string> textos, bool contarFrases)
    {
        var contador = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        foreach (var texto in textos)
        {
            var limpio = Regex.Replace(texto.Trim(), @"[^\p{L}\p{Nd} ]+", "").Trim();

            if (contarFrases)
            {
                if (!contador.ContainsKey(limpio))
                    contador[limpio] = 0;
                contador[limpio]++;
            }
            else
            {
                var palabras = limpio.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var palabra in palabras)
                {
                    if (palabra.Length > 2)
                    {
                        if (!contador.ContainsKey(palabra))
                            contador[palabra] = 0;
                        contador[palabra]++;
                    }
                }
            }
        }

        return contador
            .OrderByDescending(c => c.Value)
            .Take(10)
            .Select(c => new { texto = c.Key, conteo = c.Value })
            .Cast<object>()
            .ToList();
    }
}
