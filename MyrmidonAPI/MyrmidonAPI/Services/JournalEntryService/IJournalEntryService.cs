using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Dtos.JournalEntry;
using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Services.JournalEntryService;

public interface IJournalEntryService
{
    Task<ServiceResponse<IActionResult>> AddJournalEntry(AddJournalEntryDto journalEntryDto);
    Task<ServiceResponse<IActionResult>> RemoveJournalEntry(int journalEntryId);
    Task<ServiceResponse<IActionResult>> UpdateJournalEntry(JournalEntry journalEntry);
    Task<ServiceResponse<IActionResult>> GetJournalEntry(int journalEntryId);
}