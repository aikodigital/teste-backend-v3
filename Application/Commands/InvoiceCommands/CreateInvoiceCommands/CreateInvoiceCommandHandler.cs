using Application.Commands.Base;
using Application.Commands.TheaterPlayCommands.CreateTheaterPlayCommands;
using Domain.DTOs;
using Domain.Entites;
using Domain.Interfaces;
using Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata;

namespace Application.Commands.InvoiceCommands.CreateInvoiceCommands
{
    public class CreateInvoiceCommandHandler(IInvoiceRepository _IInvoiceRepository,
        ITheaterPlayRepository _ITheaterPlayRepository,
        IReportRepository _IReportRepository,
        IReportCreditRepository _IReportCreditRepository,
        IPlayRepository _IPlayRepository) : CommandHandlerBase<CreateInvoiceCommand, InvoiceDTO>
    {
        public override async Task<ICommandResult<InvoiceDTO>> HandleAsync(CreateInvoiceCommand command)
        {
            var performance = new List<PerformanceEntity>();
            foreach (var item in command.Performances)
            {
               var play =  _IPlayRepository.Get(item.PlayId);
                performance.Add(item);

               if(play == null)
                {
                    throw new Exception("Play not found");
                }
            }
            InvoiceEntity entity = command.Dto();
            entity.Performances = performance;


            entity = await _IInvoiceRepository.AddAsync(entity); 
            var theaterPlayDTOs = new List<TheaterPlayDTO>();
            foreach (var item in entity.Performances)
            {
               
                var theaterPlay = ( _ITheaterPlayRepository.Find(p => p.PlayId == item.PlayId)).First();
                theaterPlay.Play = item.Play;
                theaterPlay.Play.Id = item.PlayId;
                theaterPlayDTOs.Add(theaterPlay);
            }
            StatementPrinter statementPrinter = new StatementPrinter();
            var reportList = statementPrinter.Report(entity, theaterPlayDTOs);
            var credits = statementPrinter.ReportCredits(reportList);

            var  creditsAdd = _IReportCreditRepository.AddAsync(credits);

            var entityReport = new List<ReportEntity>();
            foreach (var item in reportList)
            {
                item.ReportCreditId = credits.Id;
                entityReport.Add(item);
            }
            var report = _IReportRepository.AddRange(entityReport);

            var result = new CommandResult<InvoiceDTO>(true,"sucesso");
            result.Dto = entity;

            return result;
            
        }
    }
}
