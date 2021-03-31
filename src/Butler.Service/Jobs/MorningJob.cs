using Butler.Service.Quartz;
using Butler.Service.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Butler.Service.Jobs
{
    public class MorningJob : IJob, IScheduledJob
    {
        private MailService mailService;

        public MorningJob(MailService mailService)
        {
            this.mailService = mailService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var result = await Asset.Analyse();
            if (result.Funds?.Any() ?? false)
            {
                this.mailService.SendMail("Butler 早安邮件", result.GetDescription());
            }
        }

        public IJobDetail GetJobDetail()
        {
            return JobBuilder.Create<MorningJob>()
                .WithIdentity("MorningJob", "Butler.Service")
                .StoreDurably()
                .Build();
        }

        public IEnumerable<ITrigger> GetTriggers()
        {
            yield return TriggerBuilder.Create()
                .WithIdentity("MorningJob_Trigger1", "Butler.Service")
                .WithCronSchedule("0 0 7 * * ?")
                .ForJob("MorningJob", "Butler.Service")
                .Build();

            yield return TriggerBuilder.Create()
                .WithIdentity("MorningJob_RightNow", "Butler.Service")
                .StartNow()
                .ForJob("MorningJob", "Butler.Service")
                .Build();
        }
    }
}
