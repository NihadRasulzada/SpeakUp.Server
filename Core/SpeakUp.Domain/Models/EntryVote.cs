using SpeakUp.Common.ViewModels;

namespace SpeakUp.Domain.Models;

public class EntryVote : BaseEntity
{
    public Guid EntryId { get; set; }
    public VoteType VoteType { get; set; }
    public Guid CreatedById { get; set; }
    public virtual Entry Entry { get; set; }
}