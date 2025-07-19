using System.ComponentModel.DataAnnotations;

namespace HMCTSCodingChallenge.API.Models;

public class TaskModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(100, ErrorMessage = "Title can't exceed 100 characters.")]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    public string Status { get; set; } = string.Empty;

    [Required(ErrorMessage = "DueDate is required.")]
    [DataType(DataType.DateTime)]
    public DateTime DueDate { get; set; }
}