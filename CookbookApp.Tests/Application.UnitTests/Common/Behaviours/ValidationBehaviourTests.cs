using CookbookApp.Application.Application.Common.Models;
using CookbookApp.Application.Common.Behaviours;
using CookbookApp.Application.Common.Models;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApp.Tests.Application.UnitTests
{
    public class ValidationBehaviourTests
    {
        private readonly Mock<AbstractValidator<IRequest<IRequestResult>>> _validator = new Mock<AbstractValidator<IRequest<IRequestResult>>>();
        private readonly Mock<IRequest<RequestResult>> _command = new Mock<IRequest<RequestResult>>();
        private readonly Mock<RequestResult> _okRequestResult = new Mock<RequestResult>();
        private readonly CancellationToken _cancelToken = new CancellationToken();

        public ValidationBehaviourTests()
        {
            _okRequestResult.SetupGet(ok => ok.Succeeded).Returns(true);
        }

        [Test]
        public async Task SendInvalidData_ShouldReturnFailRequestResult()
        {
            _validator
                .Setup(v => v.Validate(It.IsAny<ValidationContext<IRequest<IRequestResult>>>()))
                .Returns(new ValidationResult(new List<ValidationFailure>() { new ValidationFailure(string.Empty, string.Empty) }));
            var _validatorsList = new List<IValidator>() { _validator.Object };
            var behaviour = new ValidationBehaviour<IRequest<RequestResult>, RequestResult>(_validatorsList);
            RequestHandlerDelegate<RequestResult> nextDelegate = () => Task.FromResult(_okRequestResult.Object);

            var result = await behaviour.Handle(_command.Object, _cancelToken, nextDelegate);

            result.Succeeded.Should().BeFalse();
        }

        [Test]
        public async Task SendValidData_ShouldReturnSuccessRequestResult()
        {
            _validator
                .Setup(v => v.Validate(It.IsAny<ValidationContext<IRequest<IRequestResult>>>()))
                .Returns(new ValidationResult());
            var _validatorsList = new List<IValidator>() { _validator.Object };
            var behaviour = new ValidationBehaviour<IRequest<RequestResult>, RequestResult>(_validatorsList);
            RequestHandlerDelegate<RequestResult> nextDelegate = () => Task.FromResult(_okRequestResult.Object);

            var result = await behaviour.Handle(_command.Object, _cancelToken, nextDelegate);

            result.Succeeded.Should().BeTrue();
        }
    }
}
