namespace MedApp.Domain;

using Serilog;
using MedApp.Domain.Data.Models;

public static class PatientMatch
{
    private static readonly ILogger _logger = Log.ForContext(typeof(PatientMatch));

    public record Criteria(int Age, bool HasCancer, string City);

    public static bool IsPatientMatch(Patient patient, Criteria criteria)
    {
        _logger.Information("Matching patient {Patient} with criteria {Criteria}", patient, criteria);

        var result = MatchAge(patient, criteria.Age) &&
                     MatchHasCancer(patient, criteria.HasCancer) &&
                     MatchCity(patient, criteria.City);

        _logger.Information("Patient {Patient} matched with criteria {Criteria}: {Result}", patient, criteria, result);

        return result;
    }

    public static bool MatchAge(Patient patient, int age)
    {
        var result = GetAge(patient.DateOfBirth) >= age;
        _logger.Debug($"Matching age {patient.DateOfBirth} with criteria {age}: {result}");
        return result;
    }

    public static bool MatchHasCancer(Patient patient, bool hasCancer)
    {
        var result = patient.HasCancer == hasCancer;
        _logger.Debug("Matching hasCancer {PatientHasCancer} with criteria {CriteriaHasCancer}: {Result}", patient.HasCancer, hasCancer, result);
        return result;
    }

    public static bool MatchCity(Patient patient, string city)
    {
        var result = patient.Addresses?.FirstOrDefault()?.City.ToLower() == city.ToLower();
        _logger.Debug($"Matching city {patient.Addresses?.FirstOrDefault()?.City} with criteria {city}: {result}");
        return result;
    }

    private static int GetAge(DateTime dob)
    {
        var today = DateTime.Today;
        var age = today.Year - dob.Year;
        if (dob.Date > today.AddYears(-age)) age--;
        return age;
    }
}