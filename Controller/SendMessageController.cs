using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BikeDoctor.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotPOC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SendController : ControllerBase
    {
        [HttpPost]
        [Route("Send")]
        public async Task<IActionResult> EnviaAsync([FromBody] MensajeRequest mensajeRequest)
        {
            string token =
                "EAAUghei0XtMBO6IYgbYsLC0D4kqVY8PkhgK2q5KdbZB5dJFBXzyvDbWZB4A6RIl8aZCgHfDe950x6ZCDcI6z2BZCJ5a7l5VArkYKVz2G5U0zFREGZAwrb44YrjDgGX0yzEWJRPZByuuZBrNt5Oh6DkmIqhM2sEeCLr2W417c4Ve3dXZAxEQCrJNXOMtLwFl9ZBRZAtVEGDtKEO3XsamH9I1KvUWFtzoJi9MQ7SpN9wZD";
            string idTelefono = "511685075369811";

            string telefono = "59173418089";

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://graph.facebook.com/v15.0/" + idTelefono + "/messages"
            );
            request.Headers.Add("Authorization", "Bearer " + token);

            var payload = new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = telefono,
                type = "text",
                text = new { body = mensajeRequest.Texto },
            };

            string jsonPayload = JsonSerializer.Serialize(payload);
            request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return Ok(new { message = "Mensaje enviado con éxito", response = responseBody });
            }
            else
            {
                string errorBody = await response.Content.ReadAsStringAsync();
                return StatusCode(
                    (int)response.StatusCode,
                    new { error = "Error al enviar el mensaje", details = errorBody }
                );
            }
        }
    }
}
