using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Communication.Responses;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repositories;
using TheatricalPlayersRefactoringKata.Domain.Repositories.Plays;
using TheatricalPlayersRefactoringKata.Exception.ExceptionsBase;

namespace TheatricalPlayersRefactoringKata.Application.UseCases.Plays.Register
{
    public class RegisterPlayUseCase : IRegisterPlayUseCase
    {
        private readonly IPlaysWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterPlayUseCase(IPlaysWriteOnlyRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseRegisterPlayJson> Execute(RequestPlayJson request)
        {
            // validates the datas of request
            Validate(request);

            // create a new object Play and set values of object request to object Play
            var entity = new Play(request.Name, request.Lines, request.Type);

            await _repository.Add(entity);

            await _unitOfWork.Commit();

            return new ResponseRegisterPlayJson() { Id = entity.Id, Name = entity.Name };
        }

        private void Validate(RequestPlayJson request)
        {
            var validator = new PlayValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
