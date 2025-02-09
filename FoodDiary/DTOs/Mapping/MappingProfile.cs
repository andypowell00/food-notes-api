using AutoMapper;
using FoodDiary.DTOs;
using FoodDiary.Models;
using System.Linq;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entry to EntryDto mapping
        CreateMap<Entry, EntryDto>()
            .ForMember(dest => dest.Symptoms, opt => opt.MapFrom(src => src.EntrySymptoms.Select(es => es.Symptom)))
            .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.EntryIngredients.Select(ei => ei.Ingredient)));

        // DTOs to Entry mapping
        CreateMap<CreateEntryDto, Entry>();
        CreateMap<UpdateEntryDto, Entry>();

        // Symptom mappings
        CreateMap<Symptom, SymptomDto>();
        CreateMap<CreateSymptomDto, Symptom>();
        CreateMap<UpdateSymptomDto, Symptom>();

        // Ingredient mappings
        CreateMap<Ingredient, IngredientDto>();
        CreateMap<CreateIngredientDto, Ingredient>();
        CreateMap<UpdateIngredientDto, Ingredient>();

        // EntryIngredient and EntrySymptom mappings
        CreateMap<EntryIngredient, EntryIngredientDto>()
            .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name));
        CreateMap<CreateEntryIngredientDto, EntryIngredient>();
        CreateMap<UpdateEntryIngredientDto, EntryIngredient>();
        CreateMap<EntrySymptom, EntrySymptomDto>()
            .ForMember(dest => dest.SymptomTitle, opt => opt.MapFrom(src => src.Symptom.Title));
        CreateMap<CreateEntrySymptomDto, EntrySymptom>();
        CreateMap<UpdateEntrySymptomDto, EntrySymptom>();

    }
}
