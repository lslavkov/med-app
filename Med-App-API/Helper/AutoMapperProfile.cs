using System.Linq;
using AutoMapper;
using Med_App_API.Dto;
using Med_App_API.Models;

namespace Med_App_API.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserForListDto>();
            CreateMap<User, UserForDetailedDto>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Patient, PatientForListDto>();
            CreateMap<Physician, PhysicianForListDto>();
            CreateMap<AppointmentForCreatingDto, Appointment>().ReverseMap();
            CreateMap<Appointment, AppointmentPatientForListDto>();
            CreateMap<Appointment, AppointmentPhysicianForListDto>();
            CreateMap<PatientVaccinated, PatientsVaccinatesForListDto>();
            CreateMap<PatientsVaccinatesForCreation, PatientVaccinated>().ReverseMap();
            CreateMap<VaccineForCreationDto, Vaccines>().ReverseMap();
        }
    }
}