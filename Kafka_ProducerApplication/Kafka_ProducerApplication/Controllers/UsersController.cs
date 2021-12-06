using System.Threading.Tasks;
using Kafka.Constants;
using Kafka.Interfaces;
using Kafka.Messages.UserRegistration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kafka_ProducerApplication.Controllers
{
	[ApiController]
	[Route("api/[controller]")]

	public class UsersController : ControllerBase
	{
		private readonly IKafkaProducer<string, RegisterUser> _kafkaProducer;
		public UsersController(IKafkaProducer<string, RegisterUser> kafkaProducer)
		{
			_kafkaProducer = kafkaProducer;
		}
		 
		[HttpPost]
		[Route("Register")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[SwaggerOperation("Register User", "This endpoint can be used to register a User ,but for demo produces dummy message in Kafka Topic")]
		public async Task<IActionResult> ProduceMessage(RegisterUser request)
		{
			await _kafkaProducer.ProduceAsync(KafkaTopics.RegisterUser, null, request);

			return Ok("User Registration In Progress");
		}
	}
}
