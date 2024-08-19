using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Converters
{
    public class PlayConverter : JsonConverter<Play>
    {
        public override Play Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObject = JsonDocument.ParseValue(ref reader).RootElement;
            var playId = Guid.Parse(jsonObject.GetProperty("playId").GetString());
            var name = jsonObject.GetProperty("name").GetString();
            var type = Enum.Parse<Genre>(jsonObject.GetProperty("type").GetString());
            var price = jsonObject.GetProperty("price").GetDecimal();
            var audience = jsonObject.GetProperty("audience").GetInt32();

            return new Play(playId, name, type, price, audience);
        }

        public override void Write(Utf8JsonWriter writer, Play value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("playId", value.PlayId.ToString());
            writer.WriteString("name", value.Name);
            writer.WriteString("type", value.Genre.ToString());
            writer.WriteNumber("price", value.Price);
            writer.WriteNumber("audience", value.Audience);
            writer.WriteEndObject();
        }
    }
}