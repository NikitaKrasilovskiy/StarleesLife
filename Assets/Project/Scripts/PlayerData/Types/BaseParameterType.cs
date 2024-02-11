public enum BaseParameterType
{
    // Параметры бустов
    StarsLimit_CostBase = 11, StarsLimit_CostPercent = 12, StarsLimit_ValueBase = 13, StarsLimit_ValuePercent = 14, // Улучшение хранилища звездочек
    StarsGrow_Online_CostBase = 21, StarsGrow_Online_CostPercent = 22, StarsGrow_Online_ValueBase = 23, StarsGrow_Online_ValuePercent = 24, // Улучшение автороста звездочек в онлайне
    StarsGrow_Offline_CostBase = 31, StarsGrow_Offline_CostPercent = 32, StarsGrow_Offline_ValueBase = 33, StarsGrow_Offline_ValuePercent = 34, // Улучшение автороста звездочек в оффлайне
    SmilesForTap_CostBase = 41, SmilesForTap_CostPercent = 42, SmilesForTap_ValueBase = 43, SmilesForTap_ValuePercent = 44, // Улучшение количества смайликов за тап

    // Параметры возраста
    GrowLevel_CostBase = 51, GrowLevel_CostPercent = 52, GrowLevel_ValueBase = 53, GrowLevel_ValuePercent = 54, // Повышение возраста

    // Параметры настроения
    MoodFallingTime_Online = 60, MoodFallingTime_Offline = 61, // Скорость падения настроения

}