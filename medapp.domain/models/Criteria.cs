using Serilog;

public static class PatientMatch
{
    private static readonly ILogger _logger = Log.ForContext(typeof(PatientMatch));

    public record Criteria(int Age, bool HasCancer, string Town);

    public static bool IsMatchPatient(Patient patient, Criteria criteria)
    {
        _logger.Information("Matching patient {Patient} with criteria {Criteria}", patient, criteria);

        var result = MatchAge(patient, criteria.Age) &&
                     MatchHasCancer(patient, criteria.HasCancer) &&
                     MatchTown(patient, criteria.Town);

        _logger.Information("Patient {Patient} matched with criteria {Criteria}: {Result}", patient, criteria, result);

        return result;
    }

    public static bool MatchAge(Patient patient, int age)
    {
        var result = patient.Age >= age;
        _logger.Debug("Matching age {PatientAge} with criteria {CriteriaAge}: {Result}", patient.Age, age, result);
        return result;
    }

    public static bool MatchHasCancer(Patient patient, bool hasCancer)
    {
        var result = patient.HasCancer == hasCancer;
        _logger.Debug("Matching hasCancer {PatientHasCancer} with criteria {CriteriaHasCancer}: {Result}", patient.HasCancer, hasCancer, result);
        return result;
    }

    public static bool MatchTown(Patient patient, string city)
    {
        var result = patient.Address.City.ToLower() == city.ToLower();
        _logger.Debug("Matching city {PatientCity} with criteria {CriteriaCity}: {Result}", patient.Address.City, city, result);
        return result;
    }
}