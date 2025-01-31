﻿namespace BudgetAppBackend.Application.Models
{
    public class EmailSettings
    {
        public string? SmtpServer { get; set; }
        public int? SmtpPort { get; set; }
        public string? SenderEmail { get; set; }
        public string? EmailPassword { get; set; }
        public string? SenderName { get; set; }
    }
}
