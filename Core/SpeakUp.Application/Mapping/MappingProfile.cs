using SpeakUp.Common.Models.Queries;
using SpeakUp.Common.Models.RequestModels;
using SpeakUp.Domain.Models;

namespace SpeakUp.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginUserViewModel>()
            .ReverseMap();

        CreateMap<CreateUserCommand, User>();

        CreateMap<UpdateUserCommand, User>();

        CreateMap<UserDetailViewModel, User>()
            .ReverseMap();

        CreateMap<CreateEntryCommand, Entry>()
            .ReverseMap();

        CreateMap<Entry, GetEntriesViewModel>()
            .ForMember(x => x.CommentCount, y => y.MapFrom(z => z.EntryComments.Count));


        CreateMap<CreateEntryCommentCommand, EntryComment>()
            .ReverseMap();
    }
}