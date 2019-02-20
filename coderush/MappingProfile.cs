using AutoMapper;
using coderush.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {          

            CreateMap<UserProfile, UserProfileViewModel>();
            CreateMap<UserProfileViewModel, UserProfile>();

            CreateMap<BookGeneral, BookGeneralViewModel>();
            CreateMap<BookGeneralViewModel, BookGeneral>();

            CreateMap<BookMeetingRoom, BookMeetingRoomViewModel>();
            CreateMap<BookMeetingRoomViewModel, BookMeetingRoom>();

            CreateMap<BookCar, BookCarViewModel>();
            CreateMap<BookCarViewModel, BookCar>();

            CreateMap<MeetingRoom, MeetingRoomViewModel>();
            CreateMap<MeetingRoomViewModel, MeetingRoom>();

            CreateMap<Car, CarViewModel>();
            CreateMap<CarViewModel, Car>();

            CreateMap<General, GeneralViewModel>();
            CreateMap<GeneralViewModel, General>();

            CreateMap<MyAgenda, MyAgendaViewModel>();
            CreateMap<MyAgendaViewModel, MyAgenda>();

        }
    }
}
