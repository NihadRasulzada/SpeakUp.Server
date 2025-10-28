using AutoMapper;
using MediatR;
using SpeakUp.Application.Interfaces.Repositories;
using SpeakUp.Common.Models.RequestModels;

namespace SpeakUp.Application.Features.Commands.EntryComment.Create;

public class CreateEntryCommentCommandHandler : IRequestHandler<CreateEntryCommentCommand, Guid>
{
    private readonly IEntryCommentRepository entryCommentRepository;
    private readonly IMapper mapper;

    public CreateEntryCommentCommandHandler(IEntryCommentRepository entryCommentRepository, IMapper mapper)
    {
        this.entryCommentRepository = entryCommentRepository;
        this.mapper = mapper;
    }

    public async Task<Guid> Handle(CreateEntryCommentCommand request, CancellationToken cancellationToken)
    {
        var dbEntryComment = mapper.Map<Domain.Models.EntryComment>(request);

        await entryCommentRepository.AddAsync(dbEntryComment);

        return dbEntryComment.Id;
    }
}