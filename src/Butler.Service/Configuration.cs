namespace Butler.Service
{
    public class Configuration
    {
        public LoggingConfig Logging{get;set;}

        public MailConfig Mail { get; set; }
    }

    public class LoggingConfig
    {
        public SerilogConfig Serilog{get;set;}
    }

    public class SerilogConfig
    {
        public string File{get;set;}
    }

    public class MailConfig
    {
        /// <summary>
        /// ÓÊÏä·şÎñÆ÷
        /// </summary>
        public string SmtpServer { get; set; }

        public int SmtpPort { get; set; }

        /// <summary>
        /// ÓÊÏäÕË»§
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// ÓÊÏäÃÜÂë
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// ·¢ËÍÓÊÏä
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// ½ÓÊÕÓÊÏä
        /// </summary>
        public string To { get; set; }
    }
}