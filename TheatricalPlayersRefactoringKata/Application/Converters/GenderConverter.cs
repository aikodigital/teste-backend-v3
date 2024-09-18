using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheatricalPlayersRefactoringKata.Domain.Entities.Gender;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Strategy;

namespace TheatricalPlayersRefactoringKata.Application.Converters
{
    public class GenderConverter : JsonConverter<IGender>
    {
        public override IGender Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            _ = Enum.TryParse(reader.GetInt32().ToString(), out GenderEnum result);
            
            return result switch
            {
                GenderEnum.Tragedy => new Tragedy(),
                GenderEnum.Comedy => new Comedy(),
                GenderEnum.History => new History(new Tragedy(), new Comedy()),
                _ => throw new JsonException($"Unknown gender: {result}")
            };
        }

        public override void Write(Utf8JsonWriter writer, IGender value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(nameof(value));
        }
    }
}
